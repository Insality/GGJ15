using UnityEngine;

namespace Assets.Scripts {
    public class BeatLineLogic: MonoBehaviour {
        private const float Speed = 0.07f;

        public int MoveSide = 1;
        private float _lifeTime;

        // Use this for initialization
        private void Start() {
            _lifeTime = 1f;
        }

        // Update is called once per frame
        private void Update() {
            _lifeTime -= Time.deltaTime;

            if (_lifeTime < 0){
                //gameObject.SetActive(false);
                Destroy(gameObject);
            }

            Vector3 moveVector = new Vector3(Speed, 0)*MoveSide;
            transform.position += moveVector;
        }
    }
}