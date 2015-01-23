using UnityEngine;

public class CameraShake: MonoBehaviour {
    public float ShakeDist;
    public float ShakeTime;

    private Vector3 StartCameraPos;
    private GameObject camera;

    private void Start() {
        ShakeTime = 0f;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        StartCameraPos = camera.transform.position;
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

        var shakeVector = new Vector3(rx, rx);
        camera.transform.position = StartCameraPos + shakeVector;
    }
}