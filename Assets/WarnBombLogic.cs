using UnityEngine;
using System.Collections;

public class WarnBombLogic : GeneralProjectileLogic {

    public int LifeBeatTime;
    private int _curLifeBeatTime;
    public int WarningBeatTime;
    public int Direction;

    public Sprite AttackSprite;

    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += EventSub;
        _curLifeBeatTime = 0;
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoseGame();
            DestroyProjectile();
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    public override void BeatProjectileLogic()
    {
        _curLifeBeatTime += 1;

        if (_curLifeBeatTime >= LifeBeatTime)
        {
            DestroyProjectile();
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
