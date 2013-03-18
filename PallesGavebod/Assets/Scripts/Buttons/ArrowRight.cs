using UnityEngine;
using System.Collections;

public class ArrowRight : Button {

    private CharactorViewManager mCharactorViewManager;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {

        mCharactorViewManager = GameObject.Find("CharactorViewManager").GetComponent<CharactorViewManager>();

        if (KeyPressed && !mCharactorViewManager.AreChildrenBusy())
        {
            mCharactorViewManager.ShiftAllChildren(true);
            KeyPressed = false;
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !mCharactorViewManager.AreChildrenBusy())
        {
            mCharactorViewManager.ShiftAllChildren(true);
            OnMouseEnter();
            Invoke("SetMaterialToNormal",0.5f);
        }
	}

    void SetMaterialToNormal()
    {
        renderer.material = NormalMaterial;
    }
}
