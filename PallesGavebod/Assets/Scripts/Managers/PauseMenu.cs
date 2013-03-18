using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    public GameObject ExitObj;
    public GameObject ResumeObj;
    public GameObject OptionObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
	{
         Pause();
	}
	}

    void Pause()
    {
        if(Time.timeScale == 1)
        {
           PauseScreen();
        }
        else if (Time.timeScale == 0)
        {
            Resume();
        }
    }

    public void PauseScreen()
    {
        ExitObj.SetActive(true);
        ResumeObj.SetActive(true);
        OptionObj.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        ExitObj.SetActive(false);
        ResumeObj.SetActive(false);
        OptionObj.SetActive(false);
    }


}
