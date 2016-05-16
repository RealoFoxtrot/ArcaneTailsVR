using UnityEngine;
using System.Collections;

public class UICountDown : MonoBehaviour {

    public TextMesh Text;
    public SimpleAgent Agent;
    public EnemyArrayTracker tracker;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
        int countDown = (int)Agent.waitTimer;
        Text.text = "      Starting in " + countDown.ToString();

        if (Agent.waitTimer <= 0 && !EnemyArrayTracker.IsWinner)
        {

            Text.text = "";

        }
        else if(EnemyArrayTracker.IsWinner && tracker.WinningPlayer != null && tracker.WinningPlayer.tag != "Player")
        {
            Text.text = tracker.WinningPlayer.name + " is the Winner!";
        }
        if (EnemyArrayTracker.IsWinner && tracker.WinningPlayer != null && tracker.WinningPlayer.tag == "Player")
        {
            Text.text = "You are the Winner!";
        }
    }
}
