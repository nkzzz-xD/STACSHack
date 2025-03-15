using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    [TextArea(3, 10)]
    public string choice;

    public DialogueNode nextNode;  // Reference to a DialogueNode (still valid with ScriptableObject)
}