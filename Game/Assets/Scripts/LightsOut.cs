using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOut : MonoBehaviour {

    public int StartingLap = 1;
    bool deactivated = false;
    WaypointManager wm;
    Light light;

    // Use this for initialization
    void Start() {
        wm = GameObject.Find("WaypointManager").GetComponent<WaypointManager>();
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update() {
        if (wm.getCurrentLap() >= StartingLap && !deactivated) {
            if (Mathf.Approximately(light.intensity, 0)) {
                deactivated = true;
            } else {
                light.intensity = Mathf.Lerp(light.intensity, 0, 3f * Time.deltaTime);
            }
        }
    }
}
