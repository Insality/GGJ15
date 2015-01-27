using Assets.Scripts.BeatScripts;
using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class BombLogic: MainProjectileLogic {
        public int Direction;
        public AudioClip Explo;
        public int LifeBeatTime;
        public int MoveEveryBeat;


        public GameObject WarnBombPrefab;
        private int _curLifeBeatTime;
        private Vector3 _goalMove;

        // Use this for initialization
        private void Start() {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += EventSub;

            _curLifeBeatTime = 0;
            _goalMove = transform.position;
        }

        // Update is called once per frame
        private void Update() {
            Vector2 velocity = Vector2.zero;
            transform.position = Vector2.SmoothDamp(transform.position, _goalMove, ref velocity, 0.03f);
        }

        public override void BeatProjectileLogic() {
            _curLifeBeatTime += 1;

            if (_curLifeBeatTime >= LifeBeatTime){
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


        public override void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player"){
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoseGame();
                DestroyProjectile();
            }
        }

        private void MoveLogic() {
            if (_curLifeBeatTime%MoveEveryBeat == 0){
                //var moveV2 = GameUtils.GetVectorBySide(Direction);
                _goalMove = GameUtils.GetVectorBySide(Direction) + GameUtils.GetV2FromV3(transform.position);
                //transform.position += new Vector3(moveV2.x, moveV2.y);
            }
        }
    }
}