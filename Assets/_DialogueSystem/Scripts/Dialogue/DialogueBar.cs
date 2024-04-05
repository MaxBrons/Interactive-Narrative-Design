using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DialogueBar
{
    private TMP_Text _textField;
    private TextBarSettings _settings;

    public DialogueBar(TMP_Text textBar, TextBarSettings settings)
    {
        _settings = settings;
        _textField = textBar;
    }

    public void ShowLineSync(Dialogue.DialogueLine line)
    {
        string header = (line.Header != string.Empty ? line.Header : "...");
        _textField.text = $"[{header}] " + line.Content;
    }

    public async Task ShowLine(Dialogue.DialogueLine line, CancellationToken cancellationToken)
    {
        string header = (line.Header != string.Empty ? line.Header : "...");
        _textField.text = $"[{header}] ";

        StringReader reader = new(line.Content);

        while (GameManager.IsRunning && !cancellationToken.IsCancellationRequested) {
            int readChar = reader.Read();

            if (readChar < 0)
                break;

            char character = Convert.ToChar(readChar);

            _textField.text += character;

            await Task.Delay(_settings.DialogueDelay);
        }
    }
}
