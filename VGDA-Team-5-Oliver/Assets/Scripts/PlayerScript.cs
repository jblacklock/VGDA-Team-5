using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    private bool checkCollision;
    private StatsScript stats; 

    public int maxHP;
    public int currentHP;
    public int defense;
    public int offense;
    public int damage;
    public float collisionRadius = 0.5f; 
    
	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        speed = navAgent.speed; //set speed to default speed of nav agent 
        selected = false;
        selector = manager.GetComponent<PlayerSelector>();

        turnManager = GameObject.Find("GameRoundManager").GetComponent<TurnManager>(); 
        cameraManager = turnManager.GetComponent<CameraManager>(); 

        defaultMaterial = GetComponent<Renderer>().material;
        turnEnded = false;
        checkCollision = false; 

        graveyard = GameObject.Find("Graveyard").GetComponent<PlayerGraveyard>();

        stats = GameObject.Find("Stats").GetComponent<StatsScript>(); 

        currentHP = maxHP; 
	}
	
	void Update () {
        
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
        cameraManager.ResetCamera(); 

        GetComponent<Renderer>().material = defaultMaterial; //return to default material 

        selector.DeactivateMovementCircle(); 
        selector.SetSelectable(true);  //make it possible to select other players 

        CheckCollision(); 

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

    public void CheckCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius, LayerMask.GetMask("Enemy"));
        if (colliders.Length > 0)
        {
            Debug.Log("Collision with enemy detected");

            EnemyScript enemy = colliders[0].gameObject.GetComponent<EnemyScript>();
            bool fighting = true; 

            while(fighting)
            {
                if (defense < enemy.GetOffense())
                {
                    Debug.Log("enemy does damage");
                    currentHP = currentHP - enemy.GetDamage();
                }

                if (currentHP <= 0)
                {
                    Debug.Log("Player dies");
                    fighting = false; 
                    graveyard.AddToGraveyard(gameObject);
                }

                if (offense > enemy.GetDefense())
                {
                    Debug.Log("Player does damage");
                    enemy.SetCurrentHP(damage);
                }

                if (enemy.GetCurrentHP() <= 0)
                {
                    Debug.Log("Enemy dies");
                    fighting = false; 
                    enemy.Die();
                }
            }
        }

        //checkCollision = true; 
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other); 
        if(checkCollision)
        {
            navAgent.ResetPath();
            selector.SetSelectable(false); 

            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Player engages FIGHT!"); 
                EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();
            
                if (defense < enemy.GetOffense())
                {
                    Debug.Log("Enemy does damage"); 
                    currentHP = currentHP - enemy.GetDamage();
                }    

                if(currentHP <= 0)
                {
                    Debug.Log("Player dies");
                    checkCollision = false; 
                    graveyard.AddToGraveyard(gameObject); 
                }


                if (offense > enemy.GetDefense())
                {
                    Debug.Log("Player does damage"); 
                    enemy.SetCurrentHP(damage); 
                }

                if(enemy.GetCurrentHP() <= 0)
                {
                    Debug.Log("Enemy dies");
                    checkCollision = false;
                    enemy.Die(); 
                }

            } else
            {
                checkCollision = false; 
            }
        
        }
    }*/

    private void OnMouseOver()
    {
        //70, 80, 130, 230
        stats.DisplayStats(maxHP, currentHP, defense, offense, damage); 
        stats.GetComponent<Image>().color = new Color32(70, 80, 130, 230); 
    }

    private void OnMouseExit()
    {
        stats.HideStats(); 
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

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public int GetMaxHP()
    {
        return maxHP; 
    }

    public void SetCurrentHP(int damage)
    {
        currentHP = currentHP - damage;
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

    public void Die()
    {
        graveyard.AddToGraveyard(gameObject);
    }

}

