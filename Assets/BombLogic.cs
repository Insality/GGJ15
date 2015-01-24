using UnityEngine;
using System.Collections;

public class BombLogic : GeneralProjectileLogic {
    public int LifeBeatTime;
    private int _curLifeBeatTime;
    public int MoveEveryBeat;
    public int Direction;


    public GameObject WarnBombPrefab;
    private Vector3 _goalMove;

    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += EventSub;

        _curLifeBeatTime = 0;
        _goalMove = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector2.zero;
        transform.position = Vector2.SmoothDamp(transform.position, _goalMove, ref velocity, 0.03f);
    }

    public override void BeatProjectileLogic()
    {
        _curLifeBeatTime += 1;

        if (_curLifeBeatTime >= LifeBeatTime)
        {
            Bomb();
            DestroyProjectile();
        }

        MoveLogic();
    }

    private void Bomb()
    {
        GameObject bomb;

        bomb = Instantiate(WarnBombPrefab) as GameObject;
        bomb.transform.position = transform.position + HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(0));

        bomb = Instantiate(WarnBombPrefab) as GameObject;
        bomb.transform.position = transform.position + HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(1));

        bomb = Instantiate(WarnBombPrefab) as GameObject;
        bomb.transform.position = transform.position + HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(2));

        bomb = Instantiate(WarnBombPrefab) as GameObject;
        bomb.transform.position = transform.position + HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(3));

        bomb = Instantiate(WarnBombPrefab) as GameObject;
        bomb.transform.position = transform.position + HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(4));

        bomb = Instantiate(WarnBombPrefab) as GameObject;
        bomb.transform.position = transform.position + HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(5));

    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoseGame();
            DestroyProjectile();
        }
    }

    void MoveLogic()
    {
        if (_curLifeBeatTime % MoveEveryBeat == 0)
        {
            //var moveV2 = HexagonUtils.GetVectorBySide(Direction);
            _goalMove = HexagonUtils.GetVectorBySide(Direction) + HexagonUtils.GetV2FromV3(transform.position);
            //transform.position += new Vector3(moveV2.x, moveV2.y);
        }
    }

}
