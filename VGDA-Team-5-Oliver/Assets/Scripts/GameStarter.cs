using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    public DialogueTrigger[] dialogues;

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
            turnManager.StartCoroutine("PlayerTurn"); 
        }
    }
}
