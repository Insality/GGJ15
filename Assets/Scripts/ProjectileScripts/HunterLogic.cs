using Assets.Scripts.BeatScripts;
using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class HunterLogic: MainProjectileLogic {
        [HideInInspector] public int Direction;
        public int HuntBeatTime;
        public int LifeBeatTime;
        public int MoveEveryBeat;
        private int _curLifeBeatTime;

        private Vector3 _goalMove;
        private Transform _playerTransform;

        // Use this for initialization
        private void Start() {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += EventSub;

            _curLifeBeatTime = 0;
            _goalMove = transform.position;
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
            HuntBeatTime--;
            if (_curLifeBeatTime%MoveEveryBeat == 0){
                // if Hunt Time!
                if (HuntBeatTime > 0){
                    Direction = HexagonUtils.GetDirectionByAngle(transform.position, _playerTransform.position);
                }
                //var moveV2 = HexagonUtils.GetVectorBySide(Direction);
                _goalMove = HexagonUtils.GetVectorBySide(Direction) + HexagonUtils.GetV2FromV3(transform.position);
                //transform.position += new Vector3(moveV2.x, moveV2.y);


                GameObject nextWall = HexagonUtils.GetObjByWorldPos(_goalMove);

                if (nextWall == null){
                    gameObject.SetActive(false);
                }

                if (nextWall != null){
                    if (nextWall.ToString().StartsWith("HexagonWall")){
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}