using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class ReverseControlsPrank : MonoBehaviour {

	public GameObject textWindow;
	public CarUserControl controls;

	bool isRunning;
	bool textCreated;
	const float originalTimer = 5.0f;
	float timer;
	GameObject Car;
	GameObject reverseText;


	// Use this for initialization
	void Awake () {
		isRunning = false;
		textCreated = false;
		timer = originalTimer;
		Car = GameObject.Find ("Car");
		controls = Car.GetComponent<CarUserControl> ();
	}

	public void Activate(){
		isRunning = true;
	}

	/// <summary>
	/// A rather sloppy way to present text to Josh. :( I don't like sloppy.
	/// </summary>
	public void initalizeText(){
		reverseText = Instantiate (textWindow) as GameObject;
		reverseText.transform.SetParent(GameObject.Find ("Canvas").transform,true);
		reverseText.name = "ControlNotifier";
		int screenWidth = Screen.width;
		int screenHeight = Screen.height;
		reverseText.transform.localPosition = new Vector3 (screenWidth/2, screenHeight/2-10, 0);
		Text textMessage = reverseText.GetComponent<Text> ();
		textMessage.text = "REVERSED CONTROLS!";
		textMessage.fontSize = 30;
		textMessage.color = Color.white;
		textCreated = true;
	}

	// Update is called once per frame
	void Update () {
		if (isRunning) {
			
			if (timer == originalTimer) {
				initalizeText ();
				Debug.Log (reverseText.transform.parent.name);
			}
			if (textCreated) {
				Debug.Log (reverseText.transform.parent.name);
					if (timer == originalTimer) {
						controls.reverseDirection (true);
					}
					if (timer < 0) {
						controls.reverseDirection (false);
						timer = originalTimer;
						isRunning = false;
						Destroy (reverseText);
						this.gameObject.SetActive (false); //By setting this inactive, the PrankManager will know to clean it up.
					}
					timer -= Time.deltaTime;
			}
		}
	}
}
