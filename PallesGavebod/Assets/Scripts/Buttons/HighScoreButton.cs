using UnityEngine;
using System.Collections;

public class HighScoreButton : Button
{
    public bool InMellemMenu;
    public float TimeBetweenAnimation = 1;
    public Material[] Materials;

    private AnimationsManager mAnimationsManager;

    private bool mHasBeenExit;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        if(!InMellemMenu)
        {
            mAnimationsManager = transform.GetChild(0).GetComponent<AnimationsManager>();
        }
  
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(mHasBeenExit)
        {
            OnMouseExit();
        }

        if (mMenuRunning != null && mMenuRunning.ReachedEnd && KeyPressed)
        {
            Application.LoadLevel("HighScore");
        }
        if(KeyPressed && InMellemMenu)
        {
            Application.LoadLevel("HighScore");
        }
    }

    protected override IEnumerator DoActivity()
    {
        if(!InMellemMenu)
        {
            StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, false, true));
            yield return new WaitForSeconds(1);
            StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, true, false));
        }
        
    }

    protected override void OnMouseEnter()
    {
        base.OnMouseEnter();
        if (!InMellemMenu && !mAnimationsManager.PlayingBackWards && !mAnimationsManager.PlayingNormal && !GameObject.Find("ExitButton").GetComponent<ExitButton>().KeyPressed) 
    {
        StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, false, true));    
    }
    else if (InMellemMenu)
    {
        renderer.material = HoverMaterial;
    }  
       
    
        
    }

    protected override void OnMouseExit()
    {
        base.OnMouseExit();
        mHasBeenExit = true;
        if (!InMellemMenu && !mAnimationsManager.PlayingBackWards && !mAnimationsManager.PlayingNormal && !GameObject.Find("ExitButton").GetComponent<ExitButton>().KeyPressed)
        {
            mHasBeenExit = false;
            StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, true,
                                                            false));
        }
        else if (InMellemMenu)
        {
            renderer.material = NormalMaterial;
            mHasBeenExit = false;
        }

      
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
     

    }

}
