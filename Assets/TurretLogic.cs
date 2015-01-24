using UnityEngine;
using System.Collections;

public class TurretLogic : MonoBehaviour {

    [HideInInspector]
    public int TurretNumber;

    public int FireBeatTemp;
    private int _curBeat;

    public GameObject OneDirProjectile;
    public GameObject LaserProjectile;
    public GameObject HunterProjectile;
    public GameObject DoubleProjectile;
    public GameObject SnakeProjectile;
    public GameObject BombProjectile;


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

        //PatternSingleLaser(2);
        PatternTripleLaser();
        //PatternHunterAndSnake(1);
        //PatternSingleShot(1);
        //PatternTripleShot(1);
        //PatternShotAndSnake(2);
        //PatternTripleBombRound(2);
        //PatternDoubleAndSnake(1);
        //PatternDoubleAndDouble(2);
        
        

    }

    void PatternSingleLaser(int i)
    {
        // TurretNumber == i - переводится как "Если это я"
        if (TurretNumber == i && _curBeat % FireBeatTemp == 0)
        {
            FireBeatTemp = 8;
            GameObject _proj;
            _proj = Instantiate(LaserProjectile) as GameObject;
            _proj.transform.position = transform.position;
            _proj.GetComponent<WarningLogic>().Direction = ((TurretNumber + 3) % 6);
        }
    }

    void PatternTripleLaser()
    {
        PatternSingleLaser(0);
        PatternSingleLaser(2);
        PatternSingleLaser(4);
    }

    void PatternHunterAndSnake(int i)
    {
        //hunter
        PatternSingleHunter(i, 7);
        //snake
        PatternSingleSnake((i + 3) % 6, 3);
    }

    void PatternShotAndSnake(int i)
    {
        PatternSingleShot(i, 3);
        PatternSingleSnake( (i + 3) % 6, 3);
    }
    void PatternTripleBombRound(int i)
    {
        PatternSingleBomb(i, 6, 4);
        PatternSingleBomb((i+2)%6, 6, 5);
        PatternSingleBomb((i+4)%6, 6, 6);
    }
    void PatternDoubleAndSnake(int i)
    {
        PatternSingleDouble(i, 6, 2);
        PatternSingleSnake((i + 3) % 6, 3);
    }
    void PatternDoubleAndDouble(int i)
    {
        PatternSingleDouble(i, 5, 3);
        PatternSingleDouble((3 + i) % 6, 5, 3);
    }
    
    void PatternSingleShot(int i, int temp)
    {
        if (TurretNumber == i &&_curBeat % FireBeatTemp == 0)
        {
            FireBeatTemp = temp;
            GameObject _proj;
            _proj = Instantiate(OneDirProjectile) as GameObject;
            _proj.transform.position = transform.position;
            _proj.GetComponent<ProjectileLogic>().Direction = ((TurretNumber + 3) % 6);
        }
    }

    void PatternTripleShot(int i)
    {
        PatternSingleShot(i, 2);
        PatternSingleShot((i + 2) % 6, 2);
        PatternSingleShot((i + 4) % 6, 2);
    }

    void PatternSingleSnake(int i, int temp)
    {
        if (TurretNumber == i && _curBeat % FireBeatTemp == 0)
        {
            FireBeatTemp = temp;
            GameObject _proj;
            _proj = Instantiate(SnakeProjectile) as GameObject;
            _proj.transform.position = transform.position;
            _proj.GetComponent<SnakeLogic>().Direction = ((TurretNumber + 3) % 6);
        }
    }

    void PatternSingleHunter(int i, int temp)
    {
        if (TurretNumber == i && _curBeat % FireBeatTemp == 0)
        {
            FireBeatTemp = temp;
            GameObject _proj;
            _proj = Instantiate(HunterProjectile) as GameObject;
            _proj.transform.position = transform.position;
            _proj.GetComponent<HunterLogic>().Direction = ((TurretNumber + 3) % 6);
        }
    }

    void PatternSingleBomb(int i, int temp, int far)
    {
        if (TurretNumber == i && _curBeat % FireBeatTemp == 0)
        {
            FireBeatTemp = temp;
            GameObject _proj;
            _proj = Instantiate(BombProjectile) as GameObject;
            _proj.transform.position = transform.position;
            _proj.GetComponent<BombLogic>().Direction = ((TurretNumber + 3) % 6);
            _proj.GetComponent<BombLogic>().LifeBeatTime = far;
        }
    }

    void PatternSingleDouble(int i, int temp, int depth)
    {
        if (TurretNumber == 0 && _curBeat % FireBeatTemp == 0)
        {
            FireBeatTemp = temp;
            GameObject _proj;
            _proj = Instantiate(DoubleProjectile) as GameObject;
            _proj.transform.position = transform.position;
            _proj.GetComponent<DoubleLogic>().Direction = ((TurretNumber + 3) % 6);
            _proj.GetComponent<DoubleLogic>().DoubleTimes = depth;
        }
    }


}
