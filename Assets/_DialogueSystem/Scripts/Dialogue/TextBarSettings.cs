using UnityEngine;

[CreateAssetMenu(fileName = "Text Bar Settings", menuName = "Dialogue System/Text Bar Settings")]
public class TextBarSettings : ScriptableObject
{
    public int DialogueDelay = 250;
    public int DialogueFinnishedDelay = 750;
}
