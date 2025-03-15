using UnityEngine;

public class Bee : MonoBehaviour
{
    public string Name;
    public string Role;
    public string Alignment;
    public string Dialogue;
    public string Difficulty;

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
}