using UnityEngine;
using System.Collections;

public class NoButton : Button
{

    public float Speed = 10.0F;

    public bool MoveToButton { get; set; }
    public bool bMoveOut { get; set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (mMenuRunning.ReachedEnd && KeyPressed)
        {
           
        }

        if (MoveToButton)
        {
            MoveToExitButton();
        }
        else if (bMoveOut)
        {
            MoveOut();
        }
    }

    void MoveToExitButton()
    {
     if(transform.position.x < Camera.main.ScreenToWorldPoint(new Vector2((Screen.width/2)-(Screen.width/2.5f),0)).x)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
     else
     {
         MoveToButton = false;
     }
 
    
    }

    void MoveOut()
    {
        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.ViewportToScreenPoint(new Vector2(0,1)).x,0)).x -  (transform.lossyScale.x/2))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
        else
        {
            bMoveOut = false;
        }
   


    }

    void OnBecameInvisible()
    {
     
        bMoveOut = false;
    }

    protected override void OnMouseDown()
    {
        KeyPressed = true;
        GameObject.Find("YesButton").GetComponent<YesButton>().bMoveOut = true;
        bMoveOut = true;
        ExitButton tExitButton = GameObject.Find("ExitButton").GetComponent<ExitButton>();
        tExitButton.KeyPressed = false;
        StartCoroutine(GameObject.Find("AnimationManager").GetComponent<AnimationsManager>().MakeAnimation(
        tExitButton.ButtonsAnimation, tExitButton.renderer, tExitButton.TimeBetweenAmimation, false, true,false));

    }

}
