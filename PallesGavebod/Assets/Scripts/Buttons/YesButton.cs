using UnityEngine;
using System.Collections;

public class YesButton : Button
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
            Application.Quit();
        }
        if(MoveToButton)
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
        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector2((Screen.width / 2) + (Screen.width / 2.5f), 0)).x)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
        else
        {
            MoveToButton = false;
        }


    }

    void MoveOut()
    {
        if (transform.position.x < Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.ViewportToScreenPoint(new Vector2(1, 1)).x, 0)).x + (transform.lossyScale.x / 2))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
        else
        {
            bMoveOut = false;
        }
       
        
    }
  

    protected override void OnMouseDown()
    {
        KeyPressed = true;
        mMenuRunning.Run = true;
        GameObject.Find("NoButton").GetComponent<NoButton>().bMoveOut = true;
        bMoveOut = true;

    }

}
