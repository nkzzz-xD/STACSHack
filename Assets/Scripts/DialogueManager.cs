using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<DialogueMessage> messages;
    private List<DialogueChoice> choices;

    private bool isTyping;

    private DialogueMessage currentMessage;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private AudioSource audioSource;
    public AudioClip defaultAudio;

    [SerializeField]
    private int soundFrequency;

    private int count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        messages = new Queue<DialogueMessage>();
        choices = new List<DialogueChoice>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void StartDialogue(DialogueNode dialogue) {
        messages.Clear();
        choices.Clear();

        animator.SetBool("IsOpen", true);
        isTyping = false;

        foreach (DialogueMessage message in dialogue.messages) {
            messages.Enqueue(message);
        }

        foreach (DialogueChoice choice in dialogue.choices) {
            choices.Add(choice);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (isTyping) {
            StopAllCoroutines();
            dialogueText.text = currentMessage.sentence;
            isTyping = false;
            return;
        }

        if (messages.Count == 0) {
            if (choices.Count == 0) {
                EndDialogue();
                return;
            }

            StartCoroutine(ShowChoices());
            return;
        }

        DialogueMessage message = messages.Dequeue();
        currentMessage = message;
        count = 0;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(message));
    }

    IEnumerator TypeSentence(DialogueMessage message) {
        isTyping = true;
        dialogueText.text = "";
        nameText.text = message.name;
        dialogueText.color = message.colour;
        dialogueText.faceColor = message.colour;
        nameText.color = message.colour;
        nameText.faceColor = message.colour;

        foreach (char letter in message.sentence.ToCharArray()) {
            dialogueText.text += letter;
            if (count % soundFrequency == 0) {
                if (message.audioClip == null) {
                    audioSource.PlayOneShot(defaultAudio);
                }
                else {
                    audioSource.PlayOneShot(message.audioClip);
                }
            }

            count++;
            // one char per frame
            yield return new WaitForSeconds(0.025f);
        }

        isTyping = false;
    }

    IEnumerator ShowChoices() {
        dialogueText.text = choices[0].choice + " - press 1";

        for (int i = 1; i < choices.Count; i++) {
            dialogueText.text += "\n";
            dialogueText.text += choices[i].choice + " - press " + (i + 1).ToString();
        }

        yield return new WaitUntil(() => IsValidChoiceInput());

        // Loop through the number of choices and check for corresponding key inputs (1, 2, 3, etc.)
        for (int i = 0; i < choices.Count; i++)
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (i + 1))))
            {
                StartDialogue(choices[i].nextNode);
            }
        }
    }

    // Function to check if the player presses a valid key corresponding to the choices
    private bool IsValidChoiceInput()
    {
        // Loop through the number of choices and check for corresponding key inputs (1, 2, 3, etc.)
        for (int i = 0; i < choices.Count; i++)
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (i + 1))))
            {
                return true; // A valid key corresponding to a choice was pressed
            }
        }
        return false; // No valid key was pressed yet
    }

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
    }
}
