using UnityEngine;
using System.Collections;

public class WarningLogic : MonoBehaviour {

    public int LifeBeatTime;
    private int _curLifeBeatTime;
    public int WarningBeatTime;
    public int Direction;

    public GameObject WarningPrefab;

    public Sprite AttackSprite;
    public float NextWarnDelay;

    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += (sender, args) => BeatProjectileLogic();
        _curLifeBeatTime = 0;
    }

    void CreateNextWarning()
    {
        Vector2 NextPos = HexagonUtils.GetVectorBySide(Direction) + HexagonUtils.GetV2FromV3(transform.position);
        GameObject NextWall = HexagonUtils.GetObjByWorldPos(NextPos);

        if (NextWall != null)
        {
            if (NextWall.ToString().StartsWith("Hexagon("))
            {
                GameObject nextWarn = Instantiate(WarningPrefab, NextPos, Quaternion.identity) as GameObject;
                nextWarn.GetComponent<WarningLogic>().Direction = Direction;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        NextWarnDelay -= Time.deltaTime;

        if (NextWarnDelay <= 0)
        {
            CreateNextWarning();
            NextWarnDelay = 500;
        }
        
    }

    void BeatProjectileLogic()
    {
        _curLifeBeatTime += 1;

        if (_curLifeBeatTime >= LifeBeatTime)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent -= (sender, args) => BeatProjectileLogic();
            gameObject.SetActive(false);
            //  k   Destroy(gameObject);
        }


        if (_curLifeBeatTime >= WarningBeatTime)
        {
            GetComponent<SpriteRenderer>().sprite = AttackSprite;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    void MoveLogic()
    {
    }
}
