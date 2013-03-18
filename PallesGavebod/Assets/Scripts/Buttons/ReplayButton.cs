using UnityEngine;
using System.Collections;

public class ReplayButton : Button {

    


	
	// Update is called once per frame
	void Update () {
	if(KeyPressed)
	{
	    GameObject.Find("Gamemanager").GetComponent<GameManager>().SwitchLevel(PermenentVariables.Level);
	}
	}



    protected override void OnMouseDown()
    {
        KeyPressed = true;
    }
}
