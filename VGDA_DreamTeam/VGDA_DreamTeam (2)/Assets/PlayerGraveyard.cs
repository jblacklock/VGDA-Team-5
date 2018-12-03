using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraveyard : MonoBehaviour {

    private List<GameObject> deadPlayers;
    private PlayerSelector playerSelector;
    private int maxDeadPlayers; 


    private void Start()
    {
        deadPlayers = new List<GameObject>();

        GameObject manager = GameObject.Find("MovementManager");
        playerSelector = manager.GetComponent<PlayerSelector>();
        maxDeadPlayers = playerSelector.GetPlayers().Length;
    }


    public void AddToGraveyard(GameObject player)
    {
        deadPlayers.Add(player);
        Destroy(player);  

        if(deadPlayers.Count == maxDeadPlayers)
        {
            Debug.Log("Game Over!"); 
        }
    }

    public List<GameObject> GetDeadPlayers()
    {
        return deadPlayers; 
    }
}
