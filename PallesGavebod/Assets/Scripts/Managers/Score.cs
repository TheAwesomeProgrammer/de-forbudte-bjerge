using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{

    public float TimeToNextScoreJudgement;

	// Use this for initialization
	void Start ()
	{

	    StartCoroutine(ScoreJudgement());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator ScoreJudgement()
    {
        Debug.Log("TIME"+Time.time);
       if(Time.time > TimeToNextScoreJudgement && PermenentVariables.Score > 0)
       {
           PermenentVariables.Score--;
       }
        
        yield return new WaitForSeconds(TimeToNextScoreJudgement);
        StartCoroutine(ScoreJudgement());
    }
}
