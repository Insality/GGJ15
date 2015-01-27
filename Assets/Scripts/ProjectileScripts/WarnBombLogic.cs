using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class WarnBombLogic: MainProjectileLogic {
        public Sprite AttackSprite;
        public int WarningBeatTime;

        public void Start() {
            base.Start();
        }


        public override void BeatProjectileLogic() {
            CurLifeBeatTime += 1;

            if (CurLifeBeatTime >= LifeBeatTime){
                DestroyProjectile();
            }


            if (CurLifeBeatTime >= WarningBeatTime){
                GetComponent<SpriteRenderer>().sprite = AttackSprite;
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }

        private void MoveLogic() {
        }
    }
}