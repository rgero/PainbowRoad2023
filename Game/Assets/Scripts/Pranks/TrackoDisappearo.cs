using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackoDisappearo : MonoBehaviour {
    public GameObject Track;

    bool isRunning;
	bool isHidden;
	float timer;
	const float originalTimer = 5.0f;
	List<Transform> trackPieces;

	//This needs to be Awake as opposed to Start so when it is instantiated, it actually starts properly.
	void Awake ()
	{
		Track = GameObject.Find ("Track");
		isRunning = false;
		isHidden = false;
		timer = originalTimer;

		trackPieces = getValidPieces();
	}

	public void Activate(){
		isRunning = true;
	}

	public bool isAlive(){
		return isRunning;
	}

	void Update()
	{
		if (isRunning) {
			if (timer > 0 && trackPieces.Count > 0) {
				if (!isHidden) {
					foreach (Transform t in trackPieces) {
						t.gameObject.GetComponent<MeshRenderer> ().enabled = false;
					}
					isHidden = true;
				}
				timer -= Time.deltaTime;
			} else {
				foreach (Transform t in trackPieces) {
					t.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				}
				isHidden = false;
				timer = originalTimer;
				isRunning = false;
				this.gameObject.SetActive (false); //By setting this inactive, the PrankManager will know to clean it up.
			}
		}
	}

    private List<Transform> getValidPieces() {
        List<Transform> piecesToReturn = new List<Transform>();
        Transform[] childrenOfTrack = Track.GetComponentsInChildren<Transform>();
        foreach (Transform t in childrenOfTrack) {
            //This next line makes me think I should change this to a tag, rather than iterating through the names.
			if (!t.gameObject.name.Equals("Track") && !t.gameObject.name.Equals("Waypoint") && !t.gameObject.name.Equals("Bumpers") && !t.gameObject.name.Equals("Bumper")) {
                piecesToReturn.Add(t);
            }
        }
        return piecesToReturn;
    }
}
