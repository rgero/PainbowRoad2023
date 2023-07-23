using UnityEngine;
using UnityEngine.Video;

public class TV : MonoBehaviour {

    public VideoClip GiantsMovie;
    public VideoClip CountdownMovie;
    public AudioClip GiantsAudio;
    public AudioClip CountdownAudio;
    public Material BlankMat;
    public Material VideoMat;

    bool _giantsActivated = false;
    bool _countdownActivated = false;
    WaypointManager _waypointManager;
    AudioSource bgm;
    private VideoPlayer videoPlayer;
    AudioSource audioSource;
    private Renderer renderer;

    // Use this for initialization
    void Start() {
        _waypointManager = GameObject.Find("WaypointManager").GetComponent<WaypointManager>();
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
        bgm = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        renderer = GetComponent<Renderer>();
    }

    void Activate(VideoClip movie, AudioClip clip)
    {
        renderer.materials = new[] { BlankMat, VideoMat };
        audioSource.Stop();
        audioSource.clip = clip;

        videoPlayer.clip = movie;

        videoPlayer.Play();
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
	    if (_waypointManager.getCurrentLap() == _waypointManager.GiantsLap && !_giantsActivated) {
            Activate(GiantsMovie, GiantsAudio);
            audioSource.volume = 1;
            audioSource.spatialBlend = 1;
            _giantsActivated = true;
        } else if (_waypointManager.getCurrentLap() == _waypointManager.FinalLap && !_countdownActivated) {
            Activate(CountdownMovie, CountdownAudio);
            audioSource.volume = 0.25f;
            audioSource.spatialBlend = 0.75f;
            _countdownActivated = true;
            bgm.enabled = false;
        }
	}
}
