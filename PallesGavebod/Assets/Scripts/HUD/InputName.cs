using System;
using UnityEngine;
using System.Collections;

public class InputName : MonoBehaviour
{
    
    public int MaxStringLength = 30;
    public int WidthInput = 200;
    public int Heightinput = 20;
    public GUIStyle GuiStyle;
    public Rect WindowRect = new Rect(20, 20, 120, 50);
    public Rect YesButtonRect = new Rect(20, 20, 120, 50);
    public Rect NoButtonRect = new Rect(20, 20, 120, 50);


    private string mPlayerName = "Indtast dit navn";

    private Rect InputRect;

    private bool mPlayerNameIsInUse = false;
    private bool mPressedEnter;
    private bool mMousePressed;

    private GameManager mGameManager;

    private SaveAndLoad mSaveAndLoadManager;

	// Use this for initialization
	void Start ()
	{
        mGameManager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
        mSaveAndLoadManager = GameObject.Find("SaveAndLoadManager").GetComponent<SaveAndLoad>();
	    mSaveAndLoadManager.Load(true);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(1))
        {
            mMousePressed = true;
        }
        WindowRect = new Rect(Screen.width / 2 - (WindowRect.width / 2), Screen.height / 2 - (WindowRect.height / 2), WindowRect.width, WindowRect.height);

	}
  
 
 
    void OnGUI()
    {

        mPlayerName = mPlayerName.Replace("\n", String.Empty);
        mPlayerName = mPlayerName.Replace("\r", String.Empty);
        mPlayerName = mPlayerName.Replace("\t", String.Empty);

         
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
            mPressedEnter = true;
        }
        else if(Event.current.isMouse || mMousePressed)
        {
            MaxStringLength = 25;
            mPlayerName = "";
        }
        if(mPressedEnter)
        {
          
            if (mGameManager.HighScores.Exists(item => item.CurrentPlayerName == mPlayerName))
            {
                mPressedEnter = false;
                mPlayerNameIsInUse = true;
                mPlayerName = "Playernavn er i brug";
            }
            if (mSaveAndLoadManager.LocalHighScores.Count >= 5)
            {
                mPressedEnter = false;
                mPlayerNameIsInUse = true;
                MaxStringLength = 100;
                mPlayerName = "For mange brugere, slet en";
                
            }
             if(!mGameManager.HighScores.Exists(item => item.CurrentPlayerName == mPlayerName))
            {
                   mPlayerNameIsInUse = false;
            }

            if(!mGameManager.HighScores.Exists(item => item.CurrentPlayerName == mPlayerName) && !mPlayerNameIsInUse)
            {
                InputRect = new Rect(-10000, -10000, WidthInput, Heightinput);
                WindowRect = GUI.Window(0, WindowRect, DoMyWindow, "Bekræft");
                Debug.Log("FALSE PLAYERNAVN IKKE I BRUG");
            }
        }

        else
        {
            InputRect = new Rect(Screen.width / 2 - (WidthInput / 2), Screen.height / 2 - (Heightinput / 2), WidthInput, Heightinput);
        }

        mPlayerName = GUI.TextField(InputRect, mPlayerName, MaxStringLength,GuiStyle);
    }

    void DoMyWindow(int windowID)
    {
        if (GUI.Button(YesButtonRect, "Yes"))
        {
            PermenentVariables.Level = 1;
            PermenentVariables.PlayerName = mPlayerName+"xx";
            Application.LoadLevel("CharactorView");
        }
        else if (GUI.Button(NoButtonRect, "No"))
        {
            mPressedEnter = false;
        }
           

    }

   


}
