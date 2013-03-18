using UnityEngine;
using System.Collections;

public class NextLevelButton : Button {


	
	// Update is called once per frame
	void Update () {
	if(KeyPressed)
	{
        
        GameObject.Find("Gamemanager").GetComponent<GameManager>().NextLevel();
	    
	}
	}

    protected override void OnMouseDown()
    {
        KeyPressed = true;
    }

  
}
