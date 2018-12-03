using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    public int maxHP;
    public int currentHP;
    public int defense;
    public int offense;
    public int damage;
    public float movementTime; //the time this player can move 

    private bool turnEnded;
    private NavMeshAgent navAgent; //the players nav agent 
    private NavMeshHit navHit;
    private float speed; //speed of the player 
    private TurnManager turnManager; 


    void Start () {
        currentHP = maxHP;
        turnEnded = false;
        navAgent = GetComponent<NavMeshAgent>();
        speed = navAgent.speed; //set speed to default speed of nav agent 
        turnManager = GameObject.Find("GameRoundManager").GetComponent<TurnManager>();
    }


    void Update () {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        } else
        {
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
    }


    public IEnumerator MoveEnemy(Vector3 target)
    {
        navAgent.SetDestination(target); //set destination to mouse position 

        yield return new WaitForSeconds(movementTime); //wait for how long this player can move 

        navAgent.ResetPath(); //stop movement when time is up 

        TurnEnded();
        turnManager.CheckEnemyTurns(); 
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
    
}
