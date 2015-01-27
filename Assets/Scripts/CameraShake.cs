using UnityEngine;

namespace Assets.Scripts {
    public class CameraShake: MonoBehaviour {
        public float ShakeDist;
        public float ShakeTime;

        private Vector3 _startCameraPos;
        private new GameObject camera;

        private void Start() {
            ShakeTime = 0f;
            camera = GameObject.FindGameObjectWithTag("MainCamera");
            _startCameraPos = camera.transform.position;
        }

        // Update is called once per frame
        private void Update() {
            ShakeTime -= Time.deltaTime;
            if (ShakeTime < 0){
                ShakeTime = 0;
            }

            if (ShakeTime > 0){
                Shake();
            }
        }

        private void Shake() {
            float rx = Random.Range(-ShakeDist, ShakeDist);
            float ry = Random.Range(-ShakeDist, ShakeDist);

            var shakeVector = new Vector3(rx, ry);
            camera.transform.position = _startCameraPos + shakeVector;
        }
    }
}