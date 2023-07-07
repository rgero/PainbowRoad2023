using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PineappleSurprise : MonoBehaviour {

    public float EscapeVelocity;
    public Text WinnerBox;
    public float ExchangeMessageSeconds = 5;
    public List<Spawner> SpawnersToPower = new List<Spawner>();

    private bool winning = false;
    private bool flippyFlop = true;
    private float messageFlipTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (winning) {
            if (flippyFlop) {
                WinnerBox.text = "YOU ARE WINNER!!!";
            } else {
                WinnerBox.text = "THANK FOR PLAYING!";
            }
            if (Time.time >= messageFlipTime) {
                messageFlipTime = Time.time + ExchangeMessageSeconds;
                flippyFlop = !flippyFlop;
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * EscapeVelocity;
            winning = true;
            messageFlipTime = Time.time + ExchangeMessageSeconds;
            foreach (var spawner in SpawnersToPower) {
                spawner.Powered = true;
            }
        }
    }
}
