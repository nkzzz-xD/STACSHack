using UnityEngine;

[System.Serializable]
public class DialogueMessage
{
    [TextArea(3, 10)]
    public string sentence;

    public string name;

    [Header("Audio")]
    public AudioClip audioClip;

    public Color colour;
}
