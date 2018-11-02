﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script makes it possible to select among the different playable characters 
public class Selector : MonoBehaviour {

    public GameObject[] players; //array of every playable character
    public Camera cam; //the main camera
    public Material selectedMaterial; //the material showing a character is selected 

    private bool selectable; //check if the player is in motion 
    private GameObject movementCircle; 

    
	void Start () {
        selectable = true;
        movementCircle = GameObject.Find("MovementCircle");
        movementCircle.SetActive(false); 
    }

	
	void Update () {
        if(selectable) //if player is not in motion
        {
            if (Input.GetMouseButtonDown(0)) //if left mouse button is pressed
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); //get position of mouse
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) //if raycast hits something in the game
                {
                    if(hit.transform.tag == "Player") //if hit is Player
                    {
                        StartCoroutine("SelectPlayer", 0); //select that player 
                    } else if(hit.transform.tag == "Player2")
                    {
                        StartCoroutine("SelectPlayer", 1); 
                    }
                }
            }
        }
    }


    //this method selects a specific player 
    public IEnumerator SelectPlayer(int index)
    {
        yield return new WaitForEndOfFrame(); //at the end of the frame, the mouse press is reset  
        PlayerScript player = players[index].GetComponent<PlayerScript>(); //get the playerScript from the specific player 

        SetMovementCircle(players[index], player.GetMovementArea()); 

        players[index].GetComponent<Renderer>().material = selectedMaterial;  //set material 
        player.SelectPlayer(); //select the player
    }


    public void SetSelectable(bool b)
    {
        selectable = b; 
    }


    public void SetMovementCircle(GameObject player, float movementArea)
    {
        movementCircle.transform.position = player.transform.position;
        movementCircle.transform.localScale = new Vector3(movementArea, movementArea, 1f);
        movementCircle.SetActive(true);
    }


    public void DeactivateMovementCircle()
    {
        movementCircle.SetActive(false); 
    } 
}
