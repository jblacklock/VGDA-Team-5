using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour {
    
    public Camera cam; //main camera
    public GameObject manager; //PlayerManager object
    public float movementTime; //the time this player can move  
    public float movementArea = 50f; 

    private NavMeshAgent navAgent; //the players nav agent 
    private NavMeshHit navHit;
    private float speed; //speed of the player 
    private bool selected; //whether the player is selected
    private PlayerSelector selector; //the script Selector, to control selected players 
    private Material defaultMaterial; //the default material 
    private bool turnEnded;
    private TurnManager turnManager;
    private PlayerGraveyard graveyard;
    private CameraManager cameraManager; 

    public int maxHP;
    public int currentHP;
    public int defense;
    public int offense;
    public int damage;
    
	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        speed = navAgent.speed; //set speed to default speed of nav agent 
        selected = false;
        selector = manager.GetComponent<PlayerSelector>();
        turnManager = GameObject.Find("GameRoundManager").GetComponent<TurnManager>(); 
        defaultMaterial = GetComponent<Renderer>().material;
        turnEnded = false;
        cameraManager = GameObject.FindGameObjectWithTag("GameRoundManager").GetComponent<CameraManager>(); 

        graveyard = GameObject.Find("Graveyard").GetComponent<PlayerGraveyard>(); 

        currentHP = maxHP; 
	}
	
	void Update () {

        if (currentHP <= 0) {
            graveyard.AddToGraveyard(gameObject);  
        } else
        {
            navAgent.SamplePathPosition(-1, 0.0f, out navHit); //when player is moving 

            //check the areas the player hit 
            if (navHit.mask == 8) //NavMeshArea Mud
            {
                navAgent.speed = speed * 0.5f; //set speed to half the speed 
            }
            else
            {
                navAgent.speed = speed;
            }
        }

        if(selected)
        {
            if(Input.GetMouseButtonDown(0)) //if left mouse button is pressed 
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); //get mouse position 
                RaycastHit hit; 

                if(Physics.Raycast(ray, out hit)) //if raycast hits something 
                {

                    //TO DO...
                    //so you can select another player instead 
                    //or so you can select multiple players 
                    
                    selector.SetSelectable(false);  //not possible to select other players 
                    
                    cameraManager.ChangeMovable(); //make the camera able to follow the player
                    cameraManager.StartCoroutine("FollowPlayer", gameObject); 

                    StartCoroutine("MovePlayer", hit.point); //begin movement 
                }
            }
        }

    }
    
    public IEnumerator MovePlayer(Vector3 target)
    {
        selected = false; //unselect player 

        navAgent.SetDestination(target); //set destination to mouse position 

        yield return new WaitForSeconds(movementTime); //wait for how long this player can move 

        navAgent.ResetPath(); //stop movement when time is up 
        
        cameraManager.ChangeMovable(); //make the camera stop following the player

        GetComponent<Renderer>().material = defaultMaterial; //return to default material 

        selector.DeactivateMovementCircle(); 
        selector.SetSelectable(true);  //make it possible to select other players 

        TurnEnded();
        turnManager.CheckPlayerTurns(); 
    }

    public void SelectPlayer()
    {
        selected = true; 
    }

    public float GetMovementArea()
    {
        return movementArea; 
    }


    private void OnTriggerStay(Collider other)
    {

        navAgent.ResetPath();
        selector.SetSelectable(false); 

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();
            
            if (defense < enemy.GetOffense())
            {
                currentHP = currentHP - enemy.GetDamage();
            }

            if (offense > enemy.GetDefense())
            {
                enemy.SetCurrentHP(damage); 
            }

        };
        
    }


    public int GetOffense()
    {
        return offense; 
    }

    public int GetDamage()
    {
        return damage; 
    }

    public int GetDefense()
    {
        return defense; 
    }

    public bool GetTurnEnded()
    {
        return turnEnded; 
    }

    public void TurnEnded()
    {
        turnEnded = true; 
    }

    public void TurnBegin()
    {
        turnEnded = false; 
    }
    
}

