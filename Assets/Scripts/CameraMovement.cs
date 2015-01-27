using Assets.Scripts.BeatScripts;
using UnityEngine;

namespace Assets.Scripts {
    public class CameraMovement: MonoBehaviour {
        private const float ScaleTrashold = 0.1f;
        public GameObject Background;
        public int CameraScale = 7;
        public bool IsScaleCamera;
        public GameObject Player;
        private Camera _camera;

        private BeatTracker _cameraBeatTracker;
        private float _deltaHeight;

        // Use this for initialization
        private void Start() {
            IsScaleCamera = true;
            _cameraBeatTracker = GetComponent<BeatTracker>();
            _deltaHeight = 0;
            _camera = GetComponent<Camera>();
        }

        // Update is called once per frame
        private void Update() {
            Vector2 velocity = Vector2.zero;
            Vector3 goalV3 = Player.transform.position/2;
            goalV3.z = -10;
            transform.position = Vector2.SmoothDamp(transform.position, goalV3, ref velocity, 0.02f);
            transform.position += new Vector3(0, 0, -10);

            Background.transform.position = transform.position*-1;
            Background.transform.position += new Vector3(0, 0, 20);


            if (IsScaleCamera){
                // ScaleCoef - from 0 to 1, beat power in current moment
                float c1 = _cameraBeatTracker.GetBassBeat();
                float beatLowerLimit = _cameraBeatTracker.BeatLowerLimit;
                float beatUpperLimit = _cameraBeatTracker.BeatUpperLimit;

                float scaleCoef = c1/(beatLowerLimit + beatUpperLimit);
                if (scaleCoef <= ScaleTrashold){
                    scaleCoef = 0;
                }

                _deltaHeight = Mathf.Lerp(0f, 0.1f, scaleCoef);
                _camera.orthographicSize = CameraScale + _deltaHeight;
                //gameObject.transform.localScale = new Vector3(ScaleHowMuch, ScaleHowMuch);
            }
        }
    }
}