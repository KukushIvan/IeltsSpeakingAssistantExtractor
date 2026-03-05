using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;
using iText.Kernel.Pdf.Canvas.Parser;

namespace IeltsSpeakingAssistantExtractor;

public record GenerationOptions(
    string ResultFolder, 
    string ResultFileName,
    bool UsePrefixes,
    bool IsDictionary, string DictionaryPrefix,
    bool IsIdeas, string IdeaPrefix,
    bool IsAnswers, string AnswerPrefix);

public class PdfGeneratorService
{
    private HttpSender _mySender;
    private StringBuilder _txt;
    
    // QuestPDF Document Configuration
    private const string GlobalIdeaPrefix = "• ";

    public PdfGeneratorService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        _mySender = new HttpSender();
        _txt = new StringBuilder();
    }

    public void GenerateCore(GenerationOptions options, Action<string>? progressCallback = null)
    {
        _txt.Clear();
        string resultFolder = options.ResultFolder;
        
        if (!Directory.Exists(resultFolder))
        {
            Directory.CreateDirectory(resultFolder);
        }
        
        string cacheFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "IELTS_Extractor_Cache");
        if (!Directory.Exists(cacheFolder))
            Directory.CreateDirectory(cacheFolder);

        string versionFile = Path.Combine(cacheFolder, "versions.json");
        bool isSectionsUpdate = false;
        
        string section1FileName = Path.Combine(cacheFolder, "section1.json");
        string section2FileName = Path.Combine(cacheFolder, "section2.json");
        string section3FileName = Path.Combine(cacheFolder, "section3.json");
        
        dynamic? oldVersions = null, siteVersions;
        if (File.Exists(versionFile))
        {
            string versions = File.ReadAllText(versionFile);
            oldVersions = JObject.Parse(versions);
        }

        string siteVersion = _mySender.Get("https://dbvirtualeducation.com/ielts/api/versions");
        siteVersions = JObject.Parse(siteVersion);
        
        if (oldVersions == null || oldVersions.section1 == null || oldVersions.section1 != siteVersions.section1)
        {
            progressCallback?.Invoke("Downloading Section 1...");
            string section1 = _mySender.Get("https://dbvirtualeducation.com/ielts/api/section1");
            File.WriteAllText(section1FileName, section1);
            isSectionsUpdate = true;
        }
        if (oldVersions == null || oldVersions.section2 == null || oldVersions.section2 != siteVersions.section2)
        {
             progressCallback?.Invoke("Downloading Section 2...");
            string section2 = _mySender.Get("https://dbvirtualeducation.com/ielts/api/section2");
            File.WriteAllText(section2FileName, section2);
            isSectionsUpdate = true;
        }
        if (oldVersions == null || oldVersions.section3 == null || oldVersions.section3 != siteVersions.section3)
        {
            progressCallback?.Invoke("Downloading Section 3...");
            string section3 = _mySender.Get("https://dbvirtualeducation.com/ielts/api/section3");
            File.WriteAllText(section3FileName, section3);
            isSectionsUpdate = true;
        }
        
        if (isSectionsUpdate)
            File.WriteAllText(versionFile, siteVersion);

        // Track parsed data to build the PDF
        progressCallback?.Invoke("Generating parsing models...");
        var documentData = new PdfDocumentData();

        _txt.AppendLine("IELTS Assistant");
        
        GenerateSection("Section 1", section1FileName, documentData, options);
        GenerateSection("Section 2", section2FileName, documentData, options);
        GenerateSection("Section 3", section3FileName, documentData, options);

        string fileNameBase = options.ResultFileName;
        if (options.UsePrefixes)
        {
            fileNameBase += (options.IsDictionary ? options.DictionaryPrefix : "") +
                            (options.IsIdeas ? options.IdeaPrefix : "") +
                            (options.IsAnswers ? options.AnswerPrefix : "");
        }

        string txtFilePath = Path.Combine(resultFolder, fileNameBase + ".txt");
        File.WriteAllText(txtFilePath, _txt.ToString());

        // Generate QuestPDF Document
        progressCallback?.Invoke("Rendering PDF...");
        string pdfFilePath = Path.Combine(resultFolder, fileNameBase + ".pdf");
        GeneratePdf(pdfFilePath, documentData);
    }

    private void GenerateSection(string sectionName, string path, PdfDocumentData documentData, GenerationOptions options)
    {
        _txt.AppendLine();
        _txt.AppendLine(sectionName);
        _txt.AppendLine();
        
        var section = new PdfSection { Title = sectionName };
        documentData.Sections.Add(section);

        string json = File.ReadAllText(path);
        
        // Preserve line breaks and arrows before tearing down the rest of HTML
        string cleanJson = Regex.Replace(json, "<br\\s*/?>", "\\n", RegexOptions.IgnoreCase);
        cleanJson = Regex.Replace(cleanJson, "<arrow>", "• ", RegexOptions.IgnoreCase);
        cleanJson = Regex.Replace(cleanJson, "<.*?>", "");
        
        dynamic testitems = JObject.Parse(cleanJson);
        
        foreach (JProperty questionProp in testitems.content)
        {
            string name = questionProp.Name.Trim();
            if (name.Equals("how to", StringComparison.OrdinalIgnoreCase) || 
                name.StartsWith("BLACK FRIDAY", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("IELTS Speaking Assistant", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var pdfQuestion = new PdfQuestion { Title = name, Dictionary = "" };
            dynamic value = questionProp.Value as dynamic ?? new JObject();
            
            if (value.active != null && !value.active.Value)
                continue;
                
            _txt.AppendLine(questionProp.Name);

            if (options.IsDictionary && value.vocabulary != null)
            {
                var dictionary = new StringBuilder();
                foreach (var sentence in value.vocabulary)
                {
                    dictionary.AppendLine(sentence.text.ToString());
                }
                
                string dictText = dictionary.ToString().Trim();
                _txt.AppendLine();
                _txt.AppendLine("Dictionary:");
                _txt.AppendLine(dictText);
                
                if (!string.IsNullOrEmpty(dictText))
                    pdfQuestion.Dictionary = dictText;
            }

            if (value.ideas != null || value.answer != null)
            {
                string quest = "";
                if (value.questions != null && value.questions.Count > 1) 
                    quest = value.questions[0].text + " " + value.questions[1].text;
                
                _txt.AppendLine();
                _txt.AppendLine("Question: " + quest);
                
                var qp = new PdfQuestionPart { Title = quest };
                if (value.questions != null && value.questions.Count > 2) 
                    GenerateQuestion(value.questions[2], value.ideas, value.answer, qp, options);
                
                pdfQuestion.Parts.Add(qp);
            }
            else if (value.questions != null)
            {
                foreach (dynamic smallQuestion in value.questions)
                {
                    string qText = smallQuestion.text.ToString();
                    var qp = new PdfQuestionPart { Title = qText.Replace("#", " ") };
                    GenerateQuestion(smallQuestion, smallQuestion.answer, smallQuestion.ideas, qp, options);
                    pdfQuestion.Parts.Add(qp);
                }
            }
            
            section.Questions.Add(pdfQuestion);
        }
    }

    private void GenerateQuestion(dynamic question, dynamic answer, dynamic ideas, PdfQuestionPart qp, GenerationOptions options)
    {
        _txt.AppendLine();
        _txt.AppendLine("Question: ");
        string questions = question.text.ToString();
        var questionList = new List<string>(questions.Split('#'));
        
        _txt.AppendLine(questions.Replace("#", "\r\n"));

        if (options.IsAnswers && answer != null && answer.Count != 0)
        {
            var answerBlock = new PdfBlock { Title = "Answer: " };
            AddBulledParagraph("Answer: ", answer, answerBlock);
            qp.Blocks.Add(answerBlock);
        }
            
        if (options.IsIdeas && ideas != null && ideas.Count != 0)
        {
            var ideaBlock = new PdfBlock { Title = "Ideas: " };
            AddBulledParagraph("Ideas: ", ideas, ideaBlock);
            qp.Blocks.Add(ideaBlock);
        }
    }

    private void AddBulledParagraph(string paragraphName, dynamic List, PdfBlock currentBlock)
    {
        _txt.AppendLine();
        _txt.AppendLine(paragraphName);

        foreach (dynamic item in List)
        {
            if ("text".Equals((string)item.Name)) 
                continue;
                
            if (item.text != null && item.body != null)
            {
                var childBlock = new PdfBlock { Title = item.text.ToString() };
                AddBulledParagraph(item.text.ToString(), item.body, childBlock);
                currentBlock.Children.Add(childBlock);
            }
            else if (item.text != null)
            {
                string text = item.text.ToString();
                var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    string trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed)) continue;
                    
                    currentBlock.Items.Add(trimmed);
                    _txt.AppendLine("• " + trimmed);
                }
            }
        }
    }
    
    // ----------- QuestPDF Generation Methods -----------

    private void GeneratePdf(string filePath, PdfDocumentData data)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11).FontFamily(Fonts.Arial));

                page.Header().Element(ComposeHeader);
                page.Content().Element(x => ComposeContent(x, data));
                
                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        });
        
        byte[] rawPdf = document.GeneratePdf();
        AddPdfBookmarks(rawPdf, filePath, data);
    }

    private void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text("IELTS Assistant").FontSize(24).SemiBold().FontColor(Colors.Blue.Darken2);
            });
        });
    }

    private void ComposeContent(IContainer container, PdfDocumentData data)
    {
        container.PaddingVertical(1, Unit.Centimetre).Column(col =>
        {
            foreach (var section in data.Sections)
            {
                col.Item().Section(section.Title).PaddingTop(15).Text(section.Title).FontSize(24).Bold().FontColor(Colors.Blue.Darken1);
                col.Item().PaddingBottom(10).LineHorizontal(2).LineColor(Colors.Blue.Lighten4);
                
                foreach (var question in section.Questions)
                {
                    col.Item().Section(question.Title).PaddingTop(15).Text(question.Title).FontSize(16).Bold().FontColor(Colors.Grey.Darken3);
                    
                    if (!string.IsNullOrEmpty(question.Dictionary))
                    {
                        col.Item().PaddingTop(8).Text("Dictionary:").FontSize(13).SemiBold().FontColor(Colors.Teal.Darken2);
                        
                        // Treat dictionary with proper spacing for idioms etc
                        var dictLines = question.Dictionary.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                        col.Item().PaddingTop(4).Column(dictCol => 
                        {
                            foreach(var line in dictLines)
                            {
                                string trimmed = line.Trim();
                                bool isHeader = trimmed.EndsWith(":") || (trimmed.Length > 2 && trimmed.ToUpper() == trimmed && !trimmed.Contains("?"));
                                
                                if (isHeader)
                                {
                                    dictCol.Item().PaddingTop(8).Text(trimmed).SemiBold().FontSize(12).FontColor(Colors.Teal.Darken3);
                                }
                                else
                                {
                                    if (Regex.IsMatch(trimmed, @"^\d+\."))
                                        dictCol.Item().PaddingTop(4).Text(trimmed).FontSize(11).LineHeight(1.3f);
                                    else
                                        dictCol.Item().Text(trimmed).FontSize(11).LineHeight(1.3f);
                                }
                            }
                        });
                    }
                    
                    foreach (var part in question.Parts)
                    {
                        col.Item().PaddingTop(10).Text(part.Title).SemiBold().FontSize(12).FontColor(Colors.Black);
                        
                        foreach (var block in part.Blocks)
                        {
                            RenderPdfBlock(col, block, 0);
                        }
                    }
                    
                    col.Item().PaddingTop(15).PaddingBottom(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten3);
                }
            }
        });
    }

    private void RenderPdfBlock(ColumnDescriptor col, PdfBlock block, int depth)
    {
        if (!string.IsNullOrWhiteSpace(block.Title) && block.Title != "text")
        {
            col.Item().PaddingLeft(depth * 10).PaddingTop(5).Text(block.Title).Italic().FontColor(Colors.Grey.Darken2);
        }

        foreach (var item in block.Items)
        {
            bool isHeader = item.EndsWith(":") || item.EndsWith("?") || (item.Length > 4 && item.ToUpper() == item) || item.StartsWith("CATEGORY ", StringComparison.OrdinalIgnoreCase);
            
            if (isHeader)
            {
                col.Item().PaddingLeft(depth * 10).PaddingTop(8).Text(item).SemiBold().FontColor(Colors.Grey.Darken3);
            }
            else
            {
                string cleanItem = item;
                if (cleanItem.StartsWith("•")) cleanItem = cleanItem.Substring(1).Trim();
                else if (cleanItem.StartsWith("-")) cleanItem = cleanItem.Substring(1).Trim();
                
                col.Item().PaddingLeft((depth * 10) + 10).Row(row =>
                {
                    row.ConstantItem(15).Text("•").FontColor(Colors.Grey.Darken1);
                    row.RelativeItem().Text(cleanItem).LineHeight(1.3f);
                });
            }
        }

        foreach (var child in block.Children)
        {
            RenderPdfBlock(col, child, depth + 1);
        }
    }

    private void AddPdfBookmarks(byte[] rawPdf, string filePath, PdfDocumentData data)
    {
        string tmpPath = filePath + ".tmp";
        if (File.Exists(tmpPath))
        {
            File.Delete(tmpPath);
        }

        using (var readStream = new MemoryStream(rawPdf))
        using (PdfReader reader = new PdfReader(readStream))
        using (PdfWriter writer = new PdfWriter(tmpPath))
        using (PdfDocument pdfDoc = new PdfDocument(reader, writer))
        {
            pdfDoc.InitializeOutlines();
            PdfOutline rootOutline = pdfDoc.GetOutlines(false);
            
            int totalPages = pdfDoc.GetNumberOfPages();
            var pageTexts = new string[totalPages];
            
            for (int i = 1; i <= totalPages; i++)
            {
                var page = pdfDoc.GetPage(i);
                pageTexts[i - 1] = PdfTextExtractor.GetTextFromPage(page).Replace("\n", " ").Replace("\r", " ").ToLowerInvariant();
            }

            int startSearchPage = 1;

            foreach (var section in data.Sections)
            {
                int sectionPage = FindPage(pageTexts, section.Title, startSearchPage);
                if (sectionPage == -1) sectionPage = startSearchPage;
                startSearchPage = sectionPage;

                var sectionOutline = rootOutline.AddOutline(section.Title);
                sectionOutline.AddDestination(PdfExplicitDestination.CreateFit(pdfDoc.GetPage(sectionPage)));

                foreach (var question in section.Questions)
                {
                    int questionPage = FindPage(pageTexts, question.Title, startSearchPage);
                    if (questionPage == -1) questionPage = startSearchPage;
                    startSearchPage = questionPage;

                    var questionOutline = sectionOutline.AddOutline(question.Title);
                    questionOutline.AddDestination(PdfExplicitDestination.CreateFit(pdfDoc.GetPage(questionPage)));
                }
            }
        }

        // Atomically replace the final file
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        File.Move(tmpPath, filePath, true);
    }

    private int FindPage(string[] pageTexts, string title, int startPage)
    {
        string searchStr = title.Replace("\n", " ").Replace("\r", " ").ToLowerInvariant();
        searchStr = System.Text.RegularExpressions.Regex.Replace(searchStr, @"\s+", " ").Trim();
        
        string escapedSearch = System.Text.RegularExpressions.Regex.Escape(searchStr);
        var strictRegex = new System.Text.RegularExpressions.Regex(@"(?<=^|\W)" + escapedSearch + @"(?=\W|$)");

        // Pass 1: Strict word boundary match (to prevent "Art" matching "pARTicipate")
        for (int i = startPage - 1; i < pageTexts.Length; i++)
        {
            string pageText = System.Text.RegularExpressions.Regex.Replace(pageTexts[i], @"\s+", " ");
            if (strictRegex.IsMatch(pageText))
            {
                return i + 1;
            }
        }
        
        // Pass 2: If strict match fails, use Contains() ONLY if the string is long enough
        // to not be a common substring. This prevents false positives for short titles.
        if (searchStr.Length >= 7)
        {
            for (int i = startPage - 1; i < pageTexts.Length; i++)
            {
                string pageText = System.Text.RegularExpressions.Regex.Replace(pageTexts[i], @"\s+", " ");
                if (pageText.Contains(searchStr))
                {
                    return i + 1;
                }
            }
            
            // Pass 3: Contains on first 20 characters if it's really long
            if (searchStr.Length > 20)
            {
                string shortSearch = searchStr.Substring(0, 20);
                for (int i = startPage - 1; i < pageTexts.Length; i++)
                {
                     string pageText = System.Text.RegularExpressions.Regex.Replace(pageTexts[i], @"\s+", " ");
                     if (pageText.Contains(shortSearch))
                         return i + 1;
                }
            }
        }

        return -1;
    }
}

// Data models for parsing content before pushing to QuestPDF
public class PdfDocumentData
{
    public List<PdfSection> Sections { get; set; } = new();
}

public class PdfSection
{
    public string Title { get; set; } = "";
    public List<PdfQuestion> Questions { get; set; } = new();
}

public class PdfQuestion
{
    public string Title { get; set; } = "";
    public string Dictionary { get; set; } = "";
    public List<PdfQuestionPart> Parts { get; set; } = new();
}

public class PdfQuestionPart
{
    public string Title { get; set; } = "";
    public List<PdfBlock> Blocks { get; set; } = new();
}

public class PdfBlock
{
    public string Title { get; set; } = "";
    public List<string> Items { get; set; } = new();
    public List<PdfBlock> Children { get; set; } = new();
}
