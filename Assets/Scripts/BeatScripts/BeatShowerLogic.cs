using UnityEngine;
using System.Collections;

public class BeatShowerLogic : MonoBehaviour {


    public GameObject BeatLine;

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += (sender, args) => SpawnBeatLine();
	}


    void SpawnBeatLine(){
        // to right
        GameObject _beatRight = Instantiate(BeatLine) as GameObject;
        _beatRight.transform.position = transform.position + new Vector3(-0.1f, 0, 1);
        _beatRight.GetComponent<BeatLineLogic>().MoveSide = 1;

        // to left
        GameObject _beatLeft = Instantiate(BeatLine) as GameObject;
        _beatLeft.transform.position = transform.position + new Vector3(0.1f, 0, 1);
        _beatLeft.GetComponent<BeatLineLogic>().MoveSide = -1;
    }

	void Update () {
	
	}
}
