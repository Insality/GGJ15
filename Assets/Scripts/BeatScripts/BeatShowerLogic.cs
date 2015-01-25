using UnityEngine;

public class BeatShowerLogic: MonoBehaviour {
    public GameObject BeatLine;

    // Use this for initialization
    private void Start() {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent +=
            (sender, args)=>SpawnBeatLine();
    }


    private void SpawnBeatLine() {
        // to right
        var _beatRight = Instantiate(BeatLine) as GameObject;
        _beatRight.transform.position = transform.position + new Vector3(-0.1f, 0, 1);
        _beatRight.GetComponent<BeatLineLogic>().MoveSide = 1;

        // to left
        var _beatLeft = Instantiate(BeatLine) as GameObject;
        _beatLeft.transform.position = transform.position + new Vector3(0.1f, 0, 1);
        _beatLeft.GetComponent<BeatLineLogic>().MoveSide = -1;
    }

    private void Update() {
    }
}