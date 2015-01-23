using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour {
    // Statistics
    public static float BestTime;
    public static int DeathCounter;
    public static float TimePlayed;

    public Text BeatCounterText;
    
    [HideInInspector]
    public int BeatCounter;


    public AudioClip[] LevelAnounce;

    public float LevelTime;
    private GameObject camera;

    private bool isGameEnd;
    private bool isWin;

    // Use this for initialization
    private void Start() {
        LevelTime = 0;
        BeatCounter = 0;
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        camera.GetComponent<BeatTracker>().BeatEvent += (sender, args)=>BeatAction();
        
        BestTime = PlayerPrefs.GetFloat("besttime", 0);
        DeathCounter = PlayerPrefs.GetInt("deathtimes", 0);
        TimePlayed = PlayerPrefs.GetFloat("timesplayed", 0);
    }

    private void BeatAction() {
        BeatCounter++;

        BeatCounterText.text = "Beats: " + BeatCounter;
    }

    private void OnDestroy() {
        PlayerPrefs.SetFloat("besttime", BestTime);
        PlayerPrefs.SetInt("deathtimes", DeathCounter);
        PlayerPrefs.SetFloat("timesplayed", TimePlayed);
    }

    // Update is called once per frame
    private void Update() {
    }
}