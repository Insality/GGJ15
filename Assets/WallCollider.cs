using UnityEngine;

public class WallCollider: MonoBehaviour {
    // Use this for initialization
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Projectile"){
            other.gameObject.SetActive(false);
        }
    }
}