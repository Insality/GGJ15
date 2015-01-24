using UnityEngine;
using System.Collections;

public class TurretLogic : MonoBehaviour {

    [HideInInspector]
    public int TurretNumber;

    public int FireBeatTemp;
    private int _curBeat;

    public GameObject OneDirProjectile;
    public GameObject LaserProjectile;

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += (sender, args) => TurretBeat();
        _curBeat = 0;
	}
	
	// Update is called once per frame
	void Update () {

        Debug.DrawLine(Vector3.zero, HexagonUtils.GetPosByAxiel(0, 1));

        Debug.DrawLine(Vector3.zero, HexagonUtils.GetPosByAxiel(-2, 0));

        
	}

    void TurretBeat()
    {
        _curBeat++;

        /*
        if (_curBeat % FireBeatTemp == 0)
        {
            GameObject _proj;
            _proj = Instantiate(OneDirProjectile) as GameObject;
            _proj.transform.position = transform.position;
            _proj.GetComponent<ProjectileLogic>().Direction = ((TurretNumber + 3) % 6);
        }
         * */

        if (TurretNumber == 0 && _curBeat % FireBeatTemp == 0)
        {
            GameObject _proj;
            _proj = Instantiate(LaserProjectile) as GameObject;
            _proj.transform.position = transform.position;
            _proj.GetComponent<WarningLogic>().Direction = ((TurretNumber + 3) % 6);
        }


    }
}
