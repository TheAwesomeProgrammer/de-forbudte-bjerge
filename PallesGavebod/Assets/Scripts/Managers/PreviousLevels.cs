using UnityEngine;
using System.Collections.Generic;

public class PreviousLevels : MonoBehaviour
{

    public List<string> PreviousLevelsList { get; private set; }

    private string mCurrentLevel;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        mCurrentLevel = Application.loadedLevelName;
        PreviousLevelsList = new List<string>();
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(mCurrentLevel != Application.loadedLevelName)
	{
	    PreviousLevelsList.Add(mCurrentLevel);
	    mCurrentLevel = Application.loadedLevelName;
	}
	}

    public string GetPreviousLevel()
    {
        string tStringToReturn = null;
        if(PreviousLevelsList.Count > 0)
        {
            tStringToReturn = PreviousLevelsList.ToArray()[PreviousLevelsList.Count - 1];
            PreviousLevelsList.Remove(tStringToReturn);
        }
       
        return tStringToReturn;
    }
}
