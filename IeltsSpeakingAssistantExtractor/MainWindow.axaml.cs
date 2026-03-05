using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using QuestPDF.Infrastructure;

namespace IeltsSpeakingAssistantExtractor;

public partial class MainWindow : Window
{
    private string _curLang = "Русский";
    private bool _isInitialized = false;

    public MainWindow()
    {
        InitializeComponent();
        ResultFileNameTextBox.Text = $"IeltsAssistant_{DateTime.Now:yyyy-MM-dd}";
        SetDefaultLanguage();
        LoadSettings();
        _isInitialized = true;
        this.Closing += (s, e) => SaveSettings();
    }

    private void SetDefaultLanguage()
    {
        var sysLang = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLower();

        string targetLang = "English"; // fallback
        if (sysLang == "ru" || sysLang == "be" || sysLang == "uk" || sysLang == "kk") targetLang = "Русский";
        else if (sysLang == "zh") targetLang = "中文";
        else if (sysLang == "hi") targetLang = "हिन्दी";
        else if (sysLang == "vi") targetLang = "Tiếng Việt";
        else if (sysLang == "ar") targetLang = "العربية";
        else if (sysLang == "es") targetLang = "Español";

        // Find the index of the language in the combobox
        int targetIndex = 1; // Default to English index (1)
        if (LanguageComboBox != null && LanguageComboBox.Items != null)
        {
            int index = 0;
            foreach (Avalonia.Controls.ComboBoxItem item in LanguageComboBox.Items)
            {
                if (item.Content?.ToString() == targetLang)
                {
                    targetIndex = index;
                    break;
                }
                index++;
            }
            LanguageComboBox.SelectedIndex = targetIndex;
        }
    }

    private void OnLanguageChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LanguageComboBox == null) return;
        var item = LanguageComboBox.SelectedItem as ComboBoxItem;
        _curLang = item?.Content?.ToString() ?? "English";
        UpdateLocalization();
        if (_isInitialized) SaveSettings(); // Save immediately when language changes
    }

    private void UpdateLocalization()
    {
        if (_curLang == "Русский")
        {
            AppSubtitle.Text = "Экспорт уроков и генерация PDF";
            LabelFolder.Content = "Папка для результатов:";
            SelectFolderButton.Content = "Выбрать папку";
            LabelFilename.Content = "Имя базового файла:";
            LabelOptions.Text = "Настройки генерации";
            UsePrefixesCheckBox.Content = "Использовать префиксы";
            GenerateDictionaryCheckBox.Content = "Генерировать словарь";
            LabelDictPrefix.Content = "Префикс:";
            GenerateIdeasCheckBox.Content = "Генерировать идеи";
            LabelIdeaPrefix.Content = "Префикс:";
            GenerateAnswersCheckBox.Content = "Генерировать ответы";
            LabelAnswerPrefix.Content = "Префикс:";
            StartGenerationButton.Content = "Генерировать PDF 🚀";
        }
        else if (_curLang == "中文")
        {
            AppSubtitle.Text = "课程导出与 PDF 生成";
            LabelFolder.Content = "输出文件夹：";
            SelectFolderButton.Content = "选择文件夹";
            LabelFilename.Content = "基础文件名：";
            LabelOptions.Text = "生成选项";
            UsePrefixesCheckBox.Content = "使用前缀";
            GenerateDictionaryCheckBox.Content = "生成词典";
            LabelDictPrefix.Content = "前缀：";
            GenerateIdeasCheckBox.Content = "生成想法";
            LabelIdeaPrefix.Content = "前缀：";
            GenerateAnswersCheckBox.Content = "生成答案";
            LabelAnswerPrefix.Content = "前缀：";
            StartGenerationButton.Content = "生成 PDF 🚀";
        }
        else if (_curLang == "हिन्दी")
        {
            AppSubtitle.Text = "पाठ निर्यात और PDF निर्माण";
            LabelFolder.Content = "आउटपुट फ़ोल्डर:";
            SelectFolderButton.Content = "फ़ोल्डर चुनें";
            LabelFilename.Content = "मूल फ़ाइल नाम:";
            LabelOptions.Text = "निर्माण विकल्प";
            UsePrefixesCheckBox.Content = "उपसर्गों का उपयोग करें";
            GenerateDictionaryCheckBox.Content = "शब्दकोश बनाएँ";
            LabelDictPrefix.Content = "उपसर्ग:";
            GenerateIdeasCheckBox.Content = "विचार बनाएँ";
            LabelIdeaPrefix.Content = "उपसर्ग:";
            GenerateAnswersCheckBox.Content = "उत्तर बनाएँ";
            LabelAnswerPrefix.Content = "उपसर्ग:";
            StartGenerationButton.Content = "PDF बनाएँ 🚀";
        }
        else if (_curLang == "Tiếng Việt")
        {
            AppSubtitle.Text = "Xuất bài học và tạo PDF";
            LabelFolder.Content = "Thư mục đầu ra:";
            SelectFolderButton.Content = "Chọn thư mục";
            LabelFilename.Content = "Tên tệp cơ sở:";
            LabelOptions.Text = "Tùy chọn tạo";
            UsePrefixesCheckBox.Content = "Sử dụng tiền tố";
            GenerateDictionaryCheckBox.Content = "Tạo từ điển";
            LabelDictPrefix.Content = "Tiền tố:";
            GenerateIdeasCheckBox.Content = "Tạo ý tưởng";
            LabelIdeaPrefix.Content = "Tiền tố:";
            GenerateAnswersCheckBox.Content = "Tạo câu trả lời";
            LabelAnswerPrefix.Content = "Tiền tố:";
            StartGenerationButton.Content = "Tạo PDF 🚀";
        }
        else if (_curLang == "العربية")
        {
            AppSubtitle.Text = "تصدير الدروس وإنشاء PDF";
            LabelFolder.Content = "مجلد الإخراج:";
            SelectFolderButton.Content = "حدد المجلد";
            LabelFilename.Content = "اسم الملف الأساسي:";
            LabelOptions.Text = "خيارات الإنشاء";
            UsePrefixesCheckBox.Content = "استخدام البادئات";
            GenerateDictionaryCheckBox.Content = "إنشاء قاموس";
            LabelDictPrefix.Content = "بادئة:";
            GenerateIdeasCheckBox.Content = "إنشاء أفكار";
            LabelIdeaPrefix.Content = "بادئة:";
            GenerateAnswersCheckBox.Content = "إنشاء إجابات";
            LabelAnswerPrefix.Content = "بادئة:";
            StartGenerationButton.Content = "إنشاء PDF 🚀";
        }
        else if (_curLang == "Español")
        {
            AppSubtitle.Text = "Exportación de lecciones y generación de PDF";
            LabelFolder.Content = "Carpeta de salida:";
            SelectFolderButton.Content = "Seleccionar carpeta";
            LabelFilename.Content = "Nombre de archivo base:";
            LabelOptions.Text = "Opciones de generación";
            UsePrefixesCheckBox.Content = "Usar prefijos";
            GenerateDictionaryCheckBox.Content = "Generar diccionario";
            LabelDictPrefix.Content = "Prefijo:";
            GenerateIdeasCheckBox.Content = "Generar ideas";
            LabelIdeaPrefix.Content = "Prefijo:";
            GenerateAnswersCheckBox.Content = "Generar respuestas";
            LabelAnswerPrefix.Content = "Prefijo:";
            StartGenerationButton.Content = "Generar PDF 🚀";
        }
        else // English default
        {
            AppSubtitle.Text = "Lessons export and PDF generation";
            LabelFolder.Content = "Output Folder:";
            SelectFolderButton.Content = "Select Folder";
            LabelFilename.Content = "Base File Name:";
            LabelOptions.Text = "Generation Options";
            UsePrefixesCheckBox.Content = "Use prefixes";
            GenerateDictionaryCheckBox.Content = "Generate Dictionary";
            LabelDictPrefix.Content = "Prefix:";
            GenerateIdeasCheckBox.Content = "Generate Ideas";
            LabelIdeaPrefix.Content = "Prefix:";
            GenerateAnswersCheckBox.Content = "Generate Answers";
            LabelAnswerPrefix.Content = "Prefix:";
            StartGenerationButton.Content = "Generate PDF 🚀";
        }
    }

    private void LoadSettings()
    {
        // Simple file-based config since Avalonia doesn't use Properties.Settings by default
        string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IELTS_Extractor_Settings.json");
        if (File.Exists(configPath))
        {
            try
            {
                var json = File.ReadAllText(configPath);
                var settings = JObject.Parse(json);
                
                ResultFolderTextBox.Text = settings["ResultFolderLocation"]?.ToString();
                
                string? loadedName = settings["ResultFileName"]?.ToString();
                bool isDefaultTemplate = false;
                if (!string.IsNullOrEmpty(loadedName) && loadedName.StartsWith("IeltsAssistant_"))
                {
                    string datePart = loadedName.Substring("IeltsAssistant_".Length);
                    if (DateTime.TryParseExact(datePart, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
                    {
                        isDefaultTemplate = true;
                    }
                }

                if (string.IsNullOrEmpty(loadedName) || loadedName == "IeltsAssistant" || isDefaultTemplate)
                {
                    loadedName = $"IeltsAssistant_{DateTime.Now:yyyy-MM-dd}";
                }
                ResultFileNameTextBox.Text = loadedName;

                string? dPref = settings["DictionaryPrefix"]?.ToString();
                if (!string.IsNullOrEmpty(dPref)) DictionaryPrefixTextBox.Text = dPref;

                string? iPref = settings["IdeaPrefix"]?.ToString();
                if (!string.IsNullOrEmpty(iPref)) IdeaPrefixTextBox.Text = iPref;

                string? aPref = settings["AnswerPrefix"]?.ToString();
                if (!string.IsNullOrEmpty(aPref)) AnswerPrefixTextBox.Text = aPref;
                
                GenerateDictionaryCheckBox.IsChecked = settings["IsDictionary"]?.Value<bool>() ?? true;
                GenerateIdeasCheckBox.IsChecked = settings["IsIdeas"]?.Value<bool>() ?? true;
                GenerateAnswersCheckBox.IsChecked = settings["IsAnswers"]?.Value<bool>() ?? true;
                UsePrefixesCheckBox.IsChecked = settings["UsePrefixes"]?.Value<bool>() ?? true;
                UsePrefixesCheckBox.IsChecked = settings["UsePrefixes"]?.Value<bool>() ?? true;

                string? savedLang = settings["SelectedLanguage"]?.ToString();
                if (!string.IsNullOrEmpty(savedLang) && LanguageComboBox != null && LanguageComboBox.Items != null)
                {
                    int index = 0;
                    foreach (Avalonia.Controls.ComboBoxItem item in LanguageComboBox.Items)
                    {
                        if (item.Content?.ToString() == savedLang)
                        {
                            LanguageComboBox.SelectedIndex = index;
                            break;
                        }
                        index++;
                    }
                }
            }
            catch { /* Ignore if corrupted */ }
        }
    }

    private void SaveSettings()
    {
        string configDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string configPath = Path.Combine(configDir, "IELTS_Extractor_Settings.json");
        
        var settings = new JObject
        {
            ["ResultFolderLocation"] = ResultFolderTextBox.Text,
            ["ResultFileName"] = ResultFileNameTextBox.Text,
            ["DictionaryPrefix"] = DictionaryPrefixTextBox.Text,
            ["IdeaPrefix"] = IdeaPrefixTextBox.Text,
            ["AnswerPrefix"] = AnswerPrefixTextBox.Text,
            ["IsDictionary"] = GenerateDictionaryCheckBox.IsChecked,
            ["IsIdeas"] = GenerateIdeasCheckBox.IsChecked,
            ["IsAnswers"] = GenerateAnswersCheckBox.IsChecked,
            ["SelectedLanguage"] = _curLang
        };
        
        File.WriteAllText(configPath, settings.ToString());
    }

    private string GetLoc(string key)
    {
        if (key == "SelectFolder")
        {
            return _curLang switch
            {
                "Русский" => "Выберите папку с результатами",
                "中文" => "选择结果文件夹",
                "हिन्दी" => "परिणाम फ़ोल्डर चुनें",
                "Tiếng Việt" => "Chọn thư mục kết quả",
                "العربية" => "حدد مجلد النتائج",
                "Español" => "Seleccionar carpeta de resultados",
                _ => "Select Result Folder"
            };
        }
        return key;
    }

    private async void OnSelectFolderClicked(object? sender, RoutedEventArgs e)
    {
        IReadOnlyList<Avalonia.Platform.Storage.IStorageFolder> folders;
        try
        {
            folders = await StorageProvider.OpenFolderPickerAsync(new Avalonia.Platform.Storage.FolderPickerOpenOptions
            {
                Title = GetLoc("SelectFolder"),
                AllowMultiple = false
            });
        }
        catch (Exception ex)
        {
            StatusTextBlock.Text = GetErrorLoc("Exception", "Folder dialog error: " + ex.Message);
            StatusTextBlock.Foreground = Avalonia.Media.Brushes.Red;
            return;
        }

        if (folders.Count >= 1)
        {
            try
            {
                var folder = folders[0];
                try
                {
                    if (folder.Path != null && folder.Path.IsAbsoluteUri)
                    {
                        ResultFolderTextBox.Text = folder.Path.LocalPath;
                        StatusTextBlock.Text = "";
                        return;
                    }
                }
                catch { /* Ignore Path exceptions and fallback */ }
                
                try
                {
                    var str = folder.Path?.ToString() ?? "";
                    if (str.StartsWith("file:///")) str = str.Substring(8);
                    str = Uri.UnescapeDataString(str);
                    if (!string.IsNullOrEmpty(str))
                    {
                        ResultFolderTextBox.Text = str;
                        StatusTextBlock.Text = "";
                        return;
                    }
                }
                catch { /* Ignore string parsing exceptions */ }

                ResultFolderTextBox.Text = folder.Name;
                StatusTextBlock.Text = "";
            }
            catch (Exception ex)
            {
                // Absolute fallback if everything including folder.Name throws
                StatusTextBlock.Text = GetErrorLoc("Exception", "Cannot read folder path: " + ex.Message);
                StatusTextBlock.Foreground = Avalonia.Media.Brushes.Red;
            }
        }
    }

    private string GetErrorLoc(string key, string? extra = null)
    {
        string message = "";
        if (key == "NoFolder")
        {
            message = _curLang switch
            {
                "Русский" => "Ошибка: Папка результатов не выбрана или не существует.",
                "中文" => "错误：未选择或找不到输出文件夹。",
                "हिन्दी" => "त्रुटि: आउटपुट फ़ोल्डर नहीं चुना गया है या मौजूद नहीं है।",
                "Tiếng Việt" => "Lỗi: Thư mục đầu ra chưa được chọn hoặc không tồn tại.",
                "العربية" => "خطأ: لم يتم تحديد مجلد الإخراج أو أنه مفقود.",
                "Español" => "Error: No se seleccionó la carpeta de salida o no existe.",
                _ => "Error: Output folder is not selected or missing."
            };
        }
        else if (key == "Starting")
        {
            message = _curLang switch
            {
                "Русский" => "Начинаю загрузку данных...",
                "中文" => "开始下载数据...",
                "हिन्दी" => "डेटा डाउनलोड शुरू हो रहा है...",
                "Tiếng Việt" => "Đang bắt đầu tải dữ liệu...",
                "العربية" => "جاري بدء تنزيل البيانات...",
                "Español" => "Iniciando descarga de datos...",
                _ => "Starting data download..."
            };
        }
        else if (key == "Done")
        {
            message = _curLang switch
            {
                "Русский" => "Готово! Файлы успешно сгенерированы.",
                "中文" => "完成！文件已成功生成。",
                "हिन्दी" => "पूर्ण! फ़ाइलें सफलतापूर्वक उत्पन्न हो गईं।",
                "Tiếng Việt" => "Hoàn thành! Các tệp đã được tạo thành công.",
                "العربية" => "تم! تم إنشاء الملفات بنجاح.",
                "Español" => "¡Hecho! Archivos generados con éxito.",
                _ => "Done! Files successfully generated."
            };
        }
        else if (key == "Exception")
        {
            message = _curLang switch
            {
                "Русский" => $"Ошибка: {extra}",
                "中文" => $"错误：{extra}",
                "हिन्दी" => $"त्रुटि: {extra}",
                "Tiếng Việt" => $"Lỗi: {extra}",
                "العربية" => $"خطأ: {extra}",
                "Español" => $"Error: {extra}",
                _ => $"Error: {extra}"
            };
        }
        return message;
    }

    private async void OnStartGenerationClicked(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(ResultFolderTextBox.Text) || !Directory.Exists(ResultFolderTextBox.Text))
        {
            StatusTextBlock.Text = GetErrorLoc("NoFolder");
            StatusTextBlock.Foreground = Avalonia.Media.Brushes.Red;
            return;
        }

        SaveSettings();
        
        // Capture UI state on the UI thread to pass to the background thread
        var options = new GenerationOptions(
            ResultFolderTextBox.Text ?? "",
            ResultFileNameTextBox.Text ?? "IeltsAssistant",
            UsePrefixesCheckBox.IsChecked ?? true,
            GenerateDictionaryCheckBox.IsChecked ?? false, DictionaryPrefixTextBox.Text ?? "",
            GenerateIdeasCheckBox.IsChecked ?? false, IdeaPrefixTextBox.Text ?? "",
            GenerateAnswersCheckBox.IsChecked ?? false, AnswerPrefixTextBox.Text ?? ""
        );

        StatusTextBlock.Text = GetErrorLoc("Starting");
        StatusTextBlock.Foreground = Avalonia.Media.Brushes.Orange;
        StartGenerationButton.IsEnabled = false;

        try
        {
            // Now run in background using the captured options, without touching the UI
            await Task.Run(() =>
            {
                var svc = new PdfGeneratorService();
                svc.GenerateCore(options, msg => 
                {
                    Dispatcher.UIThread.InvokeAsync(() => 
                    {
                        StatusTextBlock.Text = msg;
                    });
                });
            });
            
            StatusTextBlock.Text = GetErrorLoc("Done");
            StatusTextBlock.Foreground = Avalonia.Media.Brushes.Green;
        }
        catch (Exception ex)
        {
            StatusTextBlock.Text = GetErrorLoc("Exception", ex.Message);
            StatusTextBlock.Foreground = Avalonia.Media.Brushes.Red;
        }
        finally
        {
            StartGenerationButton.IsEnabled = true;
        }
    }
}