using UnityEngine;
using System.Collections;

public class BeatLineLogic : MonoBehaviour {

    private float _lifeTime;
    private const float _speed = 0.07f;

    public int MoveSide = 1;

	// Use this for initialization
	void Start () {
        _lifeTime = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime < 0){
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

        var moveVector = new Vector3(_speed, 0) * MoveSide;
        transform.position += moveVector;

	}
}
