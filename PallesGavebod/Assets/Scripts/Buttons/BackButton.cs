using UnityEngine;
using System.Collections;

public class BackButton : Button
{
    public float Speed;

    private PreviousLevels mPreviousLevels;

    private bool mKeyPressed;

	// Use this for initialization
	public override void Start ()
	{
        base.Start();
	    mPreviousLevels = GameObject.Find("PreviousLevels").GetComponent<PreviousLevels>();
	}
	
	// Update is called once per frame
	void Update () {


    if (transform.GetChild(0).GetComponent<Button>().KeyPressed)
    {
        Invoke("Back", 2);
        mKeyPressed = true;
    }
        if(mKeyPressed)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }


    if (transform.GetChild(0).GetComponent<Button>().MouseEnter && !mKeyPressed)
    { 
        MoveToPosition();
    }
    if (transform.GetChild(0).GetComponent<Button>().MouseExit && !mKeyPressed)
    {
        MoveBackToStartPosition();
    }

	}



    void Back()
    {
    
    
         if (Application.loadedLevelName == "HighScore")
         {
             DisplayHighscores tHighscore = GameObject.Find("HighScore").GetComponent<DisplayHighscores>();
             if (tHighscore.ShowHighScores)
             {
                 Application.LoadLevel("HighScore");

             }
             else
             {
                 Application.LoadLevel("Menu");
             }
         }
         else if (Application.loadedLevelName == "CharactorView")
         {
             Application.LoadLevel(mPreviousLevels.GetPreviousLevel());
         }
       
        else
         {
             switch (Application.loadedLevelName)
             {
                 case "GameLoad":
                     Application.LoadLevel("Menu");
                     break;
                 case "HighScore":
                     Application.LoadLevel("Menu");
                     break;
                 case "InputName":
                     Application.LoadLevel("GameLoad");
                     break;
                 case "ChooseSave":
                     Application.LoadLevel("GameLoad");
                     break;
                 case "Credits":
                     Application.LoadLevel("Menu");
                     break;
                 case "Option":
                     Application.LoadLevel("Level"+PermenentVariables.Level);
                     break;


             }
         }

           
        
      /* else
        {
            if (Application.loadedLevelName != "HighScore")
            {
                Application.LoadLevel(mPreviousLevels.GetPreviousLevel());
            }
            
        }*/
      

    
        

    }

  



    

}
