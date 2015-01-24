using UnityEngine;

public class PlayerLogic: MonoBehaviour {
    public float GodModTime;
    private CircleCollider2D myCC2d;
    public AudioClip PlayerOutGodMode;
    // Use this for initialization
    private void Start() {
        GodModTime = 0;
        myCC2d = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    private void Update() {

        GodModTime -= Time.deltaTime;
        if (GodModTime < 0){
            GodModTime = 0;

            if (!myCC2d.enabled){
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                GetComponent<PlayerMovement>().ResetStayCounter();
                AudioSource.PlayClipAtPoint(PlayerOutGodMode, transform.position);
            }
            myCC2d.enabled = true;
            
        }

    }

    public void SetGodMode(float time) {
        GodModTime = time;
        myCC2d.enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
    }
}