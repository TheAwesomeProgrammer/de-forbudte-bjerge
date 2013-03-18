using UnityEngine;
using System.Collections;

public class ChoosePlayer : MonoBehaviour {

    public Rect WindowRect = new Rect(20, 20, 120, 50);
    public Rect PlayerRect = new Rect(20, 20, 120, 50);
    public Rect DeletePlayerRect = new Rect(20, 20, 120, 50);

    public Vector2 scrollPosition;

    public bool HasChosen { get; private set; }

    private GameManager mGameManager;

	// Use this for initialization
	void Start () {
        mGameManager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
        mGameManager.Load(true);
	}
	
	// Update is called once per frame
	void Update () {
        mGameManager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
        mGameManager.Load(true);
       
        WindowRect.height = (GameObject.Find("SaveAndLoadManager").GetComponent<SaveAndLoad>().LocalHighScores.Count * (PlayerRect.height * 2) + 50);
        WindowRect = new Rect(Screen.width / 2 - (WindowRect.width / 2), 0, WindowRect.width, WindowRect.height);
       
	}

    void OnGUI()
    {

    if(!HasChosen)
    {
        WindowRect = GUI.Window(0, WindowRect, DoMyWindow, "Vælg brugernavn");
    }
        

    }

    void DoMyWindow(int windowID)
    {
        mGameManager = GameObject.Find("Gamemanager").GetComponent<GameManager>();

       
        PlayerRect.y = 50;

        foreach (SaveAndLoad.ScoreEntry tScoreEntry in mGameManager.HighScores)
        {
            if(GUI.Button(new Rect(PlayerRect.x+PlayerRect.width,PlayerRect.y,DeletePlayerRect.width,DeletePlayerRect.height),"X" ))
            {
                mGameManager.RemoveLocal(tScoreEntry.CurrentPlayerName);
            }

            if(GUI.Button(PlayerRect,""+tScoreEntry.CurrentPlayerName))
            {
                PermenentVariables.PlayerName = tScoreEntry.CurrentPlayerName;
                PermenentVariables.Level = tScoreEntry.Level;
                PermenentVariables.Stars = tScoreEntry.Star;
                HasChosen = true;
            }
            
            PlayerRect.y += 50;
        }
        
    }
}
