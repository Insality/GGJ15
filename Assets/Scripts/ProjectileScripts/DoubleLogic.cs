using Assets.Scripts.BeatScripts;
using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class DoubleLogic: MainProjectileLogic {
        public int Direction;
        public int DoublePeriod;


        public GameObject DoublePrefab;
        public int DoubleTimes;
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

        public void MoveLogic() {
            if (_curLifeBeatTime%MoveEveryBeat == MoveEveryBeat - 1){
                if (DoubleTimes > 0 && ((_curLifeBeatTime%DoublePeriod) == DoublePeriod - 1)){
                    DoubleTimes--;
                    var newDouble = Instantiate(DoublePrefab) as GameObject;
                    newDouble.GetComponent<DoubleLogic>().Direction = (Direction + 1)%6;
                    newDouble.GetComponent<DoubleLogic>().DoubleTimes = DoubleTimes;
                    newDouble.GetComponent<DoubleLogic>().BeatProjectileLogic();

                    newDouble = Instantiate(DoublePrefab) as GameObject;
                    newDouble.GetComponent<DoubleLogic>().Direction = (Direction + 5)%6;
                    newDouble.GetComponent<DoubleLogic>().DoubleTimes = DoubleTimes;
                    newDouble.GetComponent<DoubleLogic>().BeatProjectileLogic();

                    //Direction = (Direction + 5) % 6;
                }
                //var moveV2 = GameUtils.GetVectorBySide(Direction);
                _goalMove = GameUtils.GetVectorBySide(Direction) + GameUtils.GetV2FromV3(transform.position);
                //transform.position += new Vector3(moveV2.x, moveV2.y);


                GameObject nextWall = GameUtils.GetObjByWorldPos(_goalMove);

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