using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetCar : MonoBehaviour {

	Quaternion originalRotation;
	WaypointManager wpManager;
	public Text resetCounter;
	int counter;
    Quaternion joshRotation;
    Vector3 joshPosition;
    private float? resetTime = null;
    private float? graceJointTime = null;
    private float storedBreakForce;
    private float storedBreakTorque;
    private FixedJoint joshJoint;

    public GameObject josh;
    public GameObject joshPrefab;
    public AudioClip[] NumberedTaunts;
    public AudioClip[] RandomTaunts;

    const string ResetText = "Number of Resets: ";

	// Use this for initialization
	void Start () {
		originalRotation = this.transform.rotation;	
		counter = 0;
        if (resetCounter != null) {
            resetCounter.text = ResetText + counter.ToString();
        }
        wpManager = GameObject.Find ("WaypointManager").GetComponent<WaypointManager> ();
        joshRotation = josh.transform.localRotation;
        joshPosition = josh.transform.localPosition;

        joshJoint = gameObject.GetComponent<FixedJoint>();
        storedBreakForce = joshJoint.breakForce;
        storedBreakTorque = joshJoint.breakTorque;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("ResetButton")){
			Debug.Log ("RESETTING");

            if (counter < NumberedTaunts.Length) {
                GetComponent<AudioSource>().PlayOneShot(NumberedTaunts[counter]);
            } else {
                GetComponent<AudioSource>().PlayOneShot(RandomTaunts[Random.Range(0, RandomTaunts.Length)]);
            }

			counter++;
            if (resetCounter) {
                resetCounter.text = ResetText + counter.ToString();
            }

            severJosh();
            resetTime = Time.time + 2;

            GameObject lastWaypoint = wpManager.getLastWaypoint ();
			if (lastWaypoint != null) {
				Vector3 newPosition = lastWaypoint.transform.position;
				newPosition.y += 3;
				transform.position = newPosition;
				Vector3 rotation = wpManager.getLastRotation ();
				rotation.z = 0;
				transform.rotation = Quaternion.Euler(	rotation );
			} else {
				transform.position = new Vector3 (-30, 3, 0);		//This is a magic number... This should not be a magic number.
				transform.rotation = originalRotation;
			}
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
			gameObject.GetComponent<Rigidbody> ().freezeRotation = false;
        }

        if (resetTime != null && resetTime <= Time.time)
        {
            resetTime = null;
            generateJosh();
        }

        if (graceJointTime != null && graceJointTime <= Time.time)
        {
            graceJointTime = null;
            if (joshJoint != null) {
                joshJoint.breakForce = storedBreakForce;
                joshJoint.breakTorque = storedBreakTorque;
            }
        }

    }

    void playTaunt() {

    }

    void OnJointBreak(float force)
    {
        resetTime = Time.time + 5;
        severJosh();
    }

    void severJosh()
    {
        josh.transform.SetParent(null);
        if (joshJoint != null)
        {
            Destroy(joshJoint);
            joshJoint = null;
        }
    }

    void generateJosh()
    {
        josh = (GameObject)Instantiate(joshPrefab, transform);
        josh.transform.localPosition = joshPosition;
        josh.transform.localRotation = joshRotation;


        var newJoint = gameObject.AddComponent<FixedJoint>();
        newJoint.breakForce = Mathf.Infinity;
        newJoint.breakTorque = Mathf.Infinity;
        newJoint.connectedBody = josh.transform.Find("Armature/hips").GetComponent<Rigidbody>();
        joshJoint = newJoint;
        graceJointTime = Time.time + 3;
    }
}
