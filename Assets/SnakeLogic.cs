using UnityEngine;

public class SnakeLogic: GeneralProjectileLogic {
    public int ChangeEveryBeat;
    public int Direction;
    public int LifeBeatTime;
    public int MoveEveryBeat;
    private int _curLifeBeatTime;

    private Vector3 _goalMove;
    private Transform _playerTransform;

    //private delegate _ev = (sender, args) => BeatProjectileLogic();

    // Use this for initialization
    private void Start() {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += EventSub;

        _curLifeBeatTime = 0;
        _goalMove = transform.position;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update() {
        Vector2 velocity = Vector2.zero;
        transform.position = Vector2.SmoothDamp(transform.position, _goalMove, ref velocity, 0.03f);
    }

    public override void BeatProjectileLogic() {
        _curLifeBeatTime += 1;

        if (_curLifeBeatTime >= LifeBeatTime){
            DestroyProjectile();
            //Destroy(gameObject);
        }

        MoveLogic();
    }

    public override void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoseGame();
            DestroyProjectile();
        }
    }

    public void MoveLogic() {
        if (_curLifeBeatTime%MoveEveryBeat == 0){
            //var moveV2 = HexagonUtils.GetVectorBySide(Direction);
            _goalMove = HexagonUtils.GetVectorBySide(Direction) + HexagonUtils.GetV2FromV3(transform.position);
            int _dirToPlayer = HexagonUtils.GetDirectionByAngle(transform.position, _playerTransform.position);

            if (_curLifeBeatTime%ChangeEveryBeat == 0){
                int side = ((_dirToPlayer - Direction) + 6)%6;
                if (side >= 3){
                    side = 5; // analog -1
                }
                else{
                    side = 1;
                }

                Direction += side;
                Direction %= 6;
            }

            GameObject NextWall = HexagonUtils.GetObjByWorldPos(_goalMove);

            if (NextWall == null)
            {
                gameObject.SetActive(false);
            }

            if (NextWall != null)
            {
                if (NextWall.ToString().StartsWith("HexagonWall"))
                {
                    gameObject.SetActive(false);
                }
            }
            //transform.position += new Vector3(moveV2.x, moveV2.y);
        }
    }
}