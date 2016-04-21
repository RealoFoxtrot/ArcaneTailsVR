using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyArrayTracker : MonoBehaviour {

    public static Vector3 CurrentEnemy;
    

    // usually static isn't used but just for testing
    public static float CurrentShortestDis;
    public static Vector3 ClosestEnemy;
    public static bool IsWinner = false;
    bool PlayerWins = false;


    // list so I can add player
    public static List<GameObject> EnemyList = new List<GameObject>();


    void Awake()
    {

        //add enemies and player to the list
        EnemyList.Clear(); // this fixed the problems I am having with the enemiews not bieng there when the level is reloaded.
        EnemyList.AddRange(GameObject.FindGameObjectsWithTag("Attacker"));
        EnemyList.Add(GameObject.FindGameObjectWithTag("Player"));

        IsWinner = false;// make sure the Iswinner is false at startup

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
            //just check all in the list not just the enemies.
                if (enemy.tag == "Attacker" && enemy.GetComponent<SimpleAgent>().lives < 1 
                 || enemy.tag == "Player" && enemy.GetComponent<PinballMovement>().lives < 1)
                {

                    counter++;
                }
            

                 Dis = Vector3.Distance(CurrentEnemy, enemy.transform.position);

           
           

            // if it isn't checking against iself and is less than the last distance
            if (Dis != 0 )
            {
                CurrentShortestDis = Dis;
                ClosestEnemy = enemy.transform.position;
                
            }
            
        }

        //check after all enemies have been gone through.
        //check to see if all the players have been eliminated player wins
        if (counter == EnemyList.Count - 1)
        {
            //someone isn't dead. They Win!
            // apply winning state. Do Celebration after.
            IsWinner = true;


        }



        if (counter == EnemyList.Count)
        {
            // all are dead. Eh? how did that happen? Throw an error.
            //Debug.LogError("All players and enemies are dead, that isn't supposed to happen.");

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
