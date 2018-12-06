using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

    public int maxHP;
    public int currentHP;
    public int defense;
    public int offense;
    public int damage;
    public float movementTime; //the time this player can move 
    public float collisionRadius = 0.5f; 

    private bool turnEnded;
    private NavMeshAgent navAgent; //the players nav agent 
    private NavMeshHit navHit;
    private float speed; //speed of the player 
    private TurnManager turnManager;
    private CameraManager cameraManager;
    private EnemyGraveyard graveyard;
    private bool alive;
    private bool checkCollision;
    private StatsScript stats; 


    void Start () {
        currentHP = maxHP;
        turnEnded = false;
        alive = true;
        checkCollision = false; 
        navAgent = GetComponent<NavMeshAgent>();
        speed = navAgent.speed; //set speed to default speed of nav agent 
        turnManager = GameObject.Find("GameRoundManager").GetComponent<TurnManager>();
        cameraManager = turnManager.GetComponent<CameraManager>();

        graveyard = GameObject.Find("Graveyard").GetComponent<EnemyGraveyard>();

        stats = GameObject.Find("Stats").GetComponent<StatsScript>(); 
    }


    void Update () {

        //check the areas the player hit 
        navAgent.SamplePathPosition(-1, 0.0f, out navHit); //when player is moving 

        if (navHit.mask == 8) //NavMeshArea Mud
        {
            navAgent.speed = speed * 0.5f; //set speed to half the speed 
        }
        else
        {
            navAgent.speed = speed;
        }
        
    }


    public IEnumerator MoveEnemy(Vector3 target)
    {
        cameraManager.ChangeMovable();
        cameraManager.StartCoroutine("FollowEnemy", gameObject); 

        navAgent.SetDestination(target); //set destination to mouse position 
        yield return new WaitForSeconds(movementTime); //wait for how long this player can move 
        navAgent.ResetPath(); //stop movement when time is up 

        cameraManager.ChangeMovable(); //make the camera stop following the player
        cameraManager.ResetCamera();

        CheckCollision();

        TurnEnded();
        turnManager.CheckEnemyTurns();
    }

    public void CheckCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionRadius, LayerMask.GetMask("Player")); 
        if (colliders.Length > 0)
        {
            Debug.Log("Collision with player detected");

            PlayerScript player = colliders[0].gameObject.GetComponent<PlayerScript>();
            bool fighting = true; 
            while(fighting)
            {
                if (defense < player.GetOffense())
                {
                    Debug.Log("player does damage");
                    currentHP = currentHP - player.GetDamage();
                }

                if (currentHP <= 0)
                {
                    Debug.Log("Enemy dies");
                    fighting = false; 
                    graveyard.AddToGraveyard(gameObject);
                }

                if (offense > player.GetDefense())
                {
                    Debug.Log("Enemy does damage");
                    player.SetCurrentHP(damage);
                }

                if (player.GetCurrentHP() <= 0)
                {
                    Debug.Log("Player dies");
                    fighting = false; 
                    player.Die();
                }
            }
        }

        //checkCollision = true; 
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (checkCollision)
        {
            if (other.GetComponent<PlayerScript>() != null)
            {
                Debug.Log("Enemy engages FIGHT!"); 

                PlayerScript player = other.GetComponent<PlayerScript>(); 

                if (defense < player.GetOffense())
                {
                    Debug.Log("player does damage");
                    currentHP = currentHP - player.GetDamage();
                }

                if (currentHP <= 0)
                {
                    Debug.Log("Enemy dies"); 
                    checkCollision = false;
                    graveyard.AddToGraveyard(gameObject);
                    Debug.Log("ending turn"); 
                    TurnEnded();
                    turnManager.CheckEnemyTurns();
                }


                if (offense > player.GetDefense())
                {
                    Debug.Log("Enemy does damage");
                    player.SetCurrentHP(damage);
                }

                if (player.GetCurrentHP() <= 0)
                {
                    Debug.Log("Player dies");
                    checkCollision = false;
                    player.Die();
                    TurnEnded();
                    turnManager.CheckEnemyTurns();
                }

            } else
            {
                checkCollision = false;
                TurnEnded();
                turnManager.CheckEnemyTurns();
            }

        }
    }*/

    private void OnMouseOver()
    {
        //115, 55, 55, 230
        stats.DisplayStats(maxHP, currentHP, defense, offense, damage);
        stats.GetComponent<Image>().color = new Color32(115, 55, 55, 230);
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
