using UnityEngine;

public class WarningLogic: GeneralProjectileLogic {
    public Sprite AttackSprite;
    public int Direction;
    public int LifeBeatTime;
    public float NextWarnDelay;
    public int WarningBeatTime;

    public GameObject WarningPrefab;
    private int _curLifeBeatTime;

    // Use this for initialization
    private void Start() {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent += EventSub;
        _curLifeBeatTime = 0;
    }

    private void CreateNextWarning() {
        Vector2 NextPos = HexagonUtils.GetVectorBySide(Direction) + HexagonUtils.GetV2FromV3(transform.position);
        GameObject NextWall = HexagonUtils.GetObjByWorldPos(NextPos);

        if (NextWall != null){
            if (NextWall.ToString().StartsWith("Hexagon(")){
                var nextWarn = Instantiate(WarningPrefab, NextPos, Quaternion.identity) as GameObject;
                nextWarn.GetComponent<WarningLogic>().Direction = Direction;
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoseGame();
            DestroyProjectile();
        }
    }


    // Update is called once per frame
    private void Update() {
        NextWarnDelay -= Time.deltaTime;

        if (NextWarnDelay <= 0){
            CreateNextWarning();
            NextWarnDelay = 500;
        }
    }

    public override void BeatProjectileLogic() {
        _curLifeBeatTime += 1;

        if (_curLifeBeatTime >= LifeBeatTime){
            DestroyProjectile();
        }


        if (_curLifeBeatTime >= WarningBeatTime){
            GetComponent<SpriteRenderer>().sprite = AttackSprite;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    private void MoveLogic() {
    }
}