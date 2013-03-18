using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
//You must include these namespaces
//to use BinaryFormatter
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[System.Serializable]
public class UnlockThing
{
    public string Name = "";
    public bool locked = false;
}


public class SaveAndLoad : MonoBehaviour {




    //High score entry
    [System.Serializable]
    public class ScoreEntry
    {
        //Players name
        public string CurrentPlayerName;
        //Score
        public int HighScore;
        //Level
        public int Level;

        public int Star;

        public List<UnlockThing> UnlockThings = new List<UnlockThing>();
    }
 
    //High score table
    public List<ScoreEntry> highScores;
    public List<ScoreEntry> LocalHighScores;

    

    private OnlineHighscores mOnlineHighscores;



    void Start()
    {
        LocalHighScores = Load(true);
    }
    void Update()
    {
       // MakeSureOnlyHighestIsKeept();
    }

    private void MakeSureOnlyHighestIsKeept()
    {
        if (LocalHighScores.Exists(item => item.CurrentPlayerName == PermenentVariables.PlayerName && 
            item.Level < FindHighestLevelInList(LocalHighScores,PermenentVariables.PlayerName)))
        {
            LocalHighScores.Remove(LocalHighScores.Find(item =>  item.Level < FindHighestLevelInList(LocalHighScores,PermenentVariables.PlayerName)));
        }
    }

    public void RemoveLocal(string pPlyerName)
    {
        LocalHighScores = Load(true);
        LocalHighScores.Remove(LocalHighScores.Find(item => item.CurrentPlayerName == pPlyerName));
        Save();
    }


    public void Save()
    {
        //Get a binary formatter
        var b = new BinaryFormatter();
        //Create an in memory stream
        var m = new MemoryStream();
        //Save the scores
        b.Serialize(m, LocalHighScores);
        //Add it to player prefs
        PlayerPrefs.SetString("HighScores", Convert.ToBase64String(m.GetBuffer()));
    }

    int FindHighestLevelInList(List<ScoreEntry> pScoreEntries,string pPlayerName )
    {
        int tHighestLevel = 0;
        foreach (var tScoreEntry in pScoreEntries)
        {
            if(tScoreEntry.Level > tHighestLevel && tScoreEntry.CurrentPlayerName == pPlayerName)
            {
                tHighestLevel = tScoreEntry.Level;
            }
        }
        return tHighestLevel;
    }


    public void UnlockThing(string pPlyerName,UnlockThing pUnlockThing)
    {
      if(LocalHighScores.Exists(item => item.CurrentPlayerName == pPlyerName))
      {
          List<UnlockThing> tUnlockThings = LocalHighScores.Find(item => item.CurrentPlayerName == pPlyerName).UnlockThings;

         
          if(tUnlockThings.Exists(item => item.Name == pUnlockThing.Name && item.locked != pUnlockThing.locked))
          {
              tUnlockThings.Remove(
                  tUnlockThings.Find(item => item.Name == pUnlockThing.Name && item.locked == pUnlockThing.locked));
              tUnlockThings.Add(pUnlockThing);
          }
          if (!tUnlockThings.Exists(item => item.Name == pUnlockThing.Name && item.locked == pUnlockThing.locked))
          {
              tUnlockThings.Add(pUnlockThing);
          }
          if(tUnlockThings.Exists(item => item.Name == pUnlockThing.Name && item.locked != pUnlockThing.locked))
          {
              tUnlockThings.Remove(tUnlockThings.Find(item => item.Name == pUnlockThing.Name && item.locked != pUnlockThing.locked));
          }
       

      }
      else if(!LocalHighScores.Exists(item => item.CurrentPlayerName == pPlyerName))
      {
           
      }
      Save();
    }

    public UnlockThing LoadUnlockThing(string pPlyerName, UnlockThing pUnlockThing)
    {
        if (LocalHighScores.Exists(item => item.CurrentPlayerName == pPlyerName))
        {
            return
                LocalHighScores.Find(item => item.CurrentPlayerName == pPlyerName).UnlockThings.Find(
                    item => item.Name == pUnlockThing.Name);
        }
        else
        {
            return null;
        }
    }


    public void Save(string pPlyerName,int pScore,int pLevel,int pStar)
    {
        mOnlineHighscores = GameObject.Find("OnlineHighscores").GetComponent<OnlineHighscores>();
       
       if(!string.IsNullOrEmpty(pPlyerName))
       {
           pPlyerName = pPlyerName.Replace("\n", String.Empty);
           pPlyerName = pPlyerName.Replace("\r", String.Empty);
           pPlyerName = pPlyerName.Replace("\t", String.Empty);
       }

      

        bool tAddToList = false;
        List<ScoreEntry> tEntriesToRemove = new List<ScoreEntry>();
        foreach (ScoreEntry tScoreEntry in highScores)
        {
      
            if(tScoreEntry.CurrentPlayerName == pPlyerName && tScoreEntry.HighScore < pScore && tScoreEntry.Level == pLevel)
            {
               tEntriesToRemove.Add(tScoreEntry);
               tAddToList = true;
            }
            if (tScoreEntry.CurrentPlayerName == pPlyerName && tScoreEntry.Level != pLevel && !DoesPlayerAndLevelExist(pPlyerName,pLevel))
            {
                tAddToList = true;
            }
        }
        
        if (!highScores.Exists(item => item.CurrentPlayerName == pPlyerName))
        {
            tAddToList = true;
        }
        
        if (LocalHighScores.Exists(item => item.CurrentPlayerName == pPlyerName && item.Level < pLevel))
        {
            print("REMOVE");
            pStar = GetMoney(pStar, LocalHighScores.Find(item => item.CurrentPlayerName == pPlyerName && item.Level < pLevel).Star);

            LocalHighScores.Remove(LocalHighScores.Find(item => item.CurrentPlayerName == pPlyerName && item.Level < pLevel));
        }

    
        if (LocalHighScores.Exists(item => item.CurrentPlayerName == pPlyerName && item.Level < pLevel) ||
                 !LocalHighScores.Exists(item => item.CurrentPlayerName == pPlyerName))
        {
            print("ADD");
            LocalHighScores.Add(new ScoreEntry
            {
                CurrentPlayerName = pPlyerName,
                HighScore = pScore,
                Level = pLevel,
                Star = pStar
            });

        }

        if(tAddToList)
        {

            foreach (ScoreEntry tScoreEntry in tEntriesToRemove)
            {
             
                 mOnlineHighscores.DeleteScores(tScoreEntry.CurrentPlayerName, tScoreEntry.HighScore, tScoreEntry.Level);
                highScores.Remove(tScoreEntry);
              
            }

            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                StartCoroutine(mOnlineHighscores.PostScores(pPlyerName, pScore, pLevel));
            }
        }

        //Get a binary formatter
        var b = new BinaryFormatter();
        //Create an in memory stream
        var m = new MemoryStream();
        //Save the scores
        b.Serialize(m, LocalHighScores);
        //Add it to player prefs
        PlayerPrefs.SetString("HighScores", Convert.ToBase64String(m.GetBuffer()));

        
    }



      public bool DoesPlayerAndLevelExist(string pPlayerName,int pLevel)
      {
         if(highScores.Exists(item => item.CurrentPlayerName == pPlayerName && item.Level == pLevel))
         {
             return true;
         }
          return false;

      }

      int GetMoney(int pGotMoney,int pHasMoney)
      {
          int tMoney = pGotMoney + pHasMoney;

          return tMoney;
      }

    public List<ScoreEntry> Load(bool pLocalLoad)
    {
        mOnlineHighscores = GameObject.Find("OnlineHighscores").GetComponent<OnlineHighscores>();
    if(Application.internetReachability != NetworkReachability.NotReachable && !pLocalLoad)
    {
 
        highScores = mOnlineHighscores.ReturnHighScores();

    }
      
    else if(Application.internetReachability == NetworkReachability.NotReachable || pLocalLoad)
    {
        //Get the data
        
        var data = PlayerPrefs.GetString("HighScores");
        //If not blank then load it
        if (!string.IsNullOrEmpty(data))
        {
          
            //Binary formatter for loading back
            var b = new BinaryFormatter();
            //Create a memory stream with the data
            var m = new MemoryStream(Convert.FromBase64String(data));
            //Load back the scores
            LocalHighScores = (List<ScoreEntry>)b.Deserialize(m);

        }
    }
        if(pLocalLoad)
        {
            return LocalHighScores;
        }
        return highScores;
    }


}