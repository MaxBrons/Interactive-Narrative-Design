using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSource
{
    public StartNodeExportData StartNode { get; }
    public List<TextNodeExportData> DialogueLines { get; } = new();

    public DialogueSource(string dialogue)
    {
        var json = JsonConvert.DeserializeObject<List<JObject>>(dialogue);

        for (int i = 0; i < json.Count; i++) {
            if (i == 0) {
                StartNode = json[i].ToObject<StartNodeExportData>();
                continue;
            }

            DialogueLines.Add(json[i].ToObject<TextNodeExportData>());
        }
    }
}
