using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PostProcessing : MonoBehaviour {

    public Rigidbody car;

    Volume volume;

    private ChromaticAberration chromaticAberration;

    float originalChromaticAbberationIntensity;
    float maxChromaticAbberationIntensity = 0.75f;

	// Use this for initialization
	void Start ()
    {
        volume = gameObject.GetComponent<Volume>();

        chromaticAberration = (ChromaticAberration)volume.profile.components.Find(c => c.name.Contains("Chromatic"));

        originalChromaticAbberationIntensity = chromaticAberration.intensity.value;
    }

    // Update is called once per frame
    void Update () {
        if (car.velocity.x >= 7 && !Mathf.Approximately(chromaticAberration.intensity.value, maxChromaticAbberationIntensity)) {
            chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, maxChromaticAbberationIntensity, 1f * Time.deltaTime);
        } else if (!Mathf.Approximately(chromaticAberration.intensity.value, 0)) {
            chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, originalChromaticAbberationIntensity, 5f * Time.deltaTime);
        }
    }
}
