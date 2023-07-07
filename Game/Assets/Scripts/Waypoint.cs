using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	public GameObject manager;
	WaypointManager wpManager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("WaypointManager");
		wpManager = manager.GetComponent<WaypointManager> ();

		if (this.transform.parent.gameObject.name.Equals ("Start")) {
			wpManager.setStartWaypoint (this.gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerExit(Collider c)
	{
		if (c.gameObject.name.Equals ("ColliderBody")) {
			wpManager.setLastWayPoint (this.gameObject);

			/*Rotations
				TRACK PIECE		|		CAR
							0	|	90
							90	|	180
							180 |	270
							270	|	0
			*/

			Vector3 carRotation = new Vector3 (0, 0, 0);
			carRotation.y = this.gameObject.transform.eulerAngles.y + 90;
			wpManager.setLastRotation (carRotation);
		}
	}
}
