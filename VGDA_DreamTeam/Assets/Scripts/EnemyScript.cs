using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public int maxHP;
    public int currentHP;
    public int defense;
    public int offense;
    public int damage;
    

    void Start () {
        currentHP = maxHP; 
	}
	

	void Update () {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
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
}
