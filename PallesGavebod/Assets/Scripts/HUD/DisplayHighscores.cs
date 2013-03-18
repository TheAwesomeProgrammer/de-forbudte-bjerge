using UnityEngine;
using System.Collections;

public class DisplayHighscores : MonoBehaviour
{
    public float Speed = 1.0F;

    public int Rows;

    public Vector2 scrollPosition = Vector2.zero;

    public bool ClearHighScores;

    public bool ShowHighScores { get; set; }

    private GameManager mGameManager;

    private float y = 50;

    private int mLevelChosen;
    private int mNumbersOfHighInChosenLevel;


	// Use this for initialization
	void Start ()
	{
	    mGameManager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if(ClearHighScores)
        {
            mGameManager.ClearHighScores();
        }
  
    }


    
    void OnGUI()
    {
        GUI.color = Color.black;

        if(!ShowHighScores)
        {
            int y = 50;


            for (int number = 0; number < Rows; number++)
            {
                if (GUI.Button(new Rect(Screen.width - (Screen.width / 1.5f), y, 100, 50), "Level " + ((number * 3) + 1)))
                {
                    mLevelChosen = ((number * 3) + 1);
                    ShowHighScores = true;
                }
                if(GUI.Button(new Rect(Screen.width - (Screen.width / 2), y, 100, 50), "Level " + ((number * 3)+2)))
                {
                    mLevelChosen = ((number * 3) + 2);
                    ShowHighScores = true;
                }
                if (GUI.Button(new Rect(Screen.width - (Screen.width / 3), y, 100, 50), "Level " + ((number * 3) + 3)))
                {
                    mLevelChosen = ((number * 3) + 3);
                    ShowHighScores = true;
                }
                    y += 75;
            }
        }
        else if (ShowHighScores)
        {

            
            scrollPosition = GUI.BeginScrollView(new Rect(0, 0, Screen.width, Screen.height - 5), scrollPosition, new Rect(0, 0, 500, mNumbersOfHighInChosenLevel * 50));
            mNumbersOfHighInChosenLevel = 0;
            GUI.Label(new Rect(Screen.width - (Screen.width / 1.5f), 0, 50, 50), "Player navn");
            GUI.Label(new Rect(Screen.width - (Screen.width / 2), 0, 50, 50), "High Score");
            GUI.Label(new Rect(Screen.width - (Screen.width / 3), 0, 50, 50), "Level");

            int y = 50;


            foreach (SaveAndLoad.ScoreEntry tScoreEntry in mGameManager.HighScores)
            {
                if(tScoreEntry.Level == mLevelChosen)
                {
                    GUI.Label(new Rect(Screen.width - (Screen.width / 1.5f), y, 50, 50), tScoreEntry.CurrentPlayerName);
                    GUI.Label(new Rect(Screen.width - (Screen.width / 2), y, 50, 50), tScoreEntry.HighScore.ToString());
                    GUI.Label(new Rect(Screen.width - (Screen.width / 3), y, 50, 50), tScoreEntry.Level.ToString());
                    mNumbersOfHighInChosenLevel++;
                    y += 50;
                }
            }
            GUI.EndScrollView();
        
        }
      

    }
}
