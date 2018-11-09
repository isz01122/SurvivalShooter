using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyAttack : MonoBehaviour 
{
    public GameObject player;
    bool playerInRange = false;
    public int damage = 10;
    public float attackTime = 0.5f;//delay attack time
    float timer = 0.0f;//total time!

	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	
	void Update () 
    {
        //how to use timer!!
        //Sum Time.deltatime than, we can get total time
        timer += Time.deltaTime;

		if (playerInRange && timer >= attackTime)
        {
            //player locates range, attack
            Attack();
            timer = 0.0f;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void Attack()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);

        if (player.GetComponent<PlayerHealth>().IsDead())
        {
            GetComponent<Animator>().SetTrigger("PlayerDead");
        }
    }

}
