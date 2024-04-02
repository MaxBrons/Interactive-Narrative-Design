using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextAsset _dialogueData;
    [SerializeField] private TMP_Text _dialogueBarText;
    [SerializeField] private TextBarSettings _textBarSettings;

    private TextBar _dialogueBar;
    private DialogueTree _dialogueTree;

    private async void Start()
    {
        var data = JsonConvert.DeserializeObject<List<JObject>>(_dialogueData.text);
        var startNodeData = data[0].ConvertTo<StartNodeExportData>();
        var textNodeData = new List<TextNodeExportData>();

        for (int i = 1; i < data.Count; i++) {
            var node = data[i].ConvertTo<TextNodeExportData>();
            textNodeData.Add(node);
        }
        
        _dialogueTree = new()
        {
            StartNodeData = startNodeData,
            TextNodesData = textNodeData
        };

        _dialogueBar = new(_dialogueBarText, _textBarSettings);

        DialogueTree.DialogueLine line = await _dialogueTree.GetNextLine();

        await _dialogueBar.ShowLine(line.Header, line.Content);
    }

}