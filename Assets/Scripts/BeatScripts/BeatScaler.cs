using UnityEngine;
using System.Collections;

public class BeatScaler : MonoBehaviour {

    public bool IsBeatScaling;
    public float ScaleFrom;
    public float ScaleTo;

    private const float _scaleTrashold = 0.1f;

    private BeatTracker _cameraBeatTracker;

	// Use this for initialization
	void Start () {

	    _cameraBeatTracker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>();
	}
	
	// Update is called once per frame
	void Update () {
        if (IsBeatScaling) {
            // ScaleCoef - from 0 to 1, beat power in current moment
            float c1 = _cameraBeatTracker.GetBassBeat();
            float BeatLowerLimit = _cameraBeatTracker.BeatLowerLimit;
            float BeatUpperLimit = _cameraBeatTracker.BeatUpperLimit;

            float ScaleCoef = c1 / (BeatLowerLimit + BeatUpperLimit);
            if (ScaleCoef <= _scaleTrashold) {
                ScaleCoef = 0;
            }

            float ScaleHowMuch = Mathf.Lerp(ScaleFrom, ScaleTo, ScaleCoef);
            gameObject.transform.localScale = new Vector3(ScaleHowMuch, ScaleHowMuch);
        }
	}
}
