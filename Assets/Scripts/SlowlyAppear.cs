using UnityEngine;
using UnityEngine.UI;

public class SlowlyAppear: MonoBehaviour {
    public float AppearTime;

    private float _curAppearTime;
    public float StartFrom;
    private Text _myText;
    // Use this for initialization
    private void Start() {
        _myText = GetComponent<Text>();
        _curAppearTime = 0;

        Color color = _myText.color;
        color.a = 0;
        _myText.color = color;
    }

    public void Reset() {
        _curAppearTime = 0;
    }

    // Update is called once per frame
    private void Update() {
        _curAppearTime += Time.deltaTime;

        if (_curAppearTime > AppearTime + StartFrom){
            _curAppearTime = AppearTime + StartFrom;
        }

        if (_curAppearTime > StartFrom){
            Color color = _myText.color;
            color.a = Mathf.Lerp(0, 1, (_curAppearTime - StartFrom)/AppearTime);
            _myText.color = color;
        }
    }
}