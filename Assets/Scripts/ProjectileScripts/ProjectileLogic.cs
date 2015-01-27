using Assets.Scripts.BeatScripts;
using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class ProjectileLogic: MainProjectileLogic {
        public int Direction;
        public int LifeBeatTime;
        public int MoveEveryBeat;
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
                DestroyProjectile();
            }

            MoveLogic();
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