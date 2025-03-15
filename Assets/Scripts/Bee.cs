using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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