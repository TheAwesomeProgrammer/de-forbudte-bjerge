using UnityEngine;
using System.Collections;

public class CreditsButton : Button
{

    public float TimeBetweenAnimation = 1;
    public Material[] Materials;

    private AnimationsManager mAnimationsManager;

    // Use this for initialization
    void Start()
    {
        mAnimationsManager = transform.GetChild(0).GetComponent<AnimationsManager>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (mMenuRunning.ReachedEnd && KeyPressed)
        {
            Application.LoadLevel("Credits");
        }
    }

    protected override IEnumerator DoActivity()
    {
        StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation,true,false,false));
        yield return new WaitForSeconds(1);
        mAnimationsManager.StopAnimation();
        renderer.material = NormalMaterial;
    }

    

    protected void OnMouseOver()
    {
        if (!mAnimationsManager.RunForever && !GameObject.Find("ExitButton").GetComponent<ExitButton>().KeyPressed)
      {
          StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, true, false,false));
      }
      

    }

    protected override void OnMouseExit()
    {
        base.OnMouseExit();
if(!GameObject.Find("ExitButton").GetComponent<ExitButton>().KeyPressed)
{
    mAnimationsManager.StopAnimation();
    renderer.material = NormalMaterial;
}
     
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    

    }

}
