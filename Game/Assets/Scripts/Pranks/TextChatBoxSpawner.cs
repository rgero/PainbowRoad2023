using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextChatBoxSpawner : MonoBehaviour {

	public GameObject textChatBoxPrefab;
	public GameObject TextBoxHolder;
	public int MAX_MESSAGES;
	public const float CHAT_MSG_RATE = 1.5f;
	public float timer = 1.5f;

	Queue<string> messageQueue;

	public void addToMessageQueue(string msg){
		messageQueue.Enqueue (msg);
	}


	public void Activate(string msg){
		
		GameObject tb = Instantiate (textChatBoxPrefab) as GameObject;
		tb.GetComponent<TextChatBoxes> ().setMessage (msg);
	}


	public void Start(){
		messageQueue = new Queue<string> ();
	}

	public void Update(){
		if (TextBoxHolder.transform.childCount < MAX_MESSAGES) {
			if (messageQueue.Count > 0 && timer < 0) {
				timer = CHAT_MSG_RATE;
				Activate (messageQueue.Dequeue());
			}
		}
		timer -= Time.deltaTime;


	}

}



