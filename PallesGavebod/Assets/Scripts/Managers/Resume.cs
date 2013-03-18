using UnityEngine;
using System.Collections;

public class Resume : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	SetPositionOnScreen();
	}

    void SetPositionOnScreen()
    {
        GetComponent<GUIText>().pixelOffset = new Vector2(Screen.width-(Screen.width/2), Screen.height - Screen.height / 4);

    }

    void OnGUI()
    {
     

    }

    void OnMouseDown()
    {
        GameObject.Find("PauseMenu").GetComponent<PauseMenu>().Resume();
    }
}
