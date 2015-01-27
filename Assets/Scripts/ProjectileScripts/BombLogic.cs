using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class BombLogic: MainProjectileLogic {
        public AudioClip Explo;

        public GameObject WarnBombPrefab;
        private Vector3 _goalMove;

        // Use this for initialization
        public void Start() {
            base.Start();
            _goalMove = transform.position;
        }

        // Update is called once per frame
        private void Update() {
            Vector2 velocity = Vector2.zero;
            transform.position = Vector2.SmoothDamp(transform.position, _goalMove, ref velocity, 0.03f);
        }

        public override void BeatProjectileLogic() {
            CurLifeBeatTime += 1;

            if (CurLifeBeatTime >= LifeBeatTime){
                Bomb();
                DestroyProjectile();
            }

            MoveLogic();
        }

        private void Bomb() {
            var bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(0));

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(1));

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(2));

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(3));

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(4));

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(5));

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(0)*2);

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(1)*2);

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(2)*2);

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(3)*2);

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(4)*2);

            bomb = Instantiate(WarnBombPrefab) as GameObject;
            bomb.transform.position = transform.position + GameUtils.GetV3FromV2(GameUtils.GetVectorBySide(5)*2);

            AudioSource.PlayClipAtPoint(Explo, transform.position, 0.5f);
        }

        private void MoveLogic() {
            if (CurLifeBeatTime%MoveEveryBeat == 0){
                _goalMove = GameUtils.GetVectorBySide(Direction) + GameUtils.GetV2FromV3(transform.position);
            }
        }
    }
}