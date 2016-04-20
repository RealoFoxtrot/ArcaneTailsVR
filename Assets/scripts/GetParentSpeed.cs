using UnityEngine;
using System.Collections;

public class GetParentSpeed : MonoBehaviour {


    Animator RatAnim;
    float ParentSpeed;
    GameObject Parent;
    Vector3 lastPosition = Vector3.zero;

    void Start ()
    {
        RatAnim = GetComponent<Animator>();
        
        //Make a note, Chance this based on our player Prefs system
        Parent = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate ()
    {

        //Make a note, Chance this based on our player Prefs system
        ParentSpeed = (Parent.transform.position - lastPosition).magnitude;
        RatAnim.SetFloat("ParentSpeed", ParentSpeed);
        lastPosition = Parent.transform.position;

    }










    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	//override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
	//
	//}

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	//
	//}
}
