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
}
