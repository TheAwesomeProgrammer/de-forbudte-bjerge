using UnityEngine;
using System.Collections;

public class CharactorViewManager : MonoBehaviour
{
    private CharactorView[] mCharactorViews;

    public float CountNumberUp;

    private float mStarNumberToWrite;

	// Use this for initialization
	void Start ()
	{
	    PermenentVariables.Stars = 100;
	}
	
	// Update is called once per frame
	void Update () {

        mCharactorViews = GetComponentsInChildren<CharactorView>();
        WriteStarCount();
        CalculateStarNumberToWrite();
	}

    void WriteStarCount()
    {
        if(mStarNumberToWrite < 10)
        {
            GameObject.Find("StarFourNumber").GetComponent<TextMesh>().text = "";
            GameObject.Find("StarThreeNumber").GetComponent<TextMesh>().text = "";
            GameObject.Find("StarTwoNumber").GetComponent<TextMesh>().text = mStarNumberToWrite.ToString("F0");
            GameObject.Find("StarOneNumber").GetComponent<TextMesh>().text = "";
        }
        if(mStarNumberToWrite >= 10)
        {
            GameObject.Find("StarFourNumber").GetComponent<TextMesh>().text = "";
            GameObject.Find("StarThreeNumber").GetComponent<TextMesh>().text = "";
            GameObject.Find("StarTwoNumber").GetComponent<TextMesh>().text = mStarNumberToWrite.ToString("F0");
            GameObject.Find("StarOneNumber").GetComponent<TextMesh>().text = "";
        }
        if (mStarNumberToWrite >= 100)
        {
            GameObject.Find("StarFourNumber").GetComponent<TextMesh>().text = "";
            GameObject.Find("StarThreeNumber").GetComponent<TextMesh>().text = mStarNumberToWrite.ToString("F0");
            GameObject.Find("StarTwoNumber").GetComponent<TextMesh>().text = "";
            GameObject.Find("StarOneNumber").GetComponent<TextMesh>().text = "";
        }
        if (mStarNumberToWrite >= 1000)
        {
            GameObject.Find("StarFourNumber").GetComponent<TextMesh>().text = mStarNumberToWrite.ToString("F0");
            GameObject.Find("StarThreeNumber").GetComponent<TextMesh>().text = "";
            GameObject.Find("StarTwoNumber").GetComponent<TextMesh>().text = "";
            GameObject.Find("StarOneNumber").GetComponent<TextMesh>().text = "";
        }
        
    }

    void CalculateStarNumberToWrite()
    {
        if(PermenentVariables.Stars < mStarNumberToWrite)
        {
            mStarNumberToWrite -= CountNumberUp;
        }
        else if (PermenentVariables.Stars > mStarNumberToWrite)
        {
            mStarNumberToWrite += CountNumberUp;
        }
    }

    public bool AreChildrenBusy()
    {
        bool tBusy = false;
        foreach (CharactorView tCharactorView in mCharactorViews)
        {
            if(tCharactorView.AreBusyAnimating)
            {
                tBusy = true;
            }
        }

        return tBusy;
    }

    public CharactorView FindCenterView()
    {

        CharactorView tCenterCharactorView = null;
        foreach (CharactorView tCharactorView in mCharactorViews)
        {
            if (tCharactorView.CurrentView == 0)
            {
                tCenterCharactorView = tCharactorView;
            }
        }

        return tCenterCharactorView;
    }

    public void ShiftAllChildren(bool pPlus)
    {
        CharactorView tSmallestValue = FindSmallestValue(mCharactorViews);
        CharactorView tHighestValue = FindHighestValue(mCharactorViews);

        tSmallestValue.Last = true;

        tHighestValue.Last = true;

        tHighestValue.SmallestValue = tSmallestValue.CurrentView;
        tSmallestValue.HighestValue = tHighestValue.CurrentView;

        foreach (CharactorView tCharactorView in mCharactorViews)
        {
            if(pPlus)
            {
                tCharactorView.CurrentView++;
            }
            else if (!pPlus)
            {
                tCharactorView.CurrentView--;
            }
           
        }

     
    }

    CharactorView FindSmallestValue(CharactorView[] pCharactorViews)
    {
        int tNumber = int.MaxValue;
        CharactorView tSmallestCharactorView = null;
        foreach (CharactorView tCharactorView in pCharactorViews)
        {
            if(tCharactorView.CurrentView < tNumber)
            {
                tNumber = tCharactorView.CurrentView;
                tSmallestCharactorView = tCharactorView;
            }
        }

        return tSmallestCharactorView;
    }

    CharactorView FindHighestValue(CharactorView[] pCharactorViews)
    {
        int tNumber = int.MinValue;
        CharactorView tHighestCharactorView = null;
        foreach (CharactorView tCharactorView in pCharactorViews)
        {
            if (tCharactorView.CurrentView > tNumber)
            {
                tNumber = tCharactorView.CurrentView;
                tHighestCharactorView = tCharactorView;
            }
        }

        return tHighestCharactorView;
    }
}
