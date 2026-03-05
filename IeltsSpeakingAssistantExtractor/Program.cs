using Avalonia;
using System;

namespace IeltsSpeakingAssistantExtractor;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            if (args.Length > 0 && args[0] == "--cli")
        {
            Console.WriteLine("Starting IELTS Speaking Assistant Extractor in CLI mode...");
            string outPath = args.Length > 1 ? args[1] : System.IO.Path.Combine(Environment.CurrentDirectory, "Results");
            
            var options = new GenerationOptions(
                ResultFolder: outPath,
                ResultFileName: $"IeltsAssistant_{DateTime.Now:yyyy-MM-dd}",
                UsePrefixes: true,
                IsDictionary: true, DictionaryPrefix: "-dict",
                IsIdeas: true, IdeaPrefix: "-ideas",
                IsAnswers: true, AnswerPrefix: "-answers"
            );
            
            try
            {
                var svc = new PdfGeneratorService();
                svc.GenerateCore(options, msg => Console.WriteLine(msg));
                Console.WriteLine("Done CLI generation.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during CLI generation: " + ex.ToString());
                System.IO.File.WriteAllText("cli_error.txt", ex.ToString());
            }
            return;
        }

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            System.IO.File.WriteAllText("crash.log", ex.ToString());
            throw;
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
