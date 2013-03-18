using UnityEngine;
using System.Collections;

public class PlayButton : Button
{

    public float TimeBetweenAnimation = 1;
    public Material[] Materials;

    private AnimationsManager mAnimationsManager;

    private bool mHasBeenExit;

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
            Application.LoadLevel("GameLoad");
        }

        if(mHasBeenExit)
        {
            OnMouseExit();
        }
    }

    protected override IEnumerator DoActivity()
    {
        StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, false, true));
        yield return new WaitForSeconds(1);
        StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, true, false));
    }

    protected override void OnMouseEnter()
    {
        base.OnMouseEnter();
        if (!mAnimationsManager.PlayingBackWards && !mAnimationsManager.PlayingNormal && !GameObject.Find("ExitButton").GetComponent<ExitButton>().KeyPressed)
        {
            StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, false, true));  
        }
    }

    protected override void OnMouseExit()
    {
        mHasBeenExit = true;
        base.OnMouseExit();
        if (!mAnimationsManager.PlayingBackWards && !mAnimationsManager.PlayingNormal && !GameObject.Find("ExitButton").GetComponent<ExitButton>().KeyPressed)
        {
            mHasBeenExit = false;
            StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, true, false));
        }
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();


    }

}
