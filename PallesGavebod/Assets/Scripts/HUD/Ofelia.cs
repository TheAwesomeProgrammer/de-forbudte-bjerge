using UnityEngine;
using System.Collections;

public class Ofelia : MonoBehaviour
{

    private bool mInvisble;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        if(transform.position.y <= -10)
        {
            GameObject.Find("ThomasLarsen").GetComponent<ThomasMenuRunning>().Run = true;
            mInvisble = true;
        }

	if(mInvisble)
	{
        if (GameObject.Find("ThomasLarsen").GetComponent<ThomasMenuRunning>().ReachedEnd && GameObject.Find("NewGameButton").GetComponent<NewGameButton>().KeyPressed)
        {
            Application.LoadLevel("InputName");
        }
        else if (GameObject.Find("ThomasLarsen").GetComponent<ThomasMenuRunning>().ReachedEnd && GameObject.Find("Load Game Button").GetComponent<LoadGameButton>().KeyPressed)
        {
            Application.LoadLevel("ChooseSave");
        }
        

	}
	}

  
        
       
       
 
    
}
