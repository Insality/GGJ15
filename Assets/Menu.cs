using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public int TurtleDirection;
	public GameObject _start;
	public GameObject _options;
	public GameObject _scores;
	public GameObject _exit;
	public GameObject HelpWindow;

	public Sprite redmenu_0;
	public Sprite redmenu_1;
	public Sprite redmenu_2;
	public Sprite redmenu_3;

	public Sprite whitemenu_0;
	public Sprite whitemenu_1;
	public Sprite whitemenu_2;
	public Sprite whitemenu_3;

	public int isHelpCalled;

	// Use this for initialization
	void Start () {
		TurtleDirection = 0;
		_start.GetComponent<SpriteRenderer>().sprite = redmenu_1;
		_options.GetComponent<SpriteRenderer>().sprite = whitemenu_3;
		_exit.GetComponent<SpriteRenderer>().sprite = whitemenu_2;
		_scores.GetComponent<SpriteRenderer>().sprite = whitemenu_0;
		isHelpCalled = 0;
	}

	// Update is called once per frame
	void Update () {

		Vector2 velocity = Vector2.zero;
		Quaternion _TurtleRot = Quaternion.Euler (0, 0, TurtleDirection * 60);
		transform.rotation = Quaternion.Lerp(transform.rotation, _TurtleRot, 0.4f);

	
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			AddDirection();
			TextColor ();
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			TurtleDirection = ((--TurtleDirection + 6) % 6);
			if ((TurtleDirection % 6 == 4) || ((TurtleDirection+6) % 6 == 2)) {
				TurtleDirection = ((--TurtleDirection + 6) % 6);
			}
			TextColor ();
		}
		/*if (Input.GetKeyUp(KeyCode.Return)) {
			switch (TurtleDirection)
			{
			case 0:
				//загрузка игры
				break;
			case 1:
				//загрузка рекордов
				break;
			case 3:
				//выход из игры
				break;
			case 5:
				//CallHelp();
				break;
			}
		}*/

	}
	//номера спрайтов:
	//start - 1; exit - 2; options - 3; score - 0;
	int AddDirection() {
		TurtleDirection = ++TurtleDirection % 6;
		if ((TurtleDirection % 6 == 4) || (TurtleDirection % 6 == 2))
						return ++TurtleDirection % 6;
		else
					return TurtleDirection;

		}
	void TextColor()
	{
		if (TurtleDirection == 0) {
			Debug.Log (TurtleDirection + "SDSDSDDS");
			_start.GetComponent<SpriteRenderer>().sprite = redmenu_1;
			_options.GetComponent<SpriteRenderer>().sprite = whitemenu_3;
			_exit.GetComponent<SpriteRenderer>().sprite = whitemenu_2;
			_scores.GetComponent<SpriteRenderer>().sprite = whitemenu_0;
		}
		if (TurtleDirection == 1) {
			_start.GetComponent<SpriteRenderer>().sprite = whitemenu_1;
			_options.GetComponent<SpriteRenderer>().sprite = whitemenu_3;
			_exit.GetComponent<SpriteRenderer>().sprite = whitemenu_2;
			_scores.GetComponent<SpriteRenderer>().sprite = redmenu_0;
		}
		if (TurtleDirection == 5) {
			_start.GetComponent<SpriteRenderer>().sprite = whitemenu_1;
			_options.GetComponent<SpriteRenderer>().sprite = redmenu_3;
			_exit.GetComponent<SpriteRenderer>().sprite = whitemenu_2;
			_scores.GetComponent<SpriteRenderer>().sprite = whitemenu_0;
		}
		if (TurtleDirection == 3) {
			_start.GetComponent<SpriteRenderer>().sprite = whitemenu_1;
			_options.GetComponent<SpriteRenderer>().sprite = whitemenu_3;
			_exit.GetComponent<SpriteRenderer>().sprite = redmenu_2;
			_scores.GetComponent<SpriteRenderer>().sprite = whitemenu_0;
		}
	}
	void CallHelp(){
		if (isHelpCalled == 0) {
						Instantiate (HelpWindow);
						isHelpCalled = 1;
				}
		if ((isHelpCalled == 1) && (Input.GetKeyUp (KeyCode.Escape))) {
						Destroy (HelpWindow);
						isHelpCalled = 0;
				}
	}
}

