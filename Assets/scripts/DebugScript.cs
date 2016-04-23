using UnityEngine;
using System.Collections;

public class DebugScript : MonoBehaviour {


    public TextMesh DebugText1;
    public TextMesh DebugText2;
    public TextMesh DebugText3;

    public enum GameState { Paused,None };

    public static GameState state;
    // Use this for initialization
    void Start () {
        state = GameState.None;
    }
	
	// Update is called once per frame
	void Update () {

        if (state == GameState.Paused)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (Input.GetButtonUp("Debug"))
        {
            if (state != GameState.Paused)
            {
                state = GameState.Paused;
            }
            else
            {
                state = GameState.None;
            }
        }

        if (state == GameState.Paused)
        {
            if (CameraAim.DebugHit != null)
            {
                print(CameraAim.DebugHit.name);
            }
            
            if (CameraAim.DebugHit != null && CameraAim.DebugHit.tag == "Player")
            {
                DebugText1.text ="Been hit: " + CameraAim.DebugHit.GetComponent<PinballMovement>().beenhit.ToString();

                DebugText2.text ="Velocity: " +  CameraAim.DebugHit.GetComponent<Rigidbody>().velocity.ToString();

                
            }

            if (CameraAim.DebugHit != null && CameraAim.DebugHit.tag == "Attacker")
            {

                DebugText1.text ="Been hit: " + CameraAim.DebugHit.GetComponent<SimpleAgent>().BeenHit.ToString();

                DebugText2.text ="Navmesh: " + CameraAim.DebugHit.GetComponent<NavMeshAgent>().enabled.ToString();

                DebugText3.text ="Detect Collisions: " + CameraAim.DebugHit.GetComponent<Collider>().attachedRigidbody.detectCollisions.ToString();

            }

        } else
        {
            DebugText1.text = "";
            DebugText2.text = "";
            DebugText3.text = "";
        }
	
	}
}
