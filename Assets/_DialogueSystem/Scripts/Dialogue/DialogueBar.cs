using System;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DialogueBar
{
    private TMP_Text _textField;
    private TextBarSettings _settings;
    private float _defaultSizeY;
    private RectTransform _parent;

    public DialogueBar(TMP_Text textBar, TextBarSettings settings)
    {
        _settings = settings;
        _textField = textBar;
        _parent = _textField.GetComponentInParent<RectTransform>();
        _defaultSizeY = _parent.sizeDelta.y;
    }

    public async Task ShowLine(DialogueLine line)
    {
        string header = (line.Header != string.Empty ? line.Header : "...");
        _textField.text = $"[{header}] ";

        StringReader reader = new(line.Content);

        while (true) {
            int readChar = reader.Read();

            if (readChar < 0)
                break;

            char character = Convert.ToChar(readChar);

            _textField.text += character;

            await Task.Delay(_settings.DialogueDelay);
        }

        await Task.Delay(_settings.DialogueFinnishedDelay);
    }
}
