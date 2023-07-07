using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public float SecondsBetweenSpawns = 15;
    public GameObject ThingToSpawn;
    public bool MotionActivated = false;
    public bool Powered = true;
    public bool ScaleSpawnedThings = false;
    public Vector3 SpawnedThingScale = new Vector3(1, 1, 1);
    public Vector3 LaunchForce = Vector3.zero;

    float timeSinceLastSpawn;
    bool activated;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (! Powered) {
            return;
        }

        timeSinceLastSpawn += Time.deltaTime;

        if (MotionActivated && !activated) {
            return;
        }

        if (timeSinceLastSpawn > SecondsBetweenSpawns) {
            Spawn();
            timeSinceLastSpawn = 0;
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.root.tag == "Player") {
            activated = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.transform.root.tag == "Player") {
            activated = false;
        }
    }

    void Spawn() {
        var thingie = (GameObject)Instantiate(ThingToSpawn, transform.position, ThingToSpawn.transform.rotation);
        if (ScaleSpawnedThings) {
            thingie.transform.localScale = SpawnedThingScale;
        }
        var body = thingie.transform.GetComponent<Rigidbody>();
        if (body) {
            body.AddForce(LaunchForce);
        }
    }
}
