using UnityEngine;

namespace Assets.Scripts {
    public class LevelHexagonCreator: MonoBehaviour {
        public int BoardSize;

        public GameObject HexagonTile;
        public GameObject HexagonTileWall;
        public GameObject Turret;

        public Sprite[] TurretsSprites;
        private Vector2[] _corners;

        // Use this for initialization
        private void Start() {
            _corners = new Vector2[6];
            CreateMap();
        }

        private void CreateMap() {
            GameObject curHexTile;
            for (int i = 0; i < BoardSize; i++){
                curHexTile = Instantiate(HexagonTile) as GameObject;
                curHexTile.transform.position = HexagonUtils.GetVectorBySide(4)*i;


                // corner
                if (i == BoardSize - 1){
                    _corners[4] = curHexTile.transform.position;
                }

                for (int j = 1; j < BoardSize; j++){
                    // to 2 side:
                    var curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                    Vector2 v2Temp = HexagonUtils.GetVectorBySide(2)*j;
                    curHexTileSecond.transform.position = curHexTile.transform.position +
                                                          new Vector3(v2Temp.x, v2Temp.y);

                    // To 0 side
                    var curHexTileSecond2 = Instantiate(HexagonTile) as GameObject;
                    v2Temp = HexagonUtils.GetVectorBySide(0)*j;
                    curHexTileSecond2.transform.position = curHexTile.transform.position +
                                                           new Vector3(v2Temp.x, v2Temp.y);


                    // corners 
                    if (i == BoardSize - 1 && j == BoardSize - 1){
                        _corners[3] = curHexTileSecond.transform.position;
                        _corners[5] = curHexTileSecond2.transform.position;
                    }

                    if (i == 0 && j == BoardSize - 1){
                        _corners[2] = curHexTileSecond.transform.position;
                        _corners[0] = curHexTileSecond2.transform.position;
                    }
                }
            }

            // upper right part
            for (int i = 1; i < BoardSize; i++){
                curHexTile = Instantiate(HexagonTile) as GameObject;
                curHexTile.transform.position = HexagonUtils.GetVectorBySide(1)*i;

                if (i == BoardSize - 1){
                    _corners[1] = curHexTile.transform.position;
                }

                for (int j = 1; j < BoardSize - i; j++){
                    // to 2 side:
                    var curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                    Vector2 v2Temp = HexagonUtils.GetVectorBySide(2)*j;
                    curHexTileSecond.transform.position = curHexTile.transform.position +
                                                          new Vector3(v2Temp.x, v2Temp.y);

                    // To 0 side
                    curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                    v2Temp = HexagonUtils.GetVectorBySide(0)*j;
                    curHexTileSecond.transform.position = curHexTile.transform.position +
                                                          new Vector3(v2Temp.x, v2Temp.y);
                }
            }

            foreach (Vector2 corner in _corners){
                Destroy(HexagonUtils.GetObjByWorldPos(corner));
            }


            // Turret creation
            for (int i = 0; i < _corners.Length; i++){
                var curTurret = Instantiate(Turret) as GameObject;
                curTurret.transform.position = _corners[i];

                curTurret.GetComponent<TurretLogic>().TurretNumber = i;
                curTurret.GetComponent<SpriteRenderer>().sprite = TurretsSprites[i];

                // TODO: do it!
                curTurret.transform.localRotation =
                    Quaternion.Euler(new Vector3(0, 0, HexagonUtils.GetAngleBySide(i) - 90));
            }

            // Wall Creation

            //        HexagonUtils.ReplaceWith(HexagonTileWall, -2, 0);
        }

        public void ClearWalls() {
            GameObject[] walls = GameObject.FindGameObjectsWithTag("HexTile");
            foreach (GameObject wall in walls){
                if (wall.ToString().StartsWith("HexagonW")){
                    HexagonUtils.ReplaceWith(HexagonTile, wall.transform.position);
                }
            }

            HexagonUtils.ReplaceWith(HexagonTile, 0, 0);
        }

        public void CreateLevel(int level) {
            ClearWalls();
            if (level == 0){
                HexagonUtils.ReplaceWith(HexagonTileWall, 1, 0);
                HexagonUtils.ReplaceWith(HexagonTileWall, 0, -1);
                HexagonUtils.ReplaceWith(HexagonTileWall, 1, -1);
                HexagonUtils.ReplaceWith(HexagonTileWall, 0, 1);
                HexagonUtils.ReplaceWith(HexagonTileWall, -1, -0);
                HexagonUtils.ReplaceWith(HexagonTileWall, -1, 1);
            }

            if (level == 2){
                HexagonUtils.ReplaceWith(HexagonTileWall, 1, -2);
                HexagonUtils.ReplaceWith(HexagonTileWall, 2, -1);
                HexagonUtils.ReplaceWith(HexagonTileWall, 1, 1);
                HexagonUtils.ReplaceWith(HexagonTileWall, -1, 2);
                HexagonUtils.ReplaceWith(HexagonTileWall, -2, 1);
                HexagonUtils.ReplaceWith(HexagonTileWall, -1, -1);
            }

            if (level == 4){
                HexagonUtils.ReplaceWith(HexagonTileWall, 1, -2);
                HexagonUtils.ReplaceWith(HexagonTileWall, 2, -1);
                HexagonUtils.ReplaceWith(HexagonTileWall, 1, 1);
                HexagonUtils.ReplaceWith(HexagonTileWall, -1, 2);
                HexagonUtils.ReplaceWith(HexagonTileWall, -2, 1);
                HexagonUtils.ReplaceWith(HexagonTileWall, -1, -1);

                // out side

                HexagonUtils.ReplaceWith(HexagonTileWall, -2, -1);
                HexagonUtils.ReplaceWith(HexagonTileWall, 1, -3);
                HexagonUtils.ReplaceWith(HexagonTileWall, 3, -2);
                HexagonUtils.ReplaceWith(HexagonTileWall, 2, 1);
                HexagonUtils.ReplaceWith(HexagonTileWall, -1, 3);
                HexagonUtils.ReplaceWith(HexagonTileWall, -3, 2);
            }

            if (level == 5){
                HexagonUtils.ReplaceWith(HexagonTileWall, -4, 1);
                HexagonUtils.ReplaceWith(HexagonTileWall, -5, 2);
                HexagonUtils.ReplaceWith(HexagonTileWall, -5, 3);
                HexagonUtils.ReplaceWith(HexagonTileWall, -4, 3);

                HexagonUtils.ReplaceWith(HexagonTileWall, 4, -1);
                HexagonUtils.ReplaceWith(HexagonTileWall, 5, -2);
                HexagonUtils.ReplaceWith(HexagonTileWall, 5, -3);
                HexagonUtils.ReplaceWith(HexagonTileWall, 4, -3);

                HexagonUtils.ReplaceWith(HexagonTileWall, -3, 4);
                HexagonUtils.ReplaceWith(HexagonTileWall, -3, 5);
                HexagonUtils.ReplaceWith(HexagonTileWall, -2, 5);
                HexagonUtils.ReplaceWith(HexagonTileWall, -1, 4);

                HexagonUtils.ReplaceWith(HexagonTileWall, 3, -4);
                HexagonUtils.ReplaceWith(HexagonTileWall, 3, -5);
                HexagonUtils.ReplaceWith(HexagonTileWall, 2, -5);
                HexagonUtils.ReplaceWith(HexagonTileWall, 1, -4);


                HexagonUtils.ReplaceWith(HexagonTileWall, -3, -1);
                HexagonUtils.ReplaceWith(HexagonTileWall, -3, -2);
                HexagonUtils.ReplaceWith(HexagonTileWall, -2, -3);
                HexagonUtils.ReplaceWith(HexagonTileWall, -1, -3);

                HexagonUtils.ReplaceWith(HexagonTileWall, 3, 1);
                HexagonUtils.ReplaceWith(HexagonTileWall, 3, 2);
                HexagonUtils.ReplaceWith(HexagonTileWall, 2, 3);
                HexagonUtils.ReplaceWith(HexagonTileWall, 1, 3);
            }
        }

        private Vector2 GetHexCoord(int xw, int yw) {
            // axial to cube
            int newX = xw + (yw - (yw & 1))/2;
            return new Vector2(newX, yw);
        }
    }
}