using UnityEngine;
using System.Collections;

public class LevelHexagonCreator : MonoBehaviour {

    public int BoardSize;
    public GameObject HexagonTile;

	// Use this for initialization
	void Start () {

        CreateMap();
	}

    void CreateMap(){
        GameObject _curHexTile;
        for (int i = 0; i < BoardSize; i++){
            _curHexTile = Instantiate(HexagonTile) as GameObject;
            _curHexTile.transform.position = HexagonUtils.GetVectorBySide(4) * i;

            for (int j = 1; j < BoardSize; j++){
                // to 2 side:
                GameObject _curHexTileSecond;
                _curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                var v2temp = HexagonUtils.GetVectorBySide(2) * j;
                _curHexTileSecond.transform.position = _curHexTile.transform.position +  new Vector3(v2temp.x, v2temp.y);

                // To 0 side
                _curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                v2temp = HexagonUtils.GetVectorBySide(0) * j;
                _curHexTileSecond.transform.position = _curHexTile.transform.position + new Vector3(v2temp.x, v2temp.y);
            }
        }

        // upper right part
        for (int i = 1; i < BoardSize; i++){
            _curHexTile = Instantiate(HexagonTile) as GameObject;
            _curHexTile.transform.position = HexagonUtils.GetVectorBySide(1) * i;

            for (int j = 1; j < BoardSize - i; j++)
            {
                // to 2 side:
                GameObject _curHexTileSecond;
                _curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                var v2temp = HexagonUtils.GetVectorBySide(2) * j;
                _curHexTileSecond.transform.position = _curHexTile.transform.position + new Vector3(v2temp.x, v2temp.y);

                // To 0 side
                _curHexTileSecond = Instantiate(HexagonTile) as GameObject;
                v2temp = HexagonUtils.GetVectorBySide(0) * j;
                _curHexTileSecond.transform.position = _curHexTile.transform.position + new Vector3(v2temp.x, v2temp.y);
            }

        }
    }

    private Vector2 GetHexCoord(int xw, int yw)
    {
        //return new Vector2(x, y);

        // axial to cube
        int x, y, z;
        x = xw;
        z = yw;
        y = -x - z;

        int newX = x + (z - (z & 1)) / 2;
        return new Vector2(newX, z);
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
