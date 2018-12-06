using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerGraveyard : MonoBehaviour {

    public Text gameOverText; 

    private List<GameObject> deadPlayers;
    private PlayerSelector playerSelector;
    private int maxDeadPlayers;
    private TurnManager turnManager; 


    private void Start()
    {
        deadPlayers = new List<GameObject>();
        gameOverText.enabled = false; 

        GameObject manager = GameObject.Find("MovementManager");
        playerSelector = manager.GetComponent<PlayerSelector>();
        maxDeadPlayers = playerSelector.GetPlayers().Length;

        turnManager = GameObject.Find("GameRoundManager").GetComponent<TurnManager>(); 
    }


    public void AddToGraveyard(GameObject player)
    {
        deadPlayers.Add(player);

        if (SceneManager.GetActiveScene().name == "Level1_VikingVillage")
        {
            GameObject.Find("GameRoundManager").GetComponent<GameStarter>().GetAfterDialogues()[0].TriggerAfterDialogue();
            turnManager.SetGameOver();
            gameOverText.enabled = true;
        } else
        {
            Destroy(player);  

            if(deadPlayers.Count == maxDeadPlayers)
            {
                turnManager.SetGameOver();
                StartCoroutine("GameOver"); 
            }
        }
    }

    public List<GameObject> GetDeadPlayers()
    {
        return deadPlayers; 
    }

    public IEnumerator GameOver()
    {
        gameOverText.enabled = true;

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene("WorldMap");
    }
}
