using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 velocity = Vector2.zero;
        Vector3 goalV3 = player.transform.position / 2;
        goalV3.z = -10;
        transform.position = Vector2.SmoothDamp(transform.position, goalV3, ref velocity, 0.02f);
        transform.position += new Vector3(0, 0, -10);
	}
}
