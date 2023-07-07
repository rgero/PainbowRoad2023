using UnityEngine;
using System.Collections;

public class JoshBobble : MonoBehaviour {

	Transform joshTransform;
	const float TIME_STOP_ORIG_VALUE = 0.5f;
	float timeStop = 0.5f;
	float angleLock = 15.0f;
	float speed = 0.5f;
	public bool turnRight;
	Vector3 joshRotation;

	// Use this for initialization
	void Start () {
		joshTransform = this.GetComponent<Transform> ();
		joshRotation = joshTransform.localRotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		if (turnRight) {
			if (joshRotation.z >= -15.0) {
				joshRotation.z -= speed;
			} else {
				if (timeStop > 0) {
					timeStop -= Time.deltaTime;
				} else {
					turnRight = !turnRight;
					timeStop = TIME_STOP_ORIG_VALUE;
				}
			}
		} else {
			if (joshRotation.z <= 15.0) {
				joshRotation.z += speed;
			} else {
				if (timeStop > 0) {
					timeStop -= Time.deltaTime;
				} else {
					turnRight = !turnRight;
					timeStop = TIME_STOP_ORIG_VALUE;
				}
			}
		}
		joshTransform.localRotation = Quaternion.Euler (joshRotation);
	}
}
