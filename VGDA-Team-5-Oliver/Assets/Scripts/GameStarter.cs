using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {

    public DialogueTrigger[] dialogues;
    public DialogueTrigger[] afterDialogues;
    public Animator blackFade;

    private int index;
    private TurnManager turnManager;
    private PlayerSelector playerSelector; 

	// Use this for initialization
	void Start () {
        GameObject manager = GameObject.Find("MovementManager");
        playerSelector = manager.GetComponent<PlayerSelector>();
        playerSelector.SetSelectable(false); 

        index = 0;
        turnManager = GetComponentInParent<TurnManager>();

        StartCoroutine("FadeIn");
	}

    public IEnumerator FadeIn()
    {
        blackFade.SetTrigger("FadeIn");

        yield return new WaitForSeconds(2);

        GameObject.Find("BlackFade").GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(2); 

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
            StartCoroutine("FadeOut"); 
        }
    }

    public IEnumerator FadeOut()
    {
        GameObject.Find("BlackFade").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        blackFade.SetTrigger("FadeOut");

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene("WorldMap");
    }
}
