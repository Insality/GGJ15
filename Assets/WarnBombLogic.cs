﻿using UnityEngine;

public class WarnBombLogic: GeneralProjectileLogic {
    public Sprite AttackSprite;
    public int Direction;
    public int LifeBeatTime;
    public int WarningBeatTime;
    private int _curLifeBeatTime;

    // Use this for initialization
    private void Start() {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += EventSub;
        _curLifeBeatTime = 0;
    }


    public override void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoseGame();
            DestroyProjectile();
        }
    }


    // Update is called once per frame
    private void Update() {
    }

    public override void BeatProjectileLogic() {
        _curLifeBeatTime += 1;

        if (_curLifeBeatTime >= LifeBeatTime){
            DestroyProjectile();
        }


        if (_curLifeBeatTime >= WarningBeatTime){
            GetComponent<SpriteRenderer>().sprite = AttackSprite;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    private void MoveLogic() {
    }
}