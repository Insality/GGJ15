using UnityEngine;

namespace Assets.Scripts.BeatScripts {
    public class BeatShowerLogic: MonoBehaviour {
        public GameObject BeatLine;

        // Use this for initialization
        private void Start() {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BeatTracker>().BeatEvent +=
                (sender, args)=>SpawnBeatLine();
        }


        private void SpawnBeatLine() {
            // to right
            var beatRight = Instantiate(BeatLine) as GameObject;
            if (beatRight != null){
                beatRight.transform.position = transform.position + new Vector3(-0.1f, 0, 1);
                beatRight.GetComponent<BeatLineLogic>().MoveSide = 1;
            }

            // to left
            var beatLeft = Instantiate(BeatLine) as GameObject;
            if (beatLeft != null){
                beatLeft.transform.position = transform.position + new Vector3(0.1f, 0, 1);
                beatLeft.GetComponent<BeatLineLogic>().MoveSide = -1;
            }
        }
    }
}