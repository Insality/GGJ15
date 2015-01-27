using UnityEngine;

namespace Assets.Scripts.ProjectileScripts {
    public class ProjectileLogic: MainProjectileLogic {

        private Vector3 _goalMove;

        public void Start() {
            base.Start();
            _goalMove = transform.position;
        }

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
            if (CurLifeBeatTime%MoveEveryBeat == 0){
                _goalMove = GameUtils.GetVectorBySide(Direction) + GameUtils.GetV2FromV3(transform.position);
            }
        }
    }
}