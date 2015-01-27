using UnityEngine;

namespace Assets.Scripts {
    public class PlayerLogic: MonoBehaviour {
        public float GodModTime;
        public AudioClip PlayerOutGodMode;
        private CircleCollider2D _myCc2D;
        // Use this for initialization
        private void Start() {
            GodModTime = 0;
            _myCc2D = GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        private void Update() {
            GodModTime -= Time.deltaTime;
            if (GodModTime < 0){
                GodModTime = 0;

                if (!_myCc2D.enabled){
                    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    GetComponent<PlayerMovement>().ResetStayCounter();
                    AudioSource.PlayClipAtPoint(PlayerOutGodMode, transform.position);
                }
                _myCc2D.enabled = true;
            }
        }

        public void SetGodMode(float time) {
            GodModTime = time;
            _myCc2D.enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
        }
    }
}