using UnityEngine;
using System.Collections;

public class UICountDown : MonoBehaviour {

    public TextMesh Text;
    public SimpleAgent Agent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int countDown = (int)Agent.waitTimer;
        Text.text = countDown.ToString();

        if (Agent.waitTimer <= 0)
        {

            Destroy(gameObject);

        }
    }
}
