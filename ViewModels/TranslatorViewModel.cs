using Google.Cloud.Translation.V2;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VouwwandImages.Extensions;
using VouwwandImages.UI;

namespace VouwwandImages.ViewModels;

public class TranslatorViewModel : ViewModel
{
    private string _title = "";
    public string Title
    {
        get { return _title; }
        set { SetProperty(ref _title, value); }
    }


    private string _text = "";
    public string Text
    {
        get { return _text; }
        set { SetProperty(ref _text, value); }
    }

    public ICommand TranslateCommand
    {
        get { return new TargetCommand(Translate); }
    }

    private async void Translate()
    {
       Text =  (await TranslateHtml("NL", Text)).Replace("<", "\n<");
    }

    public ICommand EncodeCommand
    {
        get { return new TargetCommand(Encode); }
    }

    private void Encode()
    {
        StringBuilder sb = new StringBuilder();
        foreach (string line in Text.Lines())
        {
            string trimmedLine = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmedLine))
            {

            }
            else if (trimmedLine.IndexOf("<", StringComparison.Ordinal) > 0)
            {
                sb.AppendLine(line);
            }
            else if (trimmedLine.Length < 40)
            {
                sb.AppendLine($"<h2>{trimmedLine}</h2>");
            }
            else
            {
                sb.AppendLine($"<p>{trimmedLine}</p>");
            }
        }

        Text = sb.ToString();
    }

    public ICommand ExportCommand
    {
        get { return new TargetCommand(Export); }
    }

    private void Export()
    {

    }

    #region Translate 

#endregion

    private static readonly string _jsonKeyFile = @"C:/Github/App_Data/agile-antler-359709-5c41f2406c25.json";

    public TranslationClientBuilder CreateBuilder()
    {
        var builder = new TranslationClientBuilder
        {
            CredentialsPath = _jsonKeyFile
        };
        return builder;
    }

    public async Task<string> TranslateText(string language, string text)
    {
        var builder = CreateBuilder();
        var client = await builder.BuildAsync();
        var result = await client.TranslateTextAsync(text, language);
        return result.TranslatedText;
    }

    public async Task<string> TranslateHtml(string language, string html)
    {
        var builder = CreateBuilder();
        var client = await builder.BuildAsync();
        var result = await client.TranslateHtmlAsync(html, language);
        return result.TranslatedText;
    }
}