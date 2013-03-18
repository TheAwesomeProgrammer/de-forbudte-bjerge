using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    PermenentVariables.Score = 0;
	    PermenentVariables.StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
