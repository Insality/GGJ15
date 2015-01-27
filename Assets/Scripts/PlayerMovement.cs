using Assets.Scripts.BeatScripts;
using UnityEngine;

namespace Assets.Scripts {
    public class PlayerMovement: MonoBehaviour {
        // Blue - вперед, Red - назад
        public GameObject BlueArrow;
        public GameObject RedArrow;
        public int SaveStayBeats;
        private int _alreadyStay;

        // moveAction: что делать на следующем бите: -1 - назад, 1 - вперед, 0 - на месте

        private int _curSide;
        private int _moveAction;
        private Vector2 _myPosition;
        private PlayerLogic _playerLogic;

        public void ResetStayCounter() {
            _alreadyStay = 0;
        }

        private void Start() {
            _curSide = 0;
            _moveAction = 0;
            _alreadyStay = 0;
            _myPosition = gameObject.transform.position;

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent +=
                (sender, args)=>PlayerBeat();

            _playerLogic = GetComponent<PlayerLogic>();
        }

        private void PlayerBeat() {
            _curSide++;
            _curSide %= 6;

            PlayerBeatMovement();

            // change bomb color:
            float percBomb = _alreadyStay/(float) SaveStayBeats;

            if (_alreadyStay == 0){
                percBomb = 0;
            }

            Color curColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f - percBomb, 1f - percBomb, curColor.a);
            if (_alreadyStay == SaveStayBeats){
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().LoseGame();
            }
        }

        private void PlayerBeatMovement() {
            Vector3 oldPos = _myPosition;

            int oldAlreadyStay = _alreadyStay;

            if (_moveAction == 0 && _playerLogic.GodModTime == 0){
                _alreadyStay++;
            }
            else{
                _alreadyStay = 0;
            }

            if (_moveAction == -1){
                _myPosition += GameUtils.GetVectorBySide((_curSide + 3)%6);
            }

            if (_moveAction == 1){
                _myPosition += GameUtils.GetVectorBySide(_curSide);
            }

            _moveAction = 0;

            GameObject objAtNextPos = GameUtils.GetObjByWorldPos(_myPosition);
            if (objAtNextPos != null){
                if (objAtNextPos.ToString().StartsWith("HexagonWall")){
                    _myPosition = oldPos;
                }
            }

            //Quaternion tmp = Quaternion.Euler(0, 0, GameUtils.GetAngleBySide(_curSide) - 30);
            //transform.rotation = tmp;

            if (objAtNextPos == null){
                _myPosition = oldPos;
                _alreadyStay = ++oldAlreadyStay;
            }
        }

        private void Update() {
            // Update control:
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)){
                _moveAction = -1;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)){
                _moveAction = 1;
            }

            foreach (Touch touch in Input.touches){
                if (touch.phase == TouchPhase.Began){
                    if (touch.position.x > 400){
                        _moveAction = 1;
                    }
                    else{
                        _moveAction = -1;
                    }
                }
            }


            // Smooth move to target:
            Vector2 velocity = Vector2.zero;
            transform.position = Vector2.SmoothDamp(transform.position, _myPosition, ref velocity, 0.04f);


            // Arrow position. (+1) потому что показывают возможное движение на след. бит
            Vector2 curPosition = transform.position;

            Vector2 bluePos = curPosition + GameUtils.GetVectorBySide((_curSide + 1)%6);
            if (_moveAction == 1){
                bluePos += GameUtils.GetVectorBySide((_curSide + 1)%6)*0.19f;
            }
            BlueArrow.transform.position = bluePos;


            Vector2 redPos = curPosition + GameUtils.GetVectorBySide((_curSide + 3 + 1)%6);
            if (_moveAction == -1){
                redPos += GameUtils.GetVectorBySide((_curSide + 3 + 1)%6)*0.19f;
            }
            RedArrow.transform.position = redPos;

            // Arrow rotation
            Quaternion blueRot = Quaternion.Euler(0, 0, GameUtils.GetAngleBySide((_curSide + 1)%6));
            BlueArrow.transform.rotation = blueRot;

            Quaternion redRot = Quaternion.Euler(0, 0, GameUtils.GetAngleBySide((_curSide + 3 + 1)%6));
            RedArrow.transform.rotation = redRot;
        }
    }
}