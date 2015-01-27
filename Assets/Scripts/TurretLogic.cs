using Assets.Scripts.BeatScripts;
using Assets.Scripts.ProjectileScripts;
using UnityEngine;

namespace Assets.Scripts {
    public class TurretLogic: MonoBehaviour {
        public GameObject BombProjectile;
        public GameObject DoubleProjectile;
        public int FireBeatTemp;
        public GameObject HunterProjectile;
        public AudioClip LaserClip;
        public GameObject LaserProjectile;
        public GameObject OneDirProjectile;
        public GameObject SnakeProjectile;

        [HideInInspector] public int TurretNumber;
        private int _curBeat;

        private LevelManager _levelManagerScript;

        // Use this for initialization
        private void Start() {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent +=
                (sender, args)=>TurretBeat();
            _curBeat = 1;

            _levelManagerScript = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        }

        // Update is called once per frame
        private void Update() {
            Debug.DrawLine(Vector3.zero, HexagonUtils.GetPosByAxiel(0, 1));

            Debug.DrawLine(Vector3.zero, HexagonUtils.GetPosByAxiel(-2, 0));
        }

        private void TurretBeat() {
            _curBeat++;

            // Firing::
            if (_levelManagerScript.FreeTime == 0){
                if (_levelManagerScript.CurLevel == 1){
                    if (_levelManagerScript.CurrentTactic == 0){
                        PattertnSingleShotRound();
                    }
                    if (_levelManagerScript.CurrentTactic == 1){
                        PatternSingleLaser(Random.Range(0, 6));
                    }
                    if (_levelManagerScript.CurrentTactic == 2){
                        PatternSingleSnake(_levelManagerScript.CurTurretActive, 3);
                    }
                }

                if (_levelManagerScript.CurLevel == 2){
                    if (_levelManagerScript.CurrentTactic == 0){
                        PatternTripleLaser();
                    }
                    if (_levelManagerScript.CurrentTactic == 1){
                        PattertnSingleShotRoundPair();
                    }
                    if (_levelManagerScript.CurrentTactic == 2){
                        PatternSingleHunter(_levelManagerScript.CurTurretActive, 7);
                    }
                }

                if (_levelManagerScript.CurLevel == 3){
                    if (_levelManagerScript.CurrentTactic == 0){
                        PatternSingleBomb(_levelManagerScript.CurTurretActive, 3, Random.Range(4, 6));
                    }
                    if (_levelManagerScript.CurrentTactic == 1){
                        PatternSingleHunter(_levelManagerScript.CurTurretActive, 7);
                        PatternSingleShot((_levelManagerScript.CurTurretActive + 1)%6, 2);
                        PatternSingleShot((_levelManagerScript.CurTurretActive + 5)%6, 2);
                    }
                    if (_levelManagerScript.CurrentTactic == 2){
                        PatternSingleDouble(_levelManagerScript.CurTurretActive, 6, 2);
                    }
                }

                if (_levelManagerScript.CurLevel == 4){
                    if (_levelManagerScript.CurrentTactic == 0){
                        PatternSingleSnake(_levelManagerScript.CurTurretActive, 2);
                        PatternSingleSnake((_levelManagerScript.CurTurretActive + 3)%6, 2);
                    }
                    if (_levelManagerScript.CurrentTactic == 1){
                        PatternDoubleRound();
                    }
                    if (_levelManagerScript.CurrentTactic == 2){
                        PatternTripleBombRound(_levelManagerScript.CurTurretActive);
                    }
                }

                if (_levelManagerScript.CurLevel == 5){
                    if (_levelManagerScript.CurrentTactic == 0){
                        PatternHunterAndSnake(_levelManagerScript.CurTurretActive);
                    }
                    if (_levelManagerScript.CurrentTactic == 1){
                        PatternTripleAllLaser();
                    }
                    if (_levelManagerScript.CurrentTactic == 2){
                        PatternSingleShotNoCd((_levelManagerScript.CurTurretActive)%6, 8);
                        PatternSingleShotNoCd((_levelManagerScript.CurTurretActive + 1)%6, 8);
                        PatternSingleShotNoCd((_levelManagerScript.CurTurretActive + 2)%6, 8);
                        PatternSingleShotNoCd((_levelManagerScript.CurTurretActive + 3)%6, 8);
                        PatternSingleShotNoCd((_levelManagerScript.CurTurretActive + 4)%6, 8);
                        PatternSingleShotNoCd((_levelManagerScript.CurTurretActive + 5)%6, 8);
                    }
                }

                if (_levelManagerScript.CurLevel == 6){
                    if (_levelManagerScript.CurrentTactic == 0){
                        PatternOmegaDouble(_levelManagerScript.CurTurretActive);
                    }
                    if (_levelManagerScript.CurrentTactic == 1){
                        PatternSingleHunter(_levelManagerScript.CurTurretActive, 8);
                        PatternSingleHunter((_levelManagerScript.CurTurretActive + 3)%6, 8);
                    }
                    if (_levelManagerScript.CurrentTactic == 2){
                        PatternDoubleAndSnake(_levelManagerScript.CurTurretActive);
                    }
                }


                //            PatternSingleLaser(2);
                //            PatternTripleLaser();
                //PatternHunterAndSnake(1);
                //PatternSingleShot(1);
                //            PatternTripleShot(1);
                //PatternShotAndSnake(2);
                //PatternTripleBombRound(2);
                //PatternDoubleAndSnake(1);
                //PatternOmegaDouble(2);
                //PattertnSingleShotRound();
                //PatternLaserAllSide(2);
                //PattertnSingleShotRoundPair();
                //PatternSingleHunter(2, 8);
                //PatternDoubleRound();
                //PatternTripleAllLaser();
            }
        }

        private void PatternSingleLaser(int i) {
            // TurretNumber == i - переводится как "Если это я"
            if (TurretNumber == i && _curBeat%FireBeatTemp == 0){
                FireBeatTemp = 8;
                var proj = Instantiate(LaserProjectile) as GameObject;
                proj.transform.position = transform.position +
                                          HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide((TurretNumber + 3)%6));
                proj.GetComponent<WarningLogic>().Direction = ((TurretNumber + 3)%6);

                AudioSource.PlayClipAtPoint(LaserClip, transform.position, 0.3f);
            }
        }

        private void PatternLaserAllSide(int i) {
            if (TurretNumber == i && _curBeat%FireBeatTemp == 0){
                FireBeatTemp = 8;
                var proj = Instantiate(LaserProjectile) as GameObject;
                proj.transform.position = transform.position +
                                          HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide((TurretNumber + 3)%6));
                proj.GetComponent<WarningLogic>().Direction = ((TurretNumber + 3)%6);

                proj = Instantiate(LaserProjectile) as GameObject;
                proj.transform.position = transform.position +
                                          HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide((TurretNumber + 4)%6));
                proj.GetComponent<WarningLogic>().Direction = ((TurretNumber + 4)%6);

                proj = Instantiate(LaserProjectile) as GameObject;
                proj.transform.position = transform.position +
                                          HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide((TurretNumber + 2)%6));
                proj.GetComponent<WarningLogic>().Direction = ((TurretNumber + 2)%6);

                AudioSource.PlayClipAtPoint(LaserClip, transform.position);
            }
        }

        private void PatternTripleLaser() {
            PatternSingleLaser(0);
            PatternSingleLaser(2);
            PatternSingleLaser(4);
        }

        private void PatternTripleAllLaser() {
            PatternLaserAllSide(0);
            PatternLaserAllSide(2);
            PatternLaserAllSide(4);
        }

        private void PatternHunterAndSnake(int i) {
            //hunter
            PatternSingleHunter(i, 7);
            //snake
            PatternSingleSnake((i + 3)%6, 3);
        }

        private void PatternShotAndSnake(int i) {
            PatternSingleShot(i, 3);
            PatternSingleSnake((i + 3)%6, 3);
        }

        private void PatternTripleBombRound(int i) {
            PatternSingleBomb(i, 6, 4);
            PatternSingleBomb((i + 2)%6, 6, 5);
            PatternSingleBomb((i + 4)%6, 6, 6);
        }

        private void PattertnSingleShotRound() {
            PatternSingleShotNoCd(_curBeat%6, 2);
        }

        private void PattertnSingleShotRoundPair() {
            PatternSingleShotNoCd(_curBeat%6, 4);
            PatternSingleShotNoCd((_curBeat + 3)%6, 4);
        }

        private void PatternDoubleRound() {
            PatternSingleDouble(_curBeat%6, 1, 1);
        }


        // hard!
        private void PatternDoubleAndSnake(int i) {
            PatternSingleDouble(i, 8, 2);
            PatternSingleSnake((i + 3)%6, 3);
        }

        private void PatternOmegaDouble(int i) {
            PatternSingleDouble(i, 17, 3);
            //PatternSingleDouble((3 + i)%6, 5, 3);
        }

        private void PatternSingleShotNoCd(int i, int temp) {
            if (TurretNumber == i){
                FireBeatTemp = temp;
                var proj = Instantiate(OneDirProjectile) as GameObject;
                proj.transform.position = transform.position +
                                          HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(((TurretNumber + 3)%6)));
                proj.GetComponent<ProjectileLogic>().Direction = ((TurretNumber + 3)%6);
            }
        }

        private void PatternSingleShot(int i, int temp) {
            if (TurretNumber == i && _curBeat%FireBeatTemp == 0){
                PatternSingleShotNoCd(i, temp);
            }
        }

        private void PatternTripleShot(int i) {
            PatternSingleShot(i, 2);
            PatternSingleShot((i + 2)%6, 2);
            PatternSingleShot((i + 4)%6, 2);
        }

        private void PatternSingleSnake(int i, int temp) {
            if (TurretNumber == i && _curBeat%FireBeatTemp == 0){
                FireBeatTemp = temp;
                var proj = Instantiate(SnakeProjectile) as GameObject;
                proj.transform.position = transform.position +
                                          HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(((TurretNumber + 3)%6)));
                proj.GetComponent<SnakeLogic>().Direction = ((TurretNumber + 3)%6);
            }
        }

        private void PatternSingleHunter(int i, int temp) {
            if (TurretNumber == i && _curBeat%FireBeatTemp == 0){
                FireBeatTemp = temp;
                var proj = Instantiate(HunterProjectile) as GameObject;
                proj.transform.position = transform.position +
                                          HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(((TurretNumber + 3)%6)));
                proj.GetComponent<HunterLogic>().Direction = ((TurretNumber + 3)%6);
            }
        }

        private void PatternSingleBomb(int i, int temp, int far) {
            if (TurretNumber == i && _curBeat%FireBeatTemp == 0){
                FireBeatTemp = temp;
                var proj = Instantiate(BombProjectile) as GameObject;
                proj.transform.position = transform.position +
                                          HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(((TurretNumber + 3)%6)));
                proj.GetComponent<BombLogic>().Direction = ((TurretNumber + 3)%6);
                proj.GetComponent<BombLogic>().LifeBeatTime = far;
            }
        }

        private void PatternSingleDouble(int i, int temp, int depth) {
            if (TurretNumber == i && _curBeat%FireBeatTemp == 0){
                FireBeatTemp = temp;
                var proj = Instantiate(DoubleProjectile) as GameObject;
                proj.transform.position = transform.position +
                                          HexagonUtils.GetV3FromV2(HexagonUtils.GetVectorBySide(((TurretNumber + 3)%6)));
                proj.GetComponent<DoubleLogic>().Direction = ((TurretNumber + 3)%6);
                proj.GetComponent<DoubleLogic>().DoubleTimes = depth;
            }
        }
    }
}