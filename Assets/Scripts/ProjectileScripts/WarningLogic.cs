using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class WarningLogic: MainProjectileLogic {
        public Sprite AttackSprite;
        public float NextWarnDelay;
        public int WarningBeatTime;

        public GameObject WarningPrefab;

        // Use this for initialization
        public void Start() {
            base.Start();
        }

        private void CreateNextWarning() {
            Vector2 nextPos = GameUtils.GetVectorBySide(Direction) + GameUtils.GetV2FromV3(transform.position);
            GameObject nextWall = GameUtils.GetObjByWorldPos(nextPos);

            if (nextWall != null){
                if (nextWall.ToString().StartsWith("Hexagon(")){
                    var nextWarn = Instantiate(WarningPrefab, nextPos, Quaternion.identity) as GameObject;
                    nextWarn.GetComponent<WarningLogic>().Direction = Direction;
                }
            }
        }

        private void Update() {
            NextWarnDelay -= Time.deltaTime;

            if (NextWarnDelay <= 0){
                CreateNextWarning();
                NextWarnDelay = 500;
            }
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
    }
}