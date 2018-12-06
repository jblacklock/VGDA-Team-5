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
    private int firstLivingEnemy;
    private bool gameOver; 

	// Use this for initialization
	void Start () {
        gameOver = false; 
        GameObject manager = GameObject.Find("MovementManager"); 
        playerSelector = manager.GetComponent<PlayerSelector>();
        enemySelector = manager.GetComponent<EnemySelector>(); 
        turnText.enabled = false;
        gameRound = 0;
        enemies = enemySelector.GetEnemies();
        players = playerSelector.GetPlayers();

        //StartCoroutine("PlayerTurn");
    }


    public IEnumerator PlayerTurn()
    {
        if(!gameOver)
        {
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
    }


    public IEnumerator EnemyTurn()
    {
        if(!gameOver)
        {
            bool firstFound = false; 
        
            for(int i=0; i<enemies.Length; i++)
            {
                if(enemies[i] != null)
                {
                    enemies[i].GetComponent<EnemyScript>().TurnBegin();

                    if(!firstFound)
                    {
                        firstLivingEnemy = i;
                        firstFound = true;
                    }
                } 

            }

            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<EnemyScript>().TurnBegin();
                } else
                {
                
                }
            }

            gameRound++;
            playerSelector.SetSelectable(false);
            turnText.text = "Enemy turn";
            turnText.enabled = true;

            yield return new WaitForSeconds(2);

            turnText.enabled = false;
            enemySelector.NextEnemy(enemies[firstLivingEnemy]); 
        }
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
            if(enemy != null)
            {
                bool turnEnded = enemy.GetComponent<EnemyScript>().GetTurnEnded();
                if (!turnEnded)
                {
                    enemySelector.NextEnemy(enemy);
                    return; 
                }
            }
        }

        StartCoroutine("PlayerTurn"); 
    }

    public void SetGameOver()
    {
        gameOver = true; 
    }
}
