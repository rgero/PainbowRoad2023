using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public float SecondsBetweenShots = 1;
    public GameObject ThingToShoot;
    public Transform Container;
    public bool MotionActivated = true;
    public float ShotVelocity = 25;

    float timeSinceLastShot;
    bool activated;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastShot += Time.deltaTime;

        if (MotionActivated && !activated) {
            return;
        }

        if (timeSinceLastShot > SecondsBetweenShots) {
            Fire();
            timeSinceLastShot = 0;
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

    void Fire() {
        GameObject bullet = Instantiate(ThingToShoot, transform.position, ThingToShoot.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * ShotVelocity;

        bullet.transform.SetParent(Container, true);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}
