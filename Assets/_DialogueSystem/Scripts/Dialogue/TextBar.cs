using System.IO;
using System.Threading.Tasks;
using TMPro;

public class TextBar
{
    private TMP_Text _textBar;
    private TextBarSettings _settings;

    public TextBar(TMP_Text textBar, TextBarSettings settings)
    {
        _settings = settings;
        _textBar = textBar;
    }

    public async Task ShowLine(string header, string text)
    {
        _textBar.text = $"[{header}] ";

        StringReader reader = new(text);
        int character = reader.Read();

        while (character != -1) {
            _textBar.text += character;

            await Task.Delay(_settings.DialogueDelay);
        }

        await Task.Delay(_settings.DialogueFinnishedDelay);

        _textBar.text = string.Empty;
    }
}
