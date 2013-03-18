using UnityEngine;
using System.Collections;

 [System.Serializable]
public class PointsToTake
{
    public int PointsTo1Star;
    public int PointsTo2Star;
    public int PointsTo3Star;
}


public class MellemLevelMenu : MonoBehaviour
{

    public PointsToTake[] PointsToGive;

    public GameObject Star;
    public GameObject BlankStar;

    private TextMesh mScoreText;
    private TextMesh mTimeText;
    private TextMesh mTotalScoreText;

    public float WaitTimeBetweenStars;
    public float WritingCount;

    private float mScoreToWrite;
    private float mTimeToWrite;
    private float mTotalScoreToWrite;

    private float mTime;

    private bool mHasCalculatedAll;

    private int mNumberOfStars;
    private int mNumberOfBlankStars;
    private int mNumberOfTimesToSave = 0;

	// Use this for initialization
	void Start ()
	{
        PermenentVariables.TotalScore = (int)Mathf.Round(PermenentVariables.Score - ((PermenentVariables.EndTime - PermenentVariables.StartTime) / 3));
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    mScoreText = GameObject.Find("Score").GetComponent<TextMesh>();
        mTimeText = GameObject.Find("Times").GetComponent<TextMesh>();
        mTotalScoreText = GameObject.Find("Total score").GetComponent<TextMesh>();
        WriteText();
        CalculateNumberOfStars();
        
        if(GameObject.FindGameObjectsWithTag("Star").Length < 4 && mHasCalculatedAll)
        {
            StartCoroutine(DrawStars());
        }
       SaveWhenDoneLoadingHighScores();
	}

    void SaveWhenDoneLoadingHighScores()
    {
        if (GameObject.Find("OnlineHighscores").GetComponent<OnlineHighscores>().HasLoaded && mNumberOfTimesToSave <= 0)
        {
            GameObject.Find("Gamemanager").GetComponent<GameManager>().Save();
            mNumberOfTimesToSave++;
        }
        
    }

    void WriteText()
    {
        mTime = PermenentVariables.EndTime - PermenentVariables.StartTime;

        CalculateValuesToWrite();

        mTimeText.text = "Time : " + mTimeToWrite.ToString("F0");
        mScoreText.text = "Score : " + mScoreToWrite.ToString("F0");
        mTotalScoreText.text = "Total score : " + (mTotalScoreToWrite).ToString("F0");
    }

    void CalculateValuesToWrite()
    {
        if((mTimeToWrite) < mTime)
        {
            mTimeToWrite += WritingCount;
            return;
        }
        else if ((mScoreToWrite) < PermenentVariables.Score)
        {
            mScoreToWrite += WritingCount;
            return;
        }
        else if ((mTotalScoreToWrite) < PermenentVariables.TotalScore)
        {
            mTotalScoreToWrite += WritingCount;
            return;
        }
        Mathf.Round(mTotalScoreToWrite);
        mHasCalculatedAll = true;
    }

    void CalculateNumberOfStars()
    {
        if (PermenentVariables.TotalScore <= PointsToGive[PermenentVariables.Level - 1].PointsTo1Star)
        {
            mNumberOfStars = 0;
            mNumberOfBlankStars = 3;
        }
        if (PermenentVariables.TotalScore >= PointsToGive[PermenentVariables.Level - 1].PointsTo1Star)
        {
            mNumberOfStars = 1;
            mNumberOfBlankStars = 2;
        }
        if (PermenentVariables.TotalScore >= PointsToGive[PermenentVariables.Level - 1].PointsTo2Star)
        {
            mNumberOfStars = 2;
            mNumberOfBlankStars = 1;
        }
        if (PermenentVariables.TotalScore >= PointsToGive[PermenentVariables.Level - 1].PointsTo3Star)
        {
            mNumberOfStars = 3;
            mNumberOfBlankStars = 0;
        }
        PermenentVariables.Stars = mNumberOfStars;
    }

    IEnumerator DrawStars()
    {
        for (int i = 0; i < mNumberOfStars;i++ )
        {
            Instantiate(Star, new Vector3(-3.5f + (3.4235135f * i), -2.16f, 0), Star.transform.rotation);
            yield return new  WaitForSeconds(WaitTimeBetweenStars);
        }
        for (int i = mNumberOfStars; i <mNumberOfBlankStars+mNumberOfStars; i++)
        {
            Instantiate(BlankStar, new Vector3(-3.5f + (3.4235135f * i), -2.16f, 0), BlankStar.transform.rotation);
            yield return new WaitForSeconds(WaitTimeBetweenStars);
        }
    }
}
