using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour {

    public GameObject[] enemies;

    private PlayerSelector playerSelector;
    private GameObject[] players; 

	// Use this for initialization
	void Start () {
        GameObject manager = GameObject.Find("MovementManager");
        playerSelector = manager.GetComponent<PlayerSelector>();
        players = playerSelector.GetPlayers();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameObject[] GetEnemies()
    {
        return enemies; 
    }

    public void NextEnemy(GameObject enemy)
    {
        float minDist = 1000.0f;
        Vector3 target = enemy.transform.position; 

        foreach(GameObject player in players)
        {
            if(player != null)
            {
                float distance = Vector3.Distance(enemy.transform.position, player.transform.position);

                if (distance < minDist)
                {
                    minDist = distance;
                    target = player.transform.position;
                }
            }
        }
        
        enemy.GetComponent<EnemyScript>().StartCoroutine("MoveEnemy", target); 
    }
}
