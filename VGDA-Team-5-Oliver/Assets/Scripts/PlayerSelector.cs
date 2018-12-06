using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script makes it possible to select among the different playable characters 
public class PlayerSelector : MonoBehaviour {

    public GameObject[] players; //array of every playable character
    public Camera cam; //the main camera
    public Material selectedMaterial; //the material showing a character is selected 

    private bool selectable; //check if the player is in motion 
    private GameObject movementCircle;
    private CircleRotator rotator; 

    
	void Start () {
        selectable = true;
        movementCircle = GameObject.Find("MovementCircle");
        rotator = movementCircle.GetComponent<CircleRotator>(); 
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
                    //if hit is Player AND the selected player's turn is not over
                    if(hit.transform.tag == "Player" && !players[0].GetComponent<PlayerScript>().GetTurnEnded()) 
                    {
                        StartCoroutine("SelectPlayer", 0); //select that player 
                    } else if(hit.transform.tag == "Player2" && !players[1].GetComponent<PlayerScript>().GetTurnEnded())
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
        movementCircle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.25f, player.transform.position.z - 0.25f); 
        movementCircle.transform.localScale = new Vector3(movementArea, movementArea, 1f); 
        movementCircle.SetActive(true);
        rotator.Rotate();
    }


    public void DeactivateMovementCircle()
    {
        rotator.Rotate(); 
        movementCircle.SetActive(false);
    } 

    public GameObject[] GetPlayers()
    {
        return players; 
    }
}
