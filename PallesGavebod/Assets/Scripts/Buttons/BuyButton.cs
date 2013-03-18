using UnityEngine;
using System.Collections;

public class BuyButton :  Button {



	
	// Update is called once per frame
	void Update ()
	{
	  

        if (KeyPressed)
        {
            CharactorView tCharactorView = GameObject.Find("CharactorViewManager").GetComponent<CharactorViewManager>().FindCenterView();


            if (tCharactorView.Locked && (PermenentVariables.Stars - tCharactorView.Price) >= 0)
            {
            PermenentVariables.Stars -= tCharactorView.Price;
                GameObject.Find("SaveAndLoadManager").GetComponent<SaveAndLoad>().Save();
                tCharactorView.Locked = false;
               }
            KeyPressed = false;
        }
        else
        {
            print("NOT ENOUGH MONEY OR UNLOCKED");
        }
	    

	}
	}

