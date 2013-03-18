using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections.Generic;

public class OnlineHighscores : MonoBehaviour
{

    private string addScoreURL = "http://pallesgavebod.bugs3.com//addscore.php?"; //be sure to add a ? to your url
    private string deleteScoreURL = "http://pallesgavebod.bugs3.com//removescore.php?";
    private string highscoreURL = "http://pallesgavebod.bugs3.com//display.php?";

    private List<SaveAndLoad.ScoreEntry> mScoreEntries;

    private InputName mInputName;
    private string pPlayerName = "";
    private string secretKey = "penis"; // Edit this value and make sure it's the same as the one stored on the server

    public bool Post { get; set; }
    public bool HasLoaded { get; set; }


    private void Start()
    {
        //mScoreEntries = ReturnHighScores();
    }

    private void Update()
    {
     
    }





    // remember to use StartCoroutine when calling this function!
    public IEnumerator PostScores(string name, int score, int level)
    {

        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        //  string hash = Md5.Md5Sum(name + score + level + secretKey);
        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&level=" + level;

       Debug.Log("POSTURL "+post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post;


      
        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }

    public void DeleteScores(string name, int score, int level)
    {

        string hash = Md5.Md5Sum(name + score + level + secretKey);

        string post_url = deleteScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&level=" + level;

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);

        while (!hs_post.isDone){}
        // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }


    public List<SaveAndLoad.ScoreEntry> ReturnHighScores()
    {
     
            StartCoroutine(GetScores());
      
        

        if (mScoreEntries == null)
        {
            mScoreEntries = new List<SaveAndLoad.ScoreEntry>();
        }
        return mScoreEntries;
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    private IEnumerator GetScores()
    {
  
        WWW hs_get = new WWW(highscoreURL);

        
        yield return hs_get;

   

        if (hs_get.error != null)
        {

            print("There was an error getting the high score: " + hs_get.error);
            yield return null;
        }
     
            
            string tFinalText = hs_get.text;
        
            string[] digits = Regex.Split(tFinalText, @"\D+");

            int numberCount = 0;
            int tLevel = 0;
            int tScore = 0;
            int tNumber = 0;
            int tTimesRunScore = 0;
            int tTimesRunlevel = 0;
            int tLastTimesRunScore = tTimesRunScore;
            int tLastTimesRunlevel = tTimesRunlevel;


            foreach (string tValue in digits)
            {
                string onlyLetters = new String(tFinalText.Where(Char.IsLetter).ToArray());

                string[] tPlayerNames = onlyLetters.Split(new string[] {"xx"}, StringSplitOptions.None);



                if (int.TryParse(tValue, out tNumber))
                {
                    if (numberCount == (1 + (2*tTimesRunScore)))
                    {
                        tScore = tNumber;
                        tTimesRunScore++;
                    }
                    if (numberCount == (2 + (2*tTimesRunlevel)))
                    {
                        tLevel = tNumber;
                        tTimesRunlevel++;
                    }

                    if (tTimesRunScore > tLastTimesRunScore && tTimesRunlevel > tLastTimesRunlevel)
                    {
                        tLastTimesRunScore = tTimesRunScore;
                        tLastTimesRunlevel = tTimesRunlevel;

                        if (tLastTimesRunScore <= tPlayerNames.Length && mScoreEntries.Count < (tPlayerNames.Length - 1))
                        {
                            
                            mScoreEntries.Add(new SaveAndLoad.ScoreEntry
                                                  {
                                                      CurrentPlayerName = tPlayerNames[tLastTimesRunScore - 1],
                                                      HighScore = tScore,
                                                      Level = tLevel
                                                  });
                        }

                    }



                }
                numberCount++;
            }
        print("Highscore length "+mScoreEntries.Count);
        HasLoaded = true;
    }
}