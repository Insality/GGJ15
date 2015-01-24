using System;
using UnityEngine;
using Object = System.Object;

public abstract class GeneralProjectileLogic: MonoBehaviour {
    public Event EventHandler;

    public abstract void BeatProjectileLogic();

    public virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoseGame();
            DestroyProjectile();
        }
    }

    public void EventSub(Object sender, EventArgs e) {
        BeatProjectileLogic();
    }

    public virtual void DestroyProjectile() {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent -= EventSub;
        //gameObject.SetActive(false);

        Destroy(gameObject);
    }
}