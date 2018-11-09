using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyZomb : MonoBehaviour
{
    public Transform player;

	// Use this for initialization
	void Start ()
    {
        //dynamic 
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //this is bad way
        //public bool isDead
        //player.GetComponent<PlayerHealth>().isDead

        if (GetComponent<EnemyHealth>().IsDead())
        {
            return; // defence code!
        }
        if (player.GetComponent<PlayerHealth>().IsDead())
        {
            GetComponent<NavMeshAgent>().speed = 0f;
        }
        else
        {
            GetComponent<NavMeshAgent>().destination = player.position;
        }


    }
}
