using UnityEngine;
using System.Collections;

public class ChooseSave : Button
{

    public int Level;

    public Material LockTexture;
    public Material UnLockTexture;

    public Rect Placement;

    private GameManager mGameManager;

    private bool Locked;

    private int mLevelChosen;
    
	// Use this for initialization
	public override void Start ()
	{
        base.Start();
	    renderer.enabled = false;
        mGameManager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
        mGameManager.Load(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if(PermenentVariables.Level < Level)
        {
            Locked = true;
            renderer.material = LockTexture;
            NormalMaterial = LockTexture;
            HoverMaterial = LockTexture;
        }
        else
        {
            Locked = false;
            renderer.material= UnLockTexture;
            NormalMaterial = UnLockTexture;
            HoverMaterial = UnLockTexture;
        }


          
        if(GameObject.Find("Main Camera").GetComponent<ChoosePlayer>().HasChosen)
        {
            renderer.enabled = true;
            if (KeyPressed && !Locked)
            {
                Application.LoadLevel("CharactorView");
            }
           
                
        }
        }

 
      
           
        
    }



    
   



