using System;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.ProjectileScripts {
    public abstract class MainProjectileLogic: MonoBehaviour {
        public Event EventHandler;
        [HideInInspector]
        public LevelManager LevelManager;

        public int Direction;
        public int LifeBeatTime;
        public int MoveEveryBeat;
        protected int CurLifeBeatTime;

        public abstract void BeatProjectileLogic();

        public virtual void Start() {
            LevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            LevelManager.Projectiles.Add(this);

            CurLifeBeatTime = 0;
        }

        public virtual void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player"){
                LevelManager.LoseGame();
                DestroyProjectile();
            }
        }

        public void EventSub(Object sender, EventArgs e) {
            BeatProjectileLogic();
        }

        public virtual void DestroyProjectile() {
            LevelManager.Projectiles.Remove(this);
            Destroy(gameObject);
        }
    }
}