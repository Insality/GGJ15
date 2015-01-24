using UnityEngine;

public class PlayerMovement: MonoBehaviour {
    // Blue - вперед, Red - назад
    public GameObject BlueArrow;
    public GameObject RedArrow;

    // moveAction: что делать на следующем бите: -1 - назад, 1 - вперед, 0 - на месте

    private int _curSide;
    private int _moveAction;
    private Vector2 _myPosition;

    private void Start() {
        _curSide = 0;
        _moveAction = 0;
        _myPosition = gameObject.transform.position;

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent +=
            (sender, args)=>PlayerBeat();
    }

    private void PlayerBeat() {

        _curSide++;
        _curSide %= 6;

        PlayerBeatMovement();
    }

    private void PlayerBeatMovement() {
        if (_moveAction == -1){
            _myPosition += HexagonUtils.GetVectorBySide((_curSide + 3)%6);
        }

        if (_moveAction == 1){
            _myPosition += HexagonUtils.GetVectorBySide(_curSide);
        }
        _moveAction = 0;
    }

    private void Update() {
        // Update control:
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)){
            _moveAction = -1;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)){
            _moveAction = 1;
        }

        // Smooth move to target:
        Vector2 velocity = Vector2.zero;
        transform.position = Vector2.SmoothDamp(transform.position, _myPosition, ref velocity, 0.04f);


        // Arrow position. (+1) потому что показывают возможное движение на след. бит
        Vector2 _curPosition = transform.position;

        Vector2 bluePos = _curPosition + HexagonUtils.GetVectorBySide((_curSide + 1)%6);
        BlueArrow.transform.position = bluePos;

        Vector2 redPos = _curPosition + HexagonUtils.GetVectorBySide((_curSide + 3 + 1)%6);
        RedArrow.transform.position = redPos;

        // Arrow rotation
        Quaternion blueRot = Quaternion.Euler(0, 0, HexagonUtils.GetAngleBySide((_curSide + 1)%6));
        BlueArrow.transform.rotation = blueRot;

        Quaternion redRot = Quaternion.Euler(0, 0, HexagonUtils.GetAngleBySide((_curSide + 3 + 1)%6));
        RedArrow.transform.rotation = redRot;
    }
}