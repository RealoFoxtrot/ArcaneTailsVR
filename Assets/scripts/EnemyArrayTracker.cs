﻿using UnityEngine;
using System.Collections;

public class EnemyArrayTracker : MonoBehaviour {

    public static Vector3 CurrentEnemy;
    

    // usually static isn't used but just for testing
    public static float CurrentShortestDis;
    public static Vector3 ClosestEnemy;

    // list of all the enemies in the secene
    public GameObject[] EnemyArray;
    

	// Use this for initialization
	void Start () {

        // find all enemys tagged attacker, will need changing in the future.
        EnemyArray = GameObject.FindGameObjectsWithTag("Attacker");
	
	}
	
	// Update is called once per frame
	void Update () {

        foreach (GameObject enemy in EnemyArray)
        {
            // check if set
            float Dis = 0;

            
                 Dis = Vector3.Distance(CurrentEnemy, enemy.transform.position);
           




            // if it isn't checking against iself and is less than the last distance
            if (Dis != 0 )
            {
                CurrentShortestDis = Dis;
                ClosestEnemy = enemy.transform.position;
               // print(ClosestEnemy);
            }
            
        }

     



	
	}


    Vector3 getCurrentEnemyPosition()
    {

        return CurrentEnemy;
    }


    void SetCurrentenemyPosition(Vector3 pos)
    {

        CurrentEnemy = pos;

    }

}