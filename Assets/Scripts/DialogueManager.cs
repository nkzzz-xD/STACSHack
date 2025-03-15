using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<DialogueMessage> messages;

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
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue) {
        messages.Clear();
        animator.SetBool("IsOpen", true);
        isTyping = false;

        foreach (DialogueMessage message in dialogue.messages) {
            messages.Enqueue(message);
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
            EndDialogue();
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

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
    }
}
