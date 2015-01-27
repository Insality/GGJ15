using UnityEngine;

namespace Assets.Scripts {
    public class Menu: MonoBehaviour {
        public GameObject HelpWindow;
        public int TurtleDirection;
        public GameObject _exit;
        public GameObject _options;
        public GameObject _scores;
        public GameObject _start;
        public int isHelpCalled;

        public Sprite redmenu_0;
        public Sprite redmenu_1;
        public Sprite redmenu_2;
        public Sprite redmenu_3;

        public Sprite whitemenu_0;
        public Sprite whitemenu_1;
        public Sprite whitemenu_2;
        public Sprite whitemenu_3;

        // Use this for initialization
        private void Start() {
            TurtleDirection = 0;
            _start.GetComponent<SpriteRenderer>().sprite = redmenu_1;
            _options.GetComponent<SpriteRenderer>().sprite = whitemenu_3;
            _exit.GetComponent<SpriteRenderer>().sprite = whitemenu_2;
            _scores.GetComponent<SpriteRenderer>().sprite = whitemenu_0;
            isHelpCalled = 0;
        }

        // Update is called once per frame
        private void Update() {
            if (isHelpCalled == 1){
                HelpWindow.SetActive(true);
            }
            else{
                HelpWindow.SetActive(false);
            }

            Vector2 velocity = Vector2.zero;
            Quaternion _TurtleRot = Quaternion.Euler(0, 0, TurtleDirection*60);
            transform.rotation = Quaternion.Lerp(transform.rotation, _TurtleRot, 0.4f);


            if (Input.GetKeyUp(KeyCode.LeftArrow)){
                AddDirection();
                TextColor();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)){
                TurtleDirection = ((--TurtleDirection + 6)%6);
                if ((TurtleDirection%6 == 4) || ((TurtleDirection + 6)%6 == 2)){
                    TurtleDirection = ((--TurtleDirection + 6)%6);
                }
                TextColor();
            }
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space)){
                switch (TurtleDirection){
                    case 0:
                        Application.LoadLevel("Test");
                        break;
                    case 1:
                        //загрузка рекордов
                        break;
                    case 3:
                        Application.Quit();
                        break;
                    case 5:
                        CallHelp();
                        break;
                }
            }

            if ((isHelpCalled == 1) && (Input.anyKeyDown)){
                //Destroy (HelpWindow);
                isHelpCalled = 0;
            }
        }

        //номера спрайтов:
        //start - 1; exit - 2; options - 3; score - 0;
        private int AddDirection() {
            TurtleDirection = ++TurtleDirection%6;
            if ((TurtleDirection%6 == 4) || (TurtleDirection%6 == 2))
                return ++TurtleDirection%6;
            return TurtleDirection;
        }

        private void TextColor() {
            if (TurtleDirection == 0){
                //Debug.Log (TurtleDirection + "SDSDSDDS");
                _start.GetComponent<SpriteRenderer>().sprite = redmenu_1;
                _options.GetComponent<SpriteRenderer>().sprite = whitemenu_3;
                _exit.GetComponent<SpriteRenderer>().sprite = whitemenu_2;
                _scores.GetComponent<SpriteRenderer>().sprite = whitemenu_0;
            }
            if (TurtleDirection == 1){
                _start.GetComponent<SpriteRenderer>().sprite = whitemenu_1;
                _options.GetComponent<SpriteRenderer>().sprite = whitemenu_3;
                _exit.GetComponent<SpriteRenderer>().sprite = whitemenu_2;
                _scores.GetComponent<SpriteRenderer>().sprite = redmenu_0;
            }
            if (TurtleDirection == 5){
                _start.GetComponent<SpriteRenderer>().sprite = whitemenu_1;
                _options.GetComponent<SpriteRenderer>().sprite = redmenu_3;
                _exit.GetComponent<SpriteRenderer>().sprite = whitemenu_2;
                _scores.GetComponent<SpriteRenderer>().sprite = whitemenu_0;
            }
            if (TurtleDirection == 3){
                _start.GetComponent<SpriteRenderer>().sprite = whitemenu_1;
                _options.GetComponent<SpriteRenderer>().sprite = whitemenu_3;
                _exit.GetComponent<SpriteRenderer>().sprite = redmenu_2;
                _scores.GetComponent<SpriteRenderer>().sprite = whitemenu_0;
            }
        }

        private void CallHelp() {
            if (isHelpCalled == 0){
                //Instantiate (HelpWindow);
                isHelpCalled = 1;
            }
            if ((isHelpCalled == 1) && (Input.anyKeyDown)){
                //Destroy (HelpWindow);
                isHelpCalled = 0;
            }
        }
    }
}