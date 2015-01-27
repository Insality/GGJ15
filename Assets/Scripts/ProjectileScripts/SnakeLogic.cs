using Assets.Scripts.BeatScripts;
using Assets.Scripts.ProjectileScripts;
using UnityEngine;

namespace Assets.Scripts {
    public class SnakeLogic: MainProjectileLogic {
        public int ChangeEveryBeat;

        private Vector3 _goalMove;
        private Transform _playerTransform;

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
                return;
            }
            MoveLogic();
        }

        public void MoveLogic() {
            if (CurLifeBeatTime%MoveEveryBeat == 0){
                _goalMove = GameUtils.GetVectorBySide(Direction) + GameUtils.GetV2FromV3(transform.position);
                int dirToPlayer = GameUtils.GetDirectionByAngle(transform.position, _playerTransform.position);

                if (CurLifeBeatTime%ChangeEveryBeat == 0){
                    int side = ((dirToPlayer - Direction) + 6)%6;
                    side = side >= 3 ? 5 : 1;

                    Direction += side;
                    Direction %= 6;
                }

                var nextWall = GameUtils.GetObjByWorldPos(_goalMove);

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