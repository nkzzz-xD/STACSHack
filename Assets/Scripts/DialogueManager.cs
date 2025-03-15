using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    private bool isTyping;

    private string currentSentence;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private AudioClip audioClip;
    private AudioSource audioSource;

    [SerializeField]
    private int soundFrequency;

    private int count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sentences = new Queue<string>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue) {
        sentences.Clear();
        animator.SetBool("IsOpen", true);
        isTyping = false;

        audioClip = dialogue.audioClip;

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        nameText.text = dialogue.name;
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        Debug.Log("press");
        if (isTyping) {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        currentSentence = sentence;
        count = 0;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            if (count % soundFrequency == 0) {
                audioSource.PlayOneShot(audioClip);
            }

            count++;
            // one char per frame
            yield return new WaitForSeconds(0.025f);
        }

        isTyping = false;
    }

    void EndDialogue() {
        Debug.Log("End of conversation");
        animator.SetBool("IsOpen", false);
    }
}
