using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {

    public DialogueTrigger[] dialogues;
    public DialogueTrigger[] afterDialogues; 

    private int index;
    private TurnManager turnManager; 

	// Use this for initialization
	void Start () {
        index = 0;
        turnManager = GetComponentInParent<TurnManager>(); 
        dialogues[index].TriggerDialogue(); 
	}

    public void NextDialogue()
    {
        index++;
        if(index < dialogues.Length)
        {
            dialogues[index].TriggerDialogue(); 
        } else
        {
            index = 0; 
            turnManager.StartCoroutine("PlayerTurn"); 
        }
    }

    public DialogueTrigger[] GetAfterDialogues()
    {
        return afterDialogues; 
    }

    public void NextAfterDialogue()
    {
        index++;
        if (index < afterDialogues.Length)
        {
            afterDialogues[index].TriggerAfterDialogue();
        }
        else
        { 
            SceneManager.LoadScene("WorldMap");
        }
    }
}
