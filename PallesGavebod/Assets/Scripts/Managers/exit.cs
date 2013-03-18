using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	SetPositionOnScreen();
	}

    void SetPositionOnScreen()
    {
        GetComponent<GUIText>().pixelOffset = new Vector2(Screen.width - (Screen.width / 2), Screen.height - Screen.height / 1.3F);
    }

    void OnMouseDown()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Menu");
    }
}
