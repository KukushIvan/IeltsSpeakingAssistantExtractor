using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using IeltsSpeakingAssistantExtractor.Properties;
using Newtonsoft.Json;
using LatexDocument;
using Newtonsoft.Json.Linq;

namespace IeltsSpeakingAssistantExtractor
{
    public partial class Form1 : Form
    {
        LatexDocument.Document _lt;
        private HttpSender _mySender;
        public Form1()
        {
            InitializeComponent();
            Application.ApplicationExit += Application_ApplicationExit;
            Load += Form1_Load;
            _mySender = new HttpSender();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbLatexLocation.Text = Settings.Default.LaTexLocation;
            tbResultFolder.Text = Settings.Default.ResultFolderLocation;
            tbResultFileName.Text = Settings.Default.ResultFileName;
            tbIdeaPrefix.Text = Settings.Default.IdeasPrefix;
            tbDictionaryPrefix.Text = Settings.Default.DictionaryPrefix;
            tbAnswerPrefix.Text = Settings.Default.AnswerPrefix;
            tbIdeaPrefix.Text = Settings.Default.IdeasPrefix;
            cbGenerateAnswers.Checked = Settings.Default.IsAnswers;
            cbGenerateDictionary.Checked = Settings.Default.IsDictionary;
            cbGenerateIdeas.Checked = Settings.Default.IsIdeas;
        }

        private void AddBulledParagraph(string paragraphName, dynamic List)
        {
            _lt.Add(new LatexParagraph(paragraphName));
            List<string> listOfTopics = new List<string>();
            foreach (dynamic item in List)
            {
                if ("text".Equals(item.Name)) //Тут стоит заглушка
                    continue;
                if (item.text != null && item.body != null)
                {
                    AddBulledParagraph(item.text.ToString(), item.body);
                }
                else
                    listOfTopics.Add(item.text.ToString());
            }

            _lt.Add(new LatexList(LatexList.BULLET, listOfTopics));
        }
        private void GenerateSection(string sectionName, string path)
        {
            LatexFont font = new LatexFont {Size = LatexFont.TEXT_Huge};
            _lt.Add(new LatexTextTitle(sectionName, font));
            string json = File.ReadAllText(path);
            string cleanJson = Regex.Replace(json, "<.*?>", "");
            dynamic testitems = JObject.Parse(cleanJson);
            foreach (JProperty question in testitems.content)
            {
                ;
                dynamic value = question.Value as dynamic;
                if (!value.active.Value)
                    continue;
                _lt.Add(new LatexParagraph(question.Name, "", font));
                if (Settings.Default.IsDictionary)
                {
                    var dictionary = new StringBuilder();
                    foreach (var sentence in value.vocabulary)
                    {
                        dictionary.Append(sentence.text);
                        dictionary.Append("\r\n");
                    }

                    _lt.Add(new LatexParagraph("Dictionary", dictionary.ToString().Trim()));
                }
                if (value.ideas != null || value.answer != null //Значит вопросы второго типа
                )
                {
                    string quest = value.questions[0].text + " " + value.questions[1].text;
                    _lt.Add(new LatexParagraph("Question: " + quest));
                    GenerateQuestion(value.questions[2], value.ideas, value.answer);
                }
                else
                    foreach (dynamic smallQuestion in value.questions)
                    {
                        GenerateQuestion(smallQuestion, smallQuestion.answer, smallQuestion.ideas);
                    }
            }
        }

        private void GenerateQuestion(dynamic question, dynamic answer, dynamic ideas)
        {
            _lt.Add(new LatexParagraph("Question: "));
            var questionList = new List<string>(question.text.ToString().Split('#'));
            _lt.Add(new LatexList(LatexList.BULLET, questionList));

            if (Settings.Default.IsAnswers 
                & answer.Count != 0)
                AddBulledParagraph("Answer: ", answer);
            if (Settings.Default.IsIdeas
            &ideas.Count != 0)
                AddBulledParagraph("Ideas: ", ideas);
        }

        private void btnStartGeneration_Click(object sender, EventArgs e)
        {
            string versionFile = Settings.Default.ResultFolderLocation + Path.DirectorySeparatorChar + "versions.json";
            bool isSectionsUpdate = false;
            string section1FileName =
                Settings.Default.ResultFolderLocation + Path.DirectorySeparatorChar + "section1.json";
            string section2FileName =
                Settings.Default.ResultFolderLocation + Path.DirectorySeparatorChar + "section2.json";
            string section3FileName =
                Settings.Default.ResultFolderLocation + Path.DirectorySeparatorChar + "section3.json";
            dynamic oldVersions = null, siteVersions;
            if (File.Exists(versionFile))
            {
                string versions = File.ReadAllText(versionFile);
                oldVersions = JObject.Parse(versions);
            }

            string siteVersion = _mySender.Get("https://dbvirtualeducation.com/ielts/api.php?versions ");
            siteVersions = JObject.Parse(siteVersion);
            if (oldVersions == null ||
                oldVersions.section1 == null
                | oldVersions.section1 != siteVersions.section1)
            {
                string section1 = _mySender.Get("https://dbvirtualeducation.com/ielts/api.php?section1 ");
                File.WriteAllText(section1FileName, section1);
                isSectionsUpdate = true;
            }
            if (oldVersions == null ||
                oldVersions.section2 == null
                | oldVersions.section2 != siteVersions.section2)
            {
                string section2 = _mySender.Get("https://dbvirtualeducation.com/ielts/api.php?section2");
                File.WriteAllText(section2FileName, section2);
                isSectionsUpdate = true;
            }
            if (oldVersions == null ||
                oldVersions.section3 == null
                | oldVersions.section3 != siteVersions.section3)
            {
                string section3 = _mySender.Get("https://dbvirtualeducation.com/ielts/api.php?section3");
                File.WriteAllText(section3FileName, section3);
                isSectionsUpdate = true;
            }
            if(isSectionsUpdate)
                File.WriteAllText(versionFile, siteVersion);

            _lt = new Document(Settings.Default.LaTexLocation, Settings.Default.ResultFolderLocation);
            LatexPageTitle title = new LatexPageTitle("IELTS Assistant");
            _lt.Add(title);
            GenerateSection("Section 1", section1FileName);
            GenerateSection("Section 2", section2FileName);
            GenerateSection("Section 3", section3FileName);
            _lt.CreatePdf(Settings.Default.ResultFileName +
                          (Settings.Default.IsDictionary ? Settings.Default.DictionaryPrefix: "")+
                          (Settings.Default.IsAnswers ? Settings.Default.AnswerPrefix : "") +
                          (Settings.Default.IsIdeas ? Settings.Default.IdeasPrefix : ""));
        }

        private void btnSelectLatex_Click(object sender, EventArgs e)
        {
            var result = ofdLatex.ShowDialog();
            if (result != DialogResult.OK) return;
            Settings.Default.LaTexLocation = ofdLatex.FileName;
            tbLatexLocation.Text = Settings.Default.LaTexLocation;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        private void btnResultFolder_Click(object sender, EventArgs e)
        {
            var result = fbdResultsFolder.ShowDialog();
            if (result != DialogResult.OK) return;
            Settings.Default.ResultFolderLocation = fbdResultsFolder.SelectedPath;
            tbResultFolder.Text = Settings.Default.ResultFolderLocation;
        }

        private void txbResultFileName_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ResultFileName = tbResultFileName.Text;
        }

        private void tbDictionaryPrefix_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.DictionaryPrefix = tbDictionaryPrefix.Text;
        }

        private void tbIdeaPrefix_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.IdeasPrefix = tbIdeaPrefix.Text;
        }

        private void tbAnswerPrefix_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.AnswerPrefix = tbAnswerPrefix.Text;
        }

        private void cbGenerateDictionary_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsDictionary = cbGenerateDictionary.Checked;
        }

        private void cbGenerateIdeas_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsIdeas = cbGenerateIdeas.Checked;
        }

        private void cbGenerateAnswers_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsAnswers = cbGenerateAnswers.Checked;
        }
    }
}