using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Image personImage; 

    public Animator animator;

    private Queue<string> sentences;
    private GameStarter gameStarter; 

    // Use this for initialization
    void Start()
    {
        gameStarter = GameObject.Find("GameRoundManager").GetComponent<GameStarter>(); 
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences = new Queue<string>();
        animator.SetBool("IsOpen", true);

        Debug.Log("starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;
        personImage.sprite = dialogue.person; 

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        gameStarter.NextDialogue(); 
    }
    

}