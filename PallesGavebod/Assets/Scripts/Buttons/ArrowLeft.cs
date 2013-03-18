using UnityEngine;
using System.Collections;

public class ArrowLeft : Button
{

    private CharactorViewManager mCharactorViewManager;

	// Use this for initialization

	
	// Update is called once per frame
	void Update ()
	{
	    mCharactorViewManager = GameObject.Find("CharactorViewManager").GetComponent<CharactorViewManager>();

        if (KeyPressed && !mCharactorViewManager.AreChildrenBusy())
        {
            mCharactorViewManager.ShiftAllChildren(false);
            KeyPressed = false;
        }
        else  if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !mCharactorViewManager.AreChildrenBusy())
        {
            mCharactorViewManager.ShiftAllChildren(false);
            OnMouseEnter();
            Invoke("SetMaterialToNormal", 0.5f);
        }
       
	}

    void SetMaterialToNormal()
    {
        renderer.material = NormalMaterial;
    }
}
