using UnityEngine;
using System.Collections;

public class Bumpers : MonoBehaviour {

    private float Force = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.root.tag == "Player") {
            other.gameObject.transform.root.GetComponent<Rigidbody>().AddForce(new Vector3(0, Force, 0), ForceMode.Impulse);
            Debug.Log("Bumper hit");
        }
    }
}
