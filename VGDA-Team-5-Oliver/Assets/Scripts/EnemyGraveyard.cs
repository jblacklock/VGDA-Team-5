using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyGraveyard : MonoBehaviour
{

    public Text winText;

    private List<GameObject> deadEnemies;
    private EnemySelector enemySelector;
    private int maxDeadEnemies;
    private TurnManager turnManager; 


    private void Start()
    {
        deadEnemies = new List<GameObject>();
        winText.enabled = false;

        GameObject manager = GameObject.Find("MovementManager");
        enemySelector = manager.GetComponent<EnemySelector>();
        maxDeadEnemies = enemySelector.GetEnemies().Length;

        turnManager = GameObject.Find("GameRoundManager").GetComponent<TurnManager>(); 
    }


    public void AddToGraveyard(GameObject enemy)
    {
        deadEnemies.Add(enemy);
        Destroy(enemy);
        Debug.Log("enemy added to graveyard"); 
        if (deadEnemies.Count == maxDeadEnemies)
        {
            turnManager.SetGameOver();
            StartCoroutine("GameOver"); 
        }
    }

    public List<GameObject> GetDeadEnemies()
    {
        return deadEnemies;
    }

    public IEnumerator GameOver()
    {
        winText.enabled = true;

        yield return new WaitForSeconds(4);
             
        Data.Instance().DeactivateLastPortal(); 

        SceneManager.LoadScene("WorldMap"); 
    }
}
