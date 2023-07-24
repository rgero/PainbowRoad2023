using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextChatBoxes : MonoBehaviour {

	public float positionX;
	public Text textComponent;
	public Transform parent;
	private Color[] listOfPotentialColors;
	bool startMoving;
	float endFloat;

	// Use this for initialization
	void Awake () {
		//This does not guarentee text will be on screen.
		parent = GameObject.Find ("TextBoxHolder").GetComponent<RectTransform>();
		RectTransform boxTransform = this.GetComponent<RectTransform> ();
		this.transform.SetParent (parent);
		positionX = Screen.width;
		endFloat = -boxTransform.rect.width;
		this.transform.position = new Vector3 (positionX, 0, this.transform.position.z);


		//It's weird that they didn't have an enumerated list of default colors. TODO: Refine this.
		listOfPotentialColors = new Color[]{Color.blue,Color.cyan,Color.gray,Color.green,Color.magenta,Color.red,Color.white,Color.yellow};
	}

	public void setMessage(string msg){
		textComponent.text = msg;
		int colorChoice = Random.Range (0, listOfPotentialColors.Length - 1);
		textComponent.color = listOfPotentialColors [colorChoice];

	}
	
	// Update is called once per frame
	void Update () {
		if (positionX > endFloat) {
			positionX -= 5.0f;
			this.transform.position = new Vector3 (positionX, 0 , this.transform.position.z);
		} else {
			Destroy (this.gameObject);
		}
	}
}
