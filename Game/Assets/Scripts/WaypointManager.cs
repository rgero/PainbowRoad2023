using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class WaypointManager : MonoBehaviour {

	GameObject track;
	Transform[] childrenOfTrack;

	public GameObject lastWaypoint;
	public Vector3 lastRotation;
	public GameObject startWaypoint;
	public Text lapCounter;
    public int TotalLaps = 3;
    public int GiantsLap {get; private set;}
    public int FinalLap {get; private set;}
    public LevelManager LevelManager;
    public string VictoryScene;

	public int Lap = 1;
	private List<BoxCollider> waypointList = new List<BoxCollider>();


	public int completedWaypoints;
	public int totalWaypoints;

	const string LapText = "Lap: ";

	// Use this for initialization
	void Start () {
		GiantsLap = 1;
		FinalLap = 3;

		lastWaypoint = null;
		lastRotation = new Vector3();

		totalWaypoints = 0;
		completedWaypoints = 0;

		track = GameObject.Find ("Track");
		childrenOfTrack = track.GetComponentsInChildren<Transform> ();
		foreach(Transform i in childrenOfTrack){
			
			if (i.name.Equals ("Waypoint") && !i.transform.parent.name.Equals("Start")) {
				totalWaypoints++;
				waypointList.Add (i.GetComponent<BoxCollider> ());
			}
		}

        // Fudge factor in case a couple are somehow skipped
        totalWaypoints -= 4;

        if (lapCounter != null) {
            lapCounter.text = LapText + Lap.ToString();
        }
    }

    public void setLastWayPoint(GameObject last){

		if (!last.Equals (startWaypoint)) {
			lastWaypoint = last;
			last.GetComponent<BoxCollider> ().enabled = false;
			completedWaypoints++;
		} else {
			lastWaypoint = last;
		}

		if (last.Equals (startWaypoint) && (completedWaypoints >= totalWaypoints)) {
			Lap++;
            if (Lap > TotalLaps) {
                LevelManager.LoadLevel(VictoryScene);
            }
            if (lapCounter) {
                lapCounter.text = LapText + Lap.ToString();
            }
            completedWaypoints = 0;
			foreach (BoxCollider j in waypointList) {
				j.enabled = true;
			}
		}
	}

	public GameObject getLastWaypoint(){
		return lastWaypoint;


	}

	public void setLastRotation(Vector3 last){
		lastRotation = last;
	}

	public Vector3 getLastRotation(){
		return lastRotation;
	}

	public void setStartWaypoint(GameObject start){
		startWaypoint = start;
	}

    public int getCurrentLap() {
        return Lap;
    }

}
