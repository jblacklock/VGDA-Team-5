using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {

    public Text turnText;  

    private PlayerSelector playerSelector;
    private EnemySelector enemySelector; 
    private int gameRound;
    private GameObject[] enemies;
    private GameObject[] players;

	// Use this for initialization
	void Start () {
        GameObject manager = GameObject.Find("MovementManager"); 
        playerSelector = manager.GetComponent<PlayerSelector>();
        enemySelector = manager.GetComponent<EnemySelector>(); 
        turnText.enabled = false;
        gameRound = 0;
        StartCoroutine("PlayerTurn");
        enemies = enemySelector.GetEnemies();
        players = playerSelector.GetPlayers();
    }


    public IEnumerator PlayerTurn()
    {
        Debug.Log(GameObject.Find("Graveyard").GetComponent<PlayerGraveyard>().GetDeadPlayers()); 

        gameRound++;
        playerSelector.SetSelectable(false); 
        turnText.text = "Player turn";
        turnText.enabled = true;

        yield return new WaitForSeconds(2);

        turnText.enabled = false; 
        playerSelector.SetSelectable(true);

        foreach (GameObject player in players)
        {
            if(player != null)
            {
                player.GetComponent<PlayerScript>().TurnBegin(); 
            }
        }
    }


    public IEnumerator EnemyTurn()
    {
        gameRound++;
        playerSelector.SetSelectable(false);
        turnText.text = "Enemy turn";
        turnText.enabled = true;

        yield return new WaitForSeconds(2);

        turnText.enabled = false;
        
        enemySelector.NextEnemy(enemies[0]); 
    }


    public void CheckPlayerTurns()
    {
        foreach(GameObject player in players)
        {
            if(player != null)
            {
                bool turnEnded = player.GetComponent<PlayerScript>().GetTurnEnded();
                if (!turnEnded) {
                    return; 
                }
            }
        }

        StartCoroutine("EnemyTurn"); 
    }


    public void CheckEnemyTurns()
    {
        foreach (GameObject enemy in enemies)
        {
            bool turnEnded = enemy.GetComponent<EnemyScript>().GetTurnEnded();
            if (!turnEnded)
            {
                enemySelector.NextEnemy(enemy);
                return; 
            }
        }

        StartCoroutine("PlayerTurn"); 
    }
}
