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
    private Selector selector; //the script Selector, to control selected players 
    private Material defaultMaterial; //the default material 

    public int maxHP;
    public int currentHP;
    public int defense;
    public int offense;
    public int damage;
    
	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        speed = navAgent.speed; //set speed to default speed of nav agent 
        selected = false;
        selector = manager.GetComponent<Selector>();
        defaultMaterial = GetComponent<Renderer>().material;

        currentHP = maxHP; 
	}
	
	void Update () {

        if (currentHP <= 0) {
            Destroy(gameObject);
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
                    StartCoroutine("MovePlayer", hit.point); //begin movement 
                }
            }
        }

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
    
    public IEnumerator MovePlayer(Vector3 target)
    {
        selected = false; //unselect player 

        navAgent.SetDestination(target); //set destination to mouse position 

        yield return new WaitForSeconds(movementTime); //wait for how long this player can move 

        navAgent.ResetPath(); //stop movement when time is up 

        GetComponent<Renderer>().material = defaultMaterial; //return to default material 

        selector.DeactivateMovementCircle(); 
        selector.SetSelectable(true);  //make it possible to select other players 
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
}

