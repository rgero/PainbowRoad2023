﻿using UnityEngine;
using System.Collections;

public class Abyss : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.root.tag != "Player") {
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.transform.root.tag != "Player") {
            Destroy(other.gameObject);
        }
    }
}
