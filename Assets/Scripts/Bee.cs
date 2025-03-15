using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using UnityEditor;
using System.Linq;
using UnityEngine.AI;

public class Bee : MonoBehaviour
{
    public string Name;
    public string Role;
    public string Alignment;
    public string Dialogue;
    public string Difficulty;

    private DialogueTrigger dt;


    public void Start()
    {
        dt = gameObject.GetComponent<DialogueTrigger>();
        string path;
        if (Alignment.ToLower() == "good") {
            path = $"Assets/Dialogue/Good/{GameState.difficulty.ToLower()}";
        }
        else {
            path = $"Assets/Dialogue/Bad/{GameState.difficulty.ToLower()}";
        }

        int folderLen = Directory.GetDirectories(path).Length;

        if (folderLen == 0) {
            Debug.LogWarning("Warning: Directory " + path + " is empty");
            return;
        }

        System.Random random = new System.Random();
        int fileNo = random.Next(1, folderLen + 1);

        if (Alignment.ToLower() == "good") fileNo = 4;

        string filePath = Path.Combine(path, fileNo.ToString());

        filePath = Path.Combine(filePath, "init.asset");

        DialogueNode node = AssetDatabase.LoadAssetAtPath<DialogueNode>(filePath);

        // set all placeholder names to our name
        replacePlaceholder(node);

        dt.dialogue.node = node;
    }


    private void replacePlaceholder(DialogueNode node) {
        foreach (DialogueMessage message in node.messages) {
            if (message.name == "placeholder") {
                message.name = Name;
            }
        }

        foreach (DialogueChoice choice in node.choices) {
            replacePlaceholder(choice.nextNode);
        }
    }

    // Setup the bee's data when it is spawned
    public void Setup(BeeData beeData)
    {
        Name = beeData.Name;
        Role = beeData.Role;
        Alignment = beeData.Alignment;
        Dialogue = beeData.Dialogue;
        Difficulty = beeData.Difficulty;

        

        // You can add any additional setup or behavior here
        Debug.Log("Bee " + Name + " has been spawned with role: " + Role);
    }

    public void Interact()
    {
        /*
        dt.dialogue.node.name = Name;
        dt.dialogue.node.messages

        message.sentence = "Role: " + Role + "\n" + Dialogue;
        message.colour = Color.black;

       // beeDialogue.messages[0] = message;
        */
        dt.TriggerDialogue();
    }

    void OnMouseDown()
    {
        Interact();
    }
}