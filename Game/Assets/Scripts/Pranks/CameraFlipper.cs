using System;
using UnityEngine;

public class CameraFlipper : MonoBehaviour 
{
    public Camera MainCamera;

	bool isRunning;
	float speed;
	float timer;
	const float originalTimer = 5.0f;
	bool direction;

	//This needs to be Awake as opposed to Start so when it is instantiated, it actually starts properly.
	void Awake() {
		isRunning = false;
		speed = 1.5f;
		timer = 5.0f;
		direction = true;

		MainCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	}

	public void Activate(){
		isRunning = true;
	}

	void Update()
	{
		if (isRunning) {
			Vector3 rotation = MainCamera.transform.localRotation.eulerAngles;
			if (direction && rotation.z < 180) {
				rotation.z += speed;
			} else {
				timer -= Time.deltaTime;
				if (timer < 0) {
					direction = false;
				}
			}
			if (!direction && rotation.z > 0) {
				rotation.z -= speed;
			}

			if (!direction && rotation.z <= 0) {
				isRunning = false;
				rotation.z = 0;
				timer = originalTimer;
				direction = true;
				this.gameObject.SetActive (false); //By setting this inactive, the PrankManager will know to clean it up.
			}

			MainCamera.transform.localRotation = Quaternion.Euler (rotation);
		}


	}
}

