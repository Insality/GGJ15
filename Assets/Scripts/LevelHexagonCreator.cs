using UnityEngine;

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
        GameObject _curHexTile;
        for (int i = 0; i < BoardSize; i++){
            _curHexTile = Instantiate(HexagonTile) as GameObject;
            _curHexTile.transform.position = HexagonUtils.GetVectorBySide(4)*i;


            // corner
            if (i == BoardSize - 1){
                _corners[4] = _curHexTile.transform.position;
            }

            for (int j = 1; j < BoardSize; j++){
                // to 2 side:
                GameObject _curHexTileSecond;
                _curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                Vector2 v2temp = HexagonUtils.GetVectorBySide(2)*j;
                _curHexTileSecond.transform.position = _curHexTile.transform.position + new Vector3(v2temp.x, v2temp.y);

                // To 0 side
                GameObject _curHexTileSecond2;
                _curHexTileSecond2 = Instantiate(HexagonTile) as GameObject;
                v2temp = HexagonUtils.GetVectorBySide(0)*j;
                _curHexTileSecond2.transform.position = _curHexTile.transform.position + new Vector3(v2temp.x, v2temp.y);


                // corners 
                if (i == BoardSize - 1 && j == BoardSize - 1){
                    _corners[3] = _curHexTileSecond.transform.position;
                    _corners[5] = _curHexTileSecond2.transform.position;
                }

                if (i == 0 && j == BoardSize - 1){
                    _corners[2] = _curHexTileSecond.transform.position;
                    _corners[0] = _curHexTileSecond2.transform.position;
                }
            }
        }

        // upper right part
        for (int i = 1; i < BoardSize; i++){
            _curHexTile = Instantiate(HexagonTile) as GameObject;
            _curHexTile.transform.position = HexagonUtils.GetVectorBySide(1)*i;

            if (i == BoardSize - 1){
                _corners[1] = _curHexTile.transform.position;
            }

            for (int j = 1; j < BoardSize - i; j++){
                // to 2 side:
                GameObject _curHexTileSecond;
                _curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                Vector2 v2temp = HexagonUtils.GetVectorBySide(2)*j;
                _curHexTileSecond.transform.position = _curHexTile.transform.position + new Vector3(v2temp.x, v2temp.y);

                // To 0 side
                _curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                v2temp = HexagonUtils.GetVectorBySide(0)*j;
                _curHexTileSecond.transform.position = _curHexTile.transform.position + new Vector3(v2temp.x, v2temp.y);
            }
        }

        for (int i = 0; i < _corners.Length; i++){
            Destroy(HexagonUtils.GetObjByWorldPos(_corners[i]));
        }


        // Turret creation
        for (int i = 0; i < _corners.Length; i++){
            var curTurret = Instantiate(Turret) as GameObject;
            curTurret.transform.position = _corners[i];

            curTurret.GetComponent<TurretLogic>().TurretNumber = i;
            curTurret.GetComponent<SpriteRenderer>().sprite = TurretsSprites[i];

            // TODO: do it!
            curTurret.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, HexagonUtils.GetAngleBySide(i) - 90));
        }

        // Wall Creation

        //        HexagonUtils.ReplaceWith(HexagonTileWall, -2, 0);
    }

    private Vector2 GetHexCoord(int xw, int yw) {
        //return new Vector2(x, y);

        // axial to cube
        int x, y, z;
        x = xw;
        z = yw;
        y = -x - z;

        int newX = x + (z - (z & 1))/2;
        return new Vector2(newX, z);
    }

    // Update is called once per frame
    private void Update() {
    }
}