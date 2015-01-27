using UnityEngine;

namespace Assets.Scripts.BeatScripts {
    public class BeatScaler: MonoBehaviour {
        private const float ScaleTrashold = 0.1f;
        public bool IsBeatScaling;
        public float ScaleFrom;
        public float ScaleTo;

        private BeatTracker _cameraBeatTracker;

        // Use this for initialization
        private void Start() {
            _cameraBeatTracker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>();
        }

        // Update is called once per frame
        private void Update() {
            if (IsBeatScaling){
                // ScaleCoef - from 0 to 1, beat power in current moment
                float c1 = _cameraBeatTracker.GetBassBeat();
                float beatLowerLimit = _cameraBeatTracker.BeatLowerLimit;
                float beatUpperLimit = _cameraBeatTracker.BeatUpperLimit;

                float scaleCoef = c1/(beatLowerLimit + beatUpperLimit);
                if (scaleCoef <= ScaleTrashold){
                    scaleCoef = 0;
                }

                float scaleHowMuch = Mathf.Lerp(ScaleFrom, ScaleTo, scaleCoef);
                gameObject.transform.localScale = new Vector3(scaleHowMuch, scaleHowMuch);
            }
        }
    }
}