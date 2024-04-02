using System.Collections.Generic;
using System.Threading.Tasks;

public class DialogueTree
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

    public StartNodeExportData StartNodeData { get; set; }
    public List<TextNodeExportData> TextNodesData { get; set; }

    private int _index = -1;

    public async Task<DialogueLine> GetNextLine()
    {
        return await Task.Run(() => {
            if (_index < 0)
                return new DialogueLine("...", "INTERACTIVE NARRATIVE DESIGN | DIALOOG - Erik, Owen & Max");

            if (_index < TextNodesData.Count)
                return new DialogueLine(TextNodesData[_index].Title, TextNodesData[_index].Content);

            _index++;
            return default;
        });
    }
}
