using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoad : MonoBehaviour {

    //Leaving these incase we end up needing the full game object, but for now we don't
    //private GameObject player;
    //private GameObject enemy1;
    //private GameObject enemy2;
    //private GameObject enemy3;

    private int playerLives;
    private int enemy1Lives;
    private int enemy2Lives;
    private int enemy3Lives;

    private float timer;
    private float looseTimer;
    public float gameEndTime = 5.0f; //Can Change this number in the editor if it needs to be changed

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {

        //Check how many lives there are
        playerLives = GameObject.Find("Player").GetComponent<PinballMovement>().lives;
        enemy1Lives = GameObject.Find("Enemy1").GetComponent<SimpleAgent>().lives;
        enemy2Lives = GameObject.Find("Enemy2").GetComponent<SimpleAgent>().lives;
        enemy3Lives = GameObject.Find("Enemy3").GetComponent<SimpleAgent>().lives;

        //If the player has no lives, and the enemies are still alive, start the loose countdown
        if (playerLives <= 0)//&& winTimer == 0.0f)
        {
            //StartCoroutine(looseTime());
            timer += 1.0f * Time.deltaTime;
            print("loosing");
        }

        //If the enemies are all dead, and the player is still alive, start the win countdown
        if (enemy1Lives == 0 && enemy2Lives == 0 && enemy3Lives == 0)//&& looseTimer == 0.0f)
        {
            //StartCoroutine(winTime());
            timer += 1.0f;
            print("winning");
        }


        if (timer >= 5)
        {

            timer = 0;
            EnemyArrayTracker.IsWinner = false;
            LoadMainMenu();

        }

    }

        //Win and Loose Scene loaders. 
        //I Am planning on adjusting these to make it so it loads in either a winscene or a loose scene rather than going to the main menu
        //needs timer instead of coroutine. This does each frame create a coroutine.
        IEnumerator winTime()
        {
        print("winner");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");

        }

        IEnumerator looseTime()
        {
        print("looser");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
        }


    void LoadMainMenu()
    {
        print("loading main menu scene");
        SceneManager.LoadScene("MainMenu");
    }

	
}
