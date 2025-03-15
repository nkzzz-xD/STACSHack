using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<DialogueMessage> messages;
    private List<DialogueChoice> choices;

    private bool isTyping;

    private DialogueMessage currentMessage;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Button continueButton;

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
            // hide continue when no messages
            continueButton.gameObject.SetActive(false);

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

        // Wait until the player presses a valid choice key
        int selectedChoice = -1;
        yield return new WaitUntil(() => (selectedChoice = GetSelectedChoice()) != -1);

        // Start the next dialogue node based on the selected choice
        StartDialogue(choices[selectedChoice].nextNode);
        continueButton.gameObject.SetActive(true);
    }

    // Function to check for valid key presses and return the selected choice index
    private int GetSelectedChoice()
    {
        for (int i = 0; i < choices.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Dynamically check the right key (1, 2, 3, ...)
            {
                return i;
            }
        }
        return -1; // No valid choice selected yet
    }

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
    }
}
