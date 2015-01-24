using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour {
    // Statistics
    public static float BestTime;
    public static int DeathCounter;
    public static float TimePlayed;

    [HideInInspector] public int BeatCounter;
    public Text BeatCounterText;


    public AudioClip[] LevelAnounce;

    public float LevelTime;
    public Text LevelTimeText;
    private GameObject camera;

    private bool isGameEnd;
    private bool isWin;

    // Use this for initialization
    private void Start() {
        HexagonUtils.GetDirectionByAngle(new Vector3(1, 1), new Vector3(1, 2));
        HexagonUtils.GetDirectionByAngle(new Vector3(1, 1), new Vector3(0, 0));
        HexagonUtils.GetDirectionByAngle(new Vector3(1, 1), new Vector3(0, 4));
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

        // Debug.Log(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent);

        BeatCounterText.text = "Beats: " + BeatCounter;
    }

    public void LoseGame() {
        Debug.Log("LOOSE");
    }

    private void OnDestroy() {
        PlayerPrefs.SetFloat("besttime", BestTime);
        PlayerPrefs.SetInt("deathtimes", DeathCounter);
        PlayerPrefs.SetFloat("timesplayed", TimePlayed);
    }

    // Update is called once per frame
    private void Update() {
        LevelTime += Time.deltaTime;
        LevelTimeText.text = string.Format("TIME: {0:F2}", LevelTime);
    }
}