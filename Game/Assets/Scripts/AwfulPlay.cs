using UnityEngine;
using System.Collections;

public class AwfulPlay : MonoBehaviour {

    public AudioSource AttachedSource;
    public float MinimumPitchShiftSeconds;
    public float MaximumPitchShiftSeconds;
    public float MinimumPitchRange;
    public float MaximumPitchRange;

    private float currentPitch = 1;
    private float nextPitchShift = 10000;

	// Use this for initialization
	void Start () {
        setNextPitchshift();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time >= nextPitchShift) {
            setPitch();
            setNextPitchshift();
        }
	}

    void setNextPitchshift() {
        nextPitchShift = Time.time + Random.Range(MinimumPitchShiftSeconds, MaximumPitchShiftSeconds);
    }

    void setPitch() {
        AttachedSource.pitch = Random.Range(MinimumPitchRange, MaximumPitchRange);
    }
}
