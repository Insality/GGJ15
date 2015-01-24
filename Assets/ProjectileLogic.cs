using UnityEngine;
using System.Collections;

public class ProjectileLogic : MonoBehaviour {

    public int LifeBeatTime;
    private int _curLifeBeatTime;
    public int MoveEveryBeat;
    public int Direction;

    private Vector3 _goalMove;

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += (sender, args) => BeatProjectileLogic();

        _curLifeBeatTime = 0;
        _goalMove = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 velocity = Vector2.zero;
        transform.position = Vector2.SmoothDamp(transform.position, _goalMove, ref velocity, 0.03f);
	}

    void BeatProjectileLogic(){
        _curLifeBeatTime += 1;

        if (_curLifeBeatTime >= LifeBeatTime){
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent -= (sender, args) => BeatProjectileLogic();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

        MoveLogic();
    }

    void MoveLogic(){
        if (_curLifeBeatTime % MoveEveryBeat == 0)
        {
            //var moveV2 = HexagonUtils.GetVectorBySide(Direction);
            _goalMove = HexagonUtils.GetVectorBySide(Direction) + HexagonUtils.GetV2FromV3(transform.position);
            //transform.position += new Vector3(moveV2.x, moveV2.y);
        }
    }


}
