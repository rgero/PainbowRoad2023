using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrankManager : MonoBehaviour {

	//These are the variables needed to control the flow of the pranks.
	Queue<string> commandQueue;
	List<GameObject> runningCommands;

	//Prefabs
	public GameObject FlipCameraPrefab;
	public GameObject TrackDisappearPrefab;
	public GameObject ReverseControlPrefab;

	//Constants
	public const int RUNNING_PRANK_MAX = 1;
	public const float COOLDOWN_TIME = 3.0f;
    public const int QUEUE_MAX = 3;
	private float timeLeft;

	bool buttonPressed;
	TextChatBoxSpawner tcb; //This is not a prefab because it can be called as many times as we want 

	// Use this for initialization
	void Start () {
		tcb = GetComponentInChildren<TextChatBoxSpawner> ();
		buttonPressed = false;
		commandQueue = new Queue<string> ();
		runningCommands = new List<GameObject> ();
		timeLeft = COOLDOWN_TIME;
	}

	/// <summary>
	/// Adds a command to the queue.
	/// </summary>
	/// <param name="command">Command String</param>
	public void addToQueue(string command){
        if (commandQueue.Count < QUEUE_MAX) {
            commandQueue.Enqueue(command);
        }
	}

	/// <summary>
	/// Executes the next command.
	/// </summary>
	public void executeNextCommand(){
		if (commandQueue.Count > 0) {
			string nextCommand = commandQueue.Dequeue ();
			GameObject newCommand = null;
			switch (nextCommand.ToLower()) {
				case "flipper":
					newCommand = Instantiate (FlipCameraPrefab) as GameObject;
					newCommand.name = "Flip";
					break;
				case "trackdisappear":
					newCommand = Instantiate (TrackDisappearPrefab) as GameObject;
					newCommand.name = "TD";
					break;
				case "reversecontrols":
					newCommand = Instantiate (ReverseControlPrefab) as GameObject;
					newCommand.name = "Reverse";
					break;
				default:
					break;
			}

			/* This is a bit sloppy I admit.
			 * However if we were to fix it we'd need a generalized script that controls the activation of the pranks. 
			 */
			if (newCommand != null) {
				newCommand.transform.parent = this.transform;
				runningCommands.Add (newCommand);
				if (newCommand.name.ToLower().Equals ("td")) {
					newCommand.GetComponent<TrackoDisappearo> ().Activate ();
				} else if (newCommand.name.ToLower().Equals ("flip")) {
					newCommand.GetComponent<CameraFlipper> ().Activate ();
				} else if (newCommand.name.ToLower().Equals ("reverse")) {
					newCommand.GetComponent<ReverseControlsPrank> ().Activate ();
				}
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.J) && !buttonPressed) {
			buttonPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.J) && buttonPressed){
			buttonPressed = false;
			addToQueue ("flipper");
		}

		if (Input.GetKey (KeyCode.K) && !buttonPressed) {
			buttonPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.K) && buttonPressed){
			buttonPressed = false;
			addToQueue ("trackdisappear");
		}

		if (Input.GetKey (KeyCode.N) && !buttonPressed) {
			buttonPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.N) && buttonPressed){
			buttonPressed = false;
			addToQueue ("reversecontrols");
		}

		if (Input.GetKey (KeyCode.L) && !buttonPressed) {
			buttonPressed = true;
		}
		if (Input.GetKeyUp(KeyCode.L) && buttonPressed){
			buttonPressed = false;
			tcb.addToMessageQueue ("iangoifngapoiudfgbpisdfgbspifdugbpfsidugbdfspiugbfdpiugbdfspiugbsfdpigubsdfpiugbfdspiubg");
		}


		//Checks to see if we are running the appropriate amount of commands.
		if (runningCommands.Count < RUNNING_PRANK_MAX) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {			// This is so we can have a cooldown if we so choose.
				executeNextCommand ();
				timeLeft = COOLDOWN_TIME;
			}
		} else {
			for(int i = 0; i < runningCommands.Count; i++){
				GameObject currentTask = runningCommands [i];
				if (!currentTask.activeSelf) {
					runningCommands.Remove(currentTask);
					Destroy (currentTask.gameObject);
				}
			}
		}



	}
}
