using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelManager: MonoBehaviour {
    // Statistics
    public static float BestTime;
    public static int DeathCounter;
    public static float TimePlayed;
    public AudioClip AgainLevel;

    public Text BadText;
    [HideInInspector] public int BeatCounter;
    public int CurLevel = 0;
    public Text FinalText;
    public float FreeTime;
    public AudioClip GameOver;
    public AudioClip GameStart;


    public String[] Level13Texts;
    public String[] Level45Texts;
    public String[] Level6Texts;
    public AudioClip[] LevelAnounce;
    public AudioClip LevelFailed;
    public float LevelTime;
    public Text LevelTimeText;
    public AudioClip LevelWin;
    public Text LivesText;
    public String[] LooseTexts;
    public AudioClip NewRecord;
    public GameObject Player;
    public AudioClip PlayerHurt;
    public int PlayersLife;
    public Text RecordText;
    public Text CurLevelText;
    public String[] StartTexts;
    private float _badTextShowTimer;


    public Image UnderImage;
    private bool _isNewRecord;
    private GameObject camera;


    private bool isGameEnd;
    private bool isWin;
    public float nextLevelTime;

    public int CurrentTactic;
    private float _changeTacticTime = 7f;
    private float _curChangeTacticTime = 0f;
    public int CurTurretActive = 0;

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

        FinalText.enabled = false;
        RecordText.enabled = false;
        _isNewRecord = false;

        isWin = false;
        UnderImage.enabled = false;

        camera.GetComponent<BeatTracker>().BeatEvent += (sender, args)=>BeatAction();

        BestTime = PlayerPrefs.GetFloat("besttime", 0);
        DeathCounter = PlayerPrefs.GetInt("deathtimes", 0);
        TimePlayed = PlayerPrefs.GetFloat("timesplayed", 0);

        AudioSource.PlayClipAtPoint(GameStart, transform.position);

        GetComponent<LevelHexagonCreator>().CreateLevel(0);
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
        AudioSource.PlayClipAtPoint(PlayerHurt, transform.position);
    }


    private void OnDestroy() {
        PlayerPrefs.SetFloat("besttime", BestTime);
        PlayerPrefs.SetInt("deathtimes", DeathCounter);
        PlayerPrefs.SetFloat("timesplayed", TimePlayed);
    }

    private void UpgradeLevel() {
        CurLevel++;
        _curChangeTacticTime = 0f;
        GetComponent<LevelHexagonCreator>().CreateLevel(CurLevel);
        CurLevelText.text = "Level " + CurLevel; 
        if (CurLevel <= 6){
            AudioSource.PlayClipAtPoint(LevelAnounce[CurLevel - 1], transform.position);
        }

        FreeTime = 3f;
        nextLevelTime += 20;

        ShowBadText();
    }

    public void ShowBadText() {
        _badTextShowTimer = 3f;

        BadText.GetComponent<SlowlyAppear>().Reset();
//        BadText.GetComponent<SlowlyAppear>().

        if (CurLevel == 1){
            BadText.text = StartTexts[Random.Range(0, StartTexts.Length)];
        }

        if (CurLevel > 1 && CurLevel <= 3){
            BadText.text = Level13Texts[Random.Range(0, Level13Texts.Length)];
        }

        if (CurLevel == 4 || CurLevel == 5){
            BadText.text = Level45Texts[Random.Range(0, Level45Texts.Length)];
        }

        if (CurLevel == 6){
            BadText.text = Level6Texts[Random.Range(0, Level6Texts.Length)];
        }
    }

    // Update is called once per frame
    private void Update() {
        LevelTime += Time.deltaTime;
        FreeTime -= Time.deltaTime;

        _curChangeTacticTime += Time.deltaTime;

        if (_curChangeTacticTime > _changeTacticTime)
        {
            _curChangeTacticTime = 0f;
            CurrentTactic += Random.Range(1, 3  );
            CurrentTactic = CurrentTactic % 3;
            CurTurretActive = Random.Range(0, 6);
            Debug.Log("CHoose tactic " + CurrentTactic);
        }

        if (LevelTime > BestTime && !_isNewRecord){
            AudioSource.PlayClipAtPoint(NewRecord, transform.position);
            _isNewRecord = true;
        }
        _badTextShowTimer -= Time.deltaTime;
        TimePlayed += Time.deltaTime;
        LevelTimeText.text = string.Format("{0:F2}", LevelTime);

        LivesText.text = "Lives " + PlayersLife;

        if (_badTextShowTimer < 0){
            BadText.text = "";
        }

        if (FreeTime < 0){
            FreeTime = 0;
        }

        if (LevelTime >= 130){
            if (!isWin){
                isWin = true;
                StopGame();
            }
        }

        if (nextLevelTime < LevelTime){
            UpgradeLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            Application.LoadLevel("Menu");
        }

        if (isGameEnd){
            if (Input.GetKeyDown(KeyCode.R)){
                AudioSource.PlayClipAtPoint(AgainLevel, transform.position);
                Time.timeScale = 1;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }


    public void StopGame() {
        Debug.Log("You win!");
        BestTime = 130f;

        isGameEnd = true;
        isWin = true;

        FinalText.enabled = true;
        RecordText.enabled = true;
        LevelTimeText.enabled = false;
        UnderImage.enabled = true;

        if (LevelTime >= 130)
        {
            LevelTime = 130f;
        }

        RecordText.text =
            (string.Format("TIME: {0:F2}\nBEST: {1:f2}\nDeaths:{2}\nTime played:{3}", LevelTime, BestTime, DeathCounter,
                (int) (TimePlayed/60)));
        FinalText.text = "AMAZING!!!\nYOU BEAT THE BEAT-MONSTER\n";
        Time.timeScale = 0;
    }

    public void LoseGameFinal() {
        Debug.Log("LOOSE");
        AudioSource.PlayClipAtPoint(LevelFailed, transform.position, 0.8f);
        _badTextShowTimer = 5f;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = 0.3f;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
        BadText.text = LooseTexts[Random.Range(0, LooseTexts.Length)];


        DeathCounter++;
        isGameEnd = true;

        if (BestTime < LevelTime){
            BestTime = LevelTime;
        }

        FinalText.enabled = true;
        RecordText.enabled = true;
        LevelTimeText.enabled = false;
        UnderImage.enabled = true;

        RecordText.text =
            (string.Format("TIME: {0:F2}\nBEST: {1:f2}\nDeaths:{2}\nTime played:{3}", LevelTime, BestTime, DeathCounter,
                (int) (TimePlayed/60)));

        FinalText.text = "ESC TO EXIT\nR TO RESTART";

        Time.timeScale = 0;
    }
}