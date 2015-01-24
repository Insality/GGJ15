using UnityEngine;
using System.Collections;

public class PlayerLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    Vector2 tmp = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);


        //Debug.DrawLine(tmp, tmp + (HexagonUtils.GetVectorBySide(0) * 10), Color.cyan);
        //Debug.DrawLine(tmp, tmp + (HexagonUtils.GetVectorBySide(1) * 10), Color.cyan);
        //Debug.DrawLine(tmp, tmp + (HexagonUtils.GetVectorBySide(2) * 10), Color.cyan);
        //Debug.DrawLine(tmp, tmp + (HexagonUtils.GetVectorBySide(3) * 10), Color.cyan);
        //Debug.DrawLine(tmp, tmp + (HexagonUtils.GetVectorBySide(4) * 10), Color.cyan);
        //Debug.DrawLine(tmp, tmp + (HexagonUtils.GetVectorBySide(5) * 10), Color.cyan);

	}
}
