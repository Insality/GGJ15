using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour {
    // Statistics
    public static float BestTime;
    public static int DeathCounter;
    public static float TimePlayed;

    public int CurLevel = 0;
    public int PlayersLife;
    public float nextLevelTime;

    [HideInInspector] public int BeatCounter;
    public Text LivesText;
    public Text BadText;
    private float _badTextShowTimer = 0;

    public float LevelTime;
    public float FreeTime;
    public Text LevelTimeText;
    private GameObject camera;
    public GameObject Player;

    public AudioClip NewRecord;
    public AudioClip[] LevelAnounce;
    public AudioClip GameStart;
    public AudioClip GameOver;


    public String[] StartTexts;
    public String[] Level13Texts;
    public String[] Level45Texts;
    public String[] Level6Texts;
    public String[] LooseTexts;



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
        nextLevelTime = 5;
        FreeTime = 0;

        isWin = false;

        camera.GetComponent<BeatTracker>().BeatEvent += (sender, args)=>BeatAction();

        BestTime = PlayerPrefs.GetFloat("besttime", 0);
        DeathCounter = PlayerPrefs.GetInt("deathtimes", 0);
        TimePlayed = PlayerPrefs.GetFloat("timesplayed", 0);

        AudioSource.PlayClipAtPoint(GameStart, transform.position);
    }

    

    private void BeatAction() {
        BeatCounter++;

        // Debug.Log(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent);
    }

    /// Just a damage player, not full lose game
    public void LoseGame() {
        PlayersLife--;
        if (PlayersLife < 0){
            LoseGameFinal();
            PlayersLife = 3;
        }
        FreeTime = 3f;
        Player.GetComponent<PlayerLogic>().SetGodMode(3f);

    }

    public void LoseGameFinal() {
        Debug.Log("LOOSE");
        _badTextShowTimer = 5f;
        BadText.text = LooseTexts[UnityEngine.Random.Range(0, LooseTexts.Length)];

    }

    private void OnDestroy() {
        PlayerPrefs.SetFloat("besttime", BestTime);
        PlayerPrefs.SetInt("deathtimes", DeathCounter);
        PlayerPrefs.SetFloat("timesplayed", TimePlayed);
    }

    private void UpgradeLevel() {
        CurLevel++;
        if (CurLevel <= 5) {
            AudioSource.PlayClipAtPoint(LevelAnounce[CurLevel-1], transform.position);
        }

        nextLevelTime += 20;

        ShowBadText();
    }

    public void ShowBadText() {
        _badTextShowTimer = 3f;
        if (CurLevel == 1){
            BadText.text = StartTexts[UnityEngine.Random.Range(0, StartTexts.Length)];
        }

        if (CurLevel > 1 && CurLevel <= 3){
            BadText.text = Level13Texts[UnityEngine.Random.Range(0, Level13Texts.Length)];
        }

        if (CurLevel == 4 || CurLevel == 5){
            BadText.text = Level45Texts[UnityEngine.Random.Range(0, Level45Texts.Length)];
        }

        if (CurLevel == 6){
            BadText.text = Level6Texts[UnityEngine.Random.Range(0, Level6Texts.Length)];
        }
    }

    // Update is called once per frame
    private void Update() {
        LevelTime += Time.deltaTime;
        FreeTime -= Time.deltaTime;
        _badTextShowTimer -= Time.deltaTime;
        LevelTimeText.text = string.Format("{0:F2}", LevelTime);

        LivesText.text = "Lives: " + PlayersLife;

        if (_badTextShowTimer < 0){
            BadText.text = "";
        }

        if (FreeTime < 0){
            FreeTime = 0;
        }

        if (LevelTime >= 130) {
            if (!isWin) {
                isWin = true;
                StopGame();
            }
        }

        if (nextLevelTime < LevelTime) {
            UpgradeLevel();
        }
    }


    public void StopGame() {
        Debug.Log("You win!");
    }
}