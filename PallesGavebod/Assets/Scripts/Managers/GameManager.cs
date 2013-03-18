using UnityEngine;
using System.Collections.Generic;



public class GameManager : MonoBehaviour
{

    

    private SaveAndLoad SaveAndLoadManager;
    public List<SaveAndLoad.ScoreEntry> HighScores;


    private float mLastScore;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
	void Start ()
    {
       
        Load(false);
        if (Application.loadedLevelName != "MellemLevelMenu")
        {
            PermenentVariables.StartTime = Time.time;
            PermenentVariables.Score = 0;
        }

	    SaveAndLoadManager = GameObject.Find("SaveAndLoadManager").GetComponent<SaveAndLoad>();
	   
        
	}
	
	// Update is called once per frame
	void Update ()
	{
        

       if (GameObject.Find("Score") != null && mLastScore != PermenentVariables.Score)
        {
            GameObject.Find("Score").GetComponent<TextMesh>().text = "Score : " + PermenentVariables.Score;
        }
       if (GameObject.Find("Time") != null)
       {
           GameObject.Find("Time").GetComponent<TextMesh>().text = "Time : " +(Time.time - PermenentVariables.StartTime).ToString("F1");
       }
        
       
        if(Input.GetKeyDown(KeyCode.G))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        mLastScore = PermenentVariables.Score;
        MakeSureRightLevel();
	}

    private void MakeSureRightLevel()
    {
        switch (Application.loadedLevelName)
        {
            case "Level1" :
                {
                    PermenentVariables.Level = 1;
                }
                break;
            case "Level2":
                {
                    PermenentVariables.Level = 2;
                }
                break;
            case "Level3":
                {
                    PermenentVariables.Level = 3;
                }
                break;
            case "Level4":
                {
                    PermenentVariables.Level = 4;
                }
                break;
            case "Level5":
                {
                    PermenentVariables.Level = 5;
                }
                break;
        }
    }

    public  void Save()
    {
       
        SaveAndLoadManager = GameObject.Find("SaveAndLoadManager").GetComponent<SaveAndLoad>();
        SaveAndLoadManager.Save(PermenentVariables.PlayerName, PermenentVariables.TotalScore, PermenentVariables.Level, PermenentVariables.Stars);
      
    }

    public void SaveUnlockThing(UnlockThing pUnlockThing)
    {
        SaveAndLoadManager = GameObject.Find("SaveAndLoadManager").GetComponent<SaveAndLoad>();
       SaveAndLoadManager.UnlockThing(PermenentVariables.PlayerName,pUnlockThing);
    }

    public UnlockThing LoadUnlockThing(UnlockThing pUnlockThing)
    {
        SaveAndLoadManager = GameObject.Find("SaveAndLoadManager").GetComponent<SaveAndLoad>();
        return SaveAndLoadManager.LoadUnlockThing(PermenentVariables.PlayerName, pUnlockThing);

    }

    public bool IsLockedOrUnlocked(string pName)
    {
       List<UnlockThing> tUnlockThing =  (SaveAndLoadManager.LocalHighScores.Find(lItem => lItem.CurrentPlayerName == PermenentVariables.PlayerName).UnlockThings);
        return tUnlockThing.Find(item => item.Name == pName).locked;
    }



    public void RemoveLocal(string pPlayername)
    {

        SaveAndLoadManager = GameObject.Find("SaveAndLoadManager").GetComponent<SaveAndLoad>();
        SaveAndLoadManager.RemoveLocal(pPlayername);

    }
    public  void Load(bool pLoadLocal)
    {
        SaveAndLoadManager = GameObject.Find("SaveAndLoadManager").GetComponent<SaveAndLoad>();
        HighScores = SaveAndLoadManager.Load(pLoadLocal);
    }

    public void ClearHighScores()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SwitchLevel(int pLevel)
    {
        switch (pLevel)
        {
            case 1 :
                PermenentVariables.Level = 1;
                Application.LoadLevel("Level1");
                break;
            case 2 :
                PermenentVariables.Level = 2;
                Application.LoadLevel("Level2");
                break;
            case 3 :
                PermenentVariables.Level = 3;
                Application.LoadLevel("Level3");
                break;
            case 4 :
                PermenentVariables.Level = 4;
                Application.LoadLevel("Level4");
                break;
            case 5 :
                PermenentVariables.Level = 5;
                Application.LoadLevel("Level5");
                break;
                    
        }
    }
    public void NextLevel()
    {
        switch (PermenentVariables.Level)
        {
            case 1 :
                PermenentVariables.Level = 2;
                Application.LoadLevel("Level2");
                break;
            case 2:
                PermenentVariables.Level = 3;
                Application.LoadLevel("Level3");
                break;
            case 3:
                PermenentVariables.Level = 4;
                Application.LoadLevel("Level4");
                break;
            case 4:
                PermenentVariables.Level = 5;
                Application.LoadLevel("Level5");
                break;
            case 5 :
                PermenentVariables.Level = 6;
                Application.LoadLevel("Level6");
                break;
            case 6:
                PermenentVariables.Level = 7;
                Application.LoadLevel("Level7");
                break;
            case 7:
                PermenentVariables.Level = 8;
                Application.LoadLevel("Level8");
                break;
     
        }
    }



}
