using UnityEngine;

public class HexagonUtils {
    public static Vector2 GetVectorBySide(int side) {
        Vector2 result = new Vector2(0, 0);

        if (side == 0){
            result.x = 1;
        }
        if (side == 1) {
            result.x = 0.5f;
            result.y = 0.866f;
        }
        if (side == 2) {
            result.x = -0.5f;
            result.y = 0.866f;
        }
        if (side == 3) {
            result.x = -1;
        }
        if (side == 4) {
            result.x = -0.5f;
            result.y = -Mathf.Sqrt(3) / 2;
        }
        if (side == 5) {
            result.x = 0.5f;
            result.y = -0.866f;
        }
        return result.normalized;
    }

    public static float GetAngleBySide(int side) {
        return 60*side;
    }

    public static Vector2 GetV2FromV3(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }

    public static Vector2 GetPosByAxiel(int x, int y){
        Vector2 result = new Vector2();

        float l = 1f;

        result.x = Mathf.Cos(60 * Mathf.Deg2Rad) * (l* y) + (l  * x );
        result.y = -Mathf.Cos(30f * Mathf.Deg2Rad) * (l* y);

        return result;
    }

    public static float V3Len(Vector3 v){
        return Mathf.Sqrt((v.x * v.x) + (v.y * v.y));
    }


    public static GameObject GetObjByWorldPos(Vector3 pos)
    {
        var tiles = GameObject.FindGameObjectsWithTag("HexTile");

        foreach (GameObject tile in tiles)
        {
            Vector3 deltaV3 = tile.transform.position - pos;

            if (V3Len(deltaV3) < 0.1f)
            {
                return tile;
            }
        }

        return null;
    }

    // x, y axiel coord
    public static void ReplaceWith(GameObject obj, int x, int y)
    {
        Vector3 WorldPos = GetPosByAxiel(x, y);

        GameObject oldObj = GetObjByWorldPos(WorldPos);
        GameObject.Destroy(oldObj);
        GameObject newObj = GameObject.Instantiate(obj) as GameObject;
        newObj.transform.position = WorldPos;
    }
}
