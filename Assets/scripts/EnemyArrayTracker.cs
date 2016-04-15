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
        EnemyList.Clear(); // this fixed the problems I am having with the enemiews no9t bieng there when the level is reloaded.
        EnemyList.AddRange(GameObject.FindGameObjectsWithTag("Attacker"));
        EnemyList.Add(GameObject.FindGameObjectWithTag("Player"));
        

    }

	// Use this for initialization
	void Start () {

        // find all enemys tagged attacker, will need changing in the future.


        
        print(EnemyList.Count);
        for (int i = 0; i < EnemyList.Count; i++)
        {

            print(EnemyList[i].name);

        }
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
               //someone isn't dead.
                
            }

            if (counter == EnemyList.Count)
            {
                // all are dead. Eh? how did that happen?

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
