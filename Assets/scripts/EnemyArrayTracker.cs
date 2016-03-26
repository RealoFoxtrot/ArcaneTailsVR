using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyArrayTracker : MonoBehaviour {

    public static Vector3 CurrentEnemy;
    

    // usually static isn't used but just for testing
    public static float CurrentShortestDis;
    public static Vector3 ClosestEnemy;
    bool PlayerWins = false;


    // list so I can add player
    public static List<GameObject> EnemyList = new List<GameObject>();


    void Awake()
    {

        //add enemies and player to the list
        EnemyList.AddRange(GameObject.FindGameObjectsWithTag("Attacker"));
        EnemyList.Add(GameObject.FindGameObjectWithTag("Player"));

    }

	// Use this for initialization
	void Start () {

        // find all enemys tagged attacker, will need changing in the future.
        print(EnemyList.Count);
	
	}
	
	// Update is called once per frame
	void Update () {
        int counter = 0;
        foreach (GameObject enemy in EnemyList)
        {
            // check if set
            float Dis = 0;
            if (enemy.tag == "Attacker" && enemy.GetComponent<SimpleAgent>().lives < 1)
            {
              
                counter++;
            }
                 Dis = Vector3.Distance(CurrentEnemy, enemy.transform.position);

            //check to see if all the players have been eliminated player wins
            if (counter == EnemyList.Count - 1)
            {
                // all enemies dead game over
                PlayerWins = true;
                
            }
           

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
