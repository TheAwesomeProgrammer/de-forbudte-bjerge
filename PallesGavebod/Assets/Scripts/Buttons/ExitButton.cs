using UnityEngine;
using System.Collections;

public class ExitButton : Button
{

    public Material[] ButtonsAnimation;
    public float TimeBetweenAmimation;

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (mMenuRunning.ReachedEnd && KeyPressed)
        {
            Application.Quit();
        }
    }

    protected override IEnumerator DoActivity()
    {
        renderer.material = HoverMaterial;
        yield return new WaitForSeconds(1);
        renderer.material = NormalMaterial;
    }



    protected override void OnMouseEnter()
    {
        base.OnMouseEnter();
        if (!KeyPressed)
        {
            renderer.material = HoverMaterial;
        }
    }

  

    protected override void OnMouseExit()
    {
        base.OnMouseExit();
        if(!KeyPressed)
        {
            renderer.material = NormalMaterial;
        }
    }

    protected override void OnMouseDown()
    {
        KeyPressed = true;
        if (renderer.material.name == "set0 (Instance)" || renderer.material.name == "set1 (Instance)")
        {
            StartCoroutine(transform.GetChild(0).GetComponent<AnimationsManager>().MakeAnimation(ButtonsAnimation, renderer, TimeBetweenAmimation, false, false, true));
        }
      
        GameObject.Find("NoButton").GetComponent<NoButton>().MoveToButton = true;
        GameObject.Find("YesButton").GetComponent<YesButton>().MoveToButton = true;

    }

    
    
}
