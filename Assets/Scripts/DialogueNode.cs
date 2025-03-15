using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    public DialogueMessage[] messages;  // Multiple messages before choices

    public DialogueChoice[] choices;    // Possible choices the player can make
}