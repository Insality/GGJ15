using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class HunterLogic: MainProjectileLogic {
        public int HuntBeatTime;

        private Vector3 _goalMove;
        private Transform _playerTransform;

        // Use this for initialization
        public void Start() {
            base.Start();
            _goalMove = transform.position;
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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

        private void MoveLogic() {
            HuntBeatTime--;
            if (CurLifeBeatTime%MoveEveryBeat == 0){
                // if Hunt Time!
                if (HuntBeatTime > 0){
                    Direction = GameUtils.GetDirectionByAngle(transform.position, _playerTransform.position);
                }
                //var moveV2 = GameUtils.GetVectorBySide(Direction);
                _goalMove = GameUtils.GetVectorBySide(Direction) + GameUtils.GetV2FromV3(transform.position);
                //transform.position += new Vector3(moveV2.x, moveV2.y);


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