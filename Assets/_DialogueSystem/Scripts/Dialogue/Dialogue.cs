using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Dialogue
{
    public struct DialogueLine
    {
        public string Header;
        public string Content;

        public DialogueLine(string header, string content)
        {
            Header = header;
            Content = content;
        }
    }

    public delegate void DialogueChoiceEvent(IEnumerable<string> choices);
    public event DialogueChoiceEvent OnChoiceRequested;

    public bool EndReached => !GetConnections(_currentNode).Any() || !GameManager.IsRunning;

    private DialogueSource _source;
    private INodeData _currentNode;
    private List<TextNodeExportData> _currentNodeChoices;
    private bool _firstNodeShown;

    public Dialogue(DialogueSource source)
    {
        _source = source;
        _currentNode = _source.StartNode;
    }

    public async Task<DialogueLine> GetNextLine()
    {
        if (!_firstNodeShown) {
            _currentNodeChoices = GetConnections(_currentNode);

            _firstNodeShown = true;
            return new("...", "Interactive narrative gemaakt door Erik, Owen & Max");
        }

        if (_currentNodeChoices.Count > 1) {
            _currentNode = null;

            OnChoiceRequested?.Invoke(_currentNodeChoices.ConvertAll(x => x.ToDialogueLine()));
            await Tools.WaitFor(() => _currentNode != null, GameManager.GameCancellationToken);
        }
        else {
            _currentNode = _currentNodeChoices[0];
        }

        _currentNodeChoices = GetConnections(_currentNode);

        TextNodeExportData data = _currentNode as TextNodeExportData;
        return new(_currentNode.Title, data.Content);
    }

    public void MakeChoice(int index)
    {
        _currentNode = _currentNodeChoices.ElementAt(index);
    }

    public DialogueLine GetLastLine()
    {
        var lastLine = _source.DialogueLines.Last();

        return new(lastLine.Title, lastLine.Content);
    }

    private List<TextNodeExportData> GetConnections(INodeData node)
    {
        if(node == null) {
            Debug.LogError("Could not find node while getting connections: " + node);
            return null;
        }
        if (node.Connections.Count < 0)
            return null;

        return node.Connections.ConvertAll(graphConnection => {
            return _source.DialogueLines.Find(textNodeData => textNodeData.ID == ulong.Parse(graphConnection.ToNode));
        });
    }
}