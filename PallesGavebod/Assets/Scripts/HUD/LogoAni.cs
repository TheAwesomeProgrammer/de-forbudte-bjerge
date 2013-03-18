using UnityEngine;
using System.Collections;

public class LogoAni : MonoBehaviour {

    public float TimeBetweenAnimation = 1;
    public Material[] Materials;

    private AnimationsManager mAnimationsManager;
    public float StartTime;

    private bool OneTime = false;

	// Use this for initialization
	void Start () {
        mAnimationsManager = GameObject.Find("AnimationManager").GetComponent<AnimationsManager>();
	    StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, false, true));
	    StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	if(mAnimationsManager.Finish && Time.time > 1 && !OneTime)
	{
	    OneTime = true;
        StartCoroutine(mAnimationsManager.MakeAnimation(Materials, renderer, TimeBetweenAnimation, false, true, false));
	}
    if (Time.time > StartTime + 2)
    {
        Application.LoadLevel("Menu");
    }
	}
}
