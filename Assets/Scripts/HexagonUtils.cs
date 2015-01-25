using UnityEngine;

public class HexagonUtils {
    public static Vector2 GetVectorBySide(int side) {
        var result = new Vector2(0, 0);

        if (side == 0){
            result.x = 1;
        }
        if (side == 1){
            result.x = 0.5f;
            result.y = 0.866f;
        }
        if (side == 2){
            result.x = -0.5f;
            result.y = 0.866f;
        }
        if (side == 3){
            result.x = -1;
        }
        if (side == 4){
            result.x = -0.5f;
            result.y = -Mathf.Sqrt(3)/2;
        }
        if (side == 5){
            result.x = 0.5f;
            result.y = -0.866f;
        }
        return result.normalized;
    }

    public static float GetAngleBySide(int side) {
        return 60*side;
    }

    public static Vector2 GetV2FromV3(Vector3 v3) {
        return new Vector2(v3.x, v3.y);
    }

    public static Vector3 GetV3FromV2(Vector2 v2) {
        return new Vector3(v2.x, v2.y, 0);
    }

    public static Vector2 GetPosByAxiel(int x, int y) {
        var result = new Vector2();

        float l = 1f;

        result.x = Mathf.Cos(60*Mathf.Deg2Rad)*(l*y) + (l*x);
        result.y = -Mathf.Cos(30f*Mathf.Deg2Rad)*(l*y);

        return result;
    }

    public static float V3Len(Vector3 v) {
        return Mathf.Sqrt((v.x*v.x) + (v.y*v.y));
    }


    public static GameObject GetObjByWorldPos(Vector3 pos) {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("HexTile");

        foreach (GameObject tile in tiles){
            Vector3 deltaV3 = tile.transform.position - pos;

            if (V3Len(deltaV3) < 0.1f){
                return tile;
            }
        }

        return null;
    }

    // x, y axiel coord
    public static void ReplaceWith(GameObject obj, int x, int y) {
        Vector3 WorldPos = GetPosByAxiel(x, y);

        GameObject oldObj = GetObjByWorldPos(WorldPos);
        Object.Destroy(oldObj);
        var newObj = Object.Instantiate(obj) as GameObject;
        newObj.transform.position = WorldPos;
    }

    public static void ReplaceWith(GameObject obj, Vector2 WorldPos)
    {
        GameObject oldObj = GetObjByWorldPos(WorldPos);
        Object.Destroy(oldObj);
        var newObj = Object.Instantiate(obj) as GameObject;
        newObj.transform.position = WorldPos;
    }

    public static int GetDirectionByAngle(Vector3 v1, Vector3 v2) {
        var tmp = new Vector2(v2.x - v1.x, v2.y - v1.y);
        Vector2 zeroV = Vector2.right;

        float angle = Vector3.Angle(tmp, zeroV);


        if (tmp.y < 0){
            angle = Vector3.Angle(zeroV*-1, tmp) + 180;
        }

        angle += 30;

        int result = ((int) (angle/60))%6;
        Debug.Log(result);
        return result;
    }
}