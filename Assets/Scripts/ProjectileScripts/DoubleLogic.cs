using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class DoubleLogic: MainProjectileLogic {
        public int DoublePeriod;

        public GameObject DoublePrefab;
        public int DoubleTimes;

        private Vector3 _goalMove;

        // Use this for initialization
        public void Start() {
            base.Start();
            CurLifeBeatTime = 0;
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
                DestroyProjectile();
            }

            MoveLogic();
        }

        public void MoveLogic() {
            if (CurLifeBeatTime%MoveEveryBeat == MoveEveryBeat - 1){
                if (DoubleTimes > 0 && ((CurLifeBeatTime%DoublePeriod) == DoublePeriod - 1)){
                    DoubleTimes--;
                    var newDouble = Instantiate(DoublePrefab) as GameObject;
                    newDouble.GetComponent<DoubleLogic>().Direction = (Direction + 1)%6;
                    newDouble.GetComponent<DoubleLogic>().DoubleTimes = DoubleTimes;
                    newDouble.GetComponent<DoubleLogic>().BeatProjectileLogic();

                    newDouble = Instantiate(DoublePrefab) as GameObject;
                    newDouble.GetComponent<DoubleLogic>().Direction = (Direction + 5)%6;
                    newDouble.GetComponent<DoubleLogic>().DoubleTimes = DoubleTimes;
                    newDouble.GetComponent<DoubleLogic>().BeatProjectileLogic();
                }
                _goalMove = GameUtils.GetVectorBySide(Direction) + GameUtils.GetV2FromV3(transform.position);


                GameObject nextWall = GameUtils.GetObjByWorldPos(_goalMove);

                if (nextWall == null){
                    DestroyProjectile();
                }

                if (nextWall != null){
                    if (nextWall.ToString().StartsWith("HexagonWall")){
                        DestroyProjectile();
                    }
                }
            }
        }
    }
}