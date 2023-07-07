using UnityEngine;
using System.Collections;

public class KillIfOld : MonoBehaviour {

    public float KillTime = 15;

    float age;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        age += Time.deltaTime;
        if (age >= KillTime) {
            Destroy(gameObject);
        }
	}
}
