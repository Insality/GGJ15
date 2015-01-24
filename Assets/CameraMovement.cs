using UnityEngine;

public class CameraMovement: MonoBehaviour {
    public GameObject player;
    public bool IsScaleCamera;

    public int CameraScale = 7;

    private BeatTracker _cameraBeatTracker;
    private float _scaleTrashold = 0.1f;
    private float _deltaHeight;
    private Camera _camera;

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
        Vector3 goalV3 = player.transform.position/2;
        goalV3.z = -10;
        transform.position = Vector2.SmoothDamp(transform.position, goalV3, ref velocity, 0.02f);
        transform.position += new Vector3(0, 0, -10);

        if (IsScaleCamera){
            // ScaleCoef - from 0 to 1, beat power in current moment
            float c1 = _cameraBeatTracker.GetBassBeat();
            float BeatLowerLimit = _cameraBeatTracker.BeatLowerLimit;
            float BeatUpperLimit = _cameraBeatTracker.BeatUpperLimit;

            float ScaleCoef = c1/(BeatLowerLimit + BeatUpperLimit);
            if (ScaleCoef <= _scaleTrashold){
                ScaleCoef = 0;
            }

            _deltaHeight = Mathf.Lerp(0f, 0.1f, ScaleCoef);
            _camera.orthographicSize = CameraScale + _deltaHeight;
            //gameObject.transform.localScale = new Vector3(ScaleHowMuch, ScaleHowMuch);
        }
    }
}