using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyArrayTracker : MonoBehaviour {

    public static Vector3 CurrentEnemy;
    

    // usually static isn't used but just for testing
    public static float CurrentShortestDis;
    public static Vector3 ClosestEnemy;

    // list of all the enemies in the secene
    public static GameObject[] EnemyArray = new GameObject[4];
    public static List<GameObject> EnemyList = new List<GameObject>();


    void Awake()
    {
        EnemyArray = GameObject.FindGameObjectsWithTag("Attacker");

        //add enemies and player to the list
        EnemyList.AddRange(GameObject.FindGameObjectsWithTag("Attacker"));
        EnemyList.Add(GameObject.FindGameObjectWithTag("Player"));

    }

	// Use this for initialization
	void Start () {

        // find all enemys tagged attacker, will need changing in the future.
        print(EnemyArray.Length);
	
	}
	
	// Update is called once per frame
	void Update () {

        foreach (GameObject enemy in EnemyArray)
        {
            // check if set
            float Dis = 0;

            
                 Dis = Vector3.Distance(CurrentEnemy, enemy.transform.position);

            //check to see if all the players have been eliminated
            



            // if it isn't checking against iself and is less than the last distance
            if (Dis != 0 )
            {
                CurrentShortestDis = Dis;
                ClosestEnemy = enemy.transform.position;
                
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
