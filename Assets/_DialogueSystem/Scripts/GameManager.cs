using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextAsset _dialogueData;
    [SerializeField] private TMP_Text _dialogueBarText;
    [SerializeField] private TextBarSettings _textBarSettings;

    private DialogueBar _dialogueBar;
    private DialogueTree _dialogueTree;

    private async void Start()
    {
        var dialogue = JsonConvert.DeserializeObject<List<JObject>>(_dialogueData.text);
        var startNode = dialogue.First().ToObject<StartNodeExportData>();

        _dialogueTree = new(startNode);
        _dialogueBar = new(_dialogueBarText, _textBarSettings);

        for (int i = 1; i < dialogue.Count; i++) {
            _dialogueTree.Stack(dialogue[i].ToObject<TextNodeExportData>());
        }

        await StartDialogue();
    }

    private async Task StartDialogue()
    {
        while (!_dialogueTree.EndReached) {
            var line = _dialogueTree.Pop();

            await _dialogueBar.ShowLine(line);
        }

        var end = _dialogueTree.Pop();

        await _dialogueBar.ShowLine(end);
    }
}