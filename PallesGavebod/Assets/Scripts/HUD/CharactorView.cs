using UnityEngine;
using System.Collections;

public class CharactorView : Button
{
    public string Name;

    public int CurrentView;
    public int Price;

    private int mLastView;

    public bool Last { get; set; }
    public bool AreBusyAnimating { get; set; }
    public bool Locked;

    public float WaitTime;

    public Vector3 CenterScale;
    public Vector3 LeftAndRightScale;

    public Material LockedMaterial;
    public Material UnLockedMaterial;

    private GameManager mGameManager;

    public int HighestValue { get; set; }
    public int SmallestValue { get; set; }


	// Use this for initialization
	void Start ()
	{
        mLastView = CurrentView;
        mGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Invoke("LoadLockthing",0);
       
	}


	
	// Update is called once per frame
	void Update () {
       
        if(Time.time > 0.001f)
        {
            if (mLastView != CurrentView)
            {
                CheckViewAndSwitchIfChanged();
            }
            SetMaterialLockedOrNot();

            if (KeyPressed && !Locked)
            {
                LoadLevel();
                KeyPressed = false;
            }

            mGameManager.SaveUnlockThing(new UnlockThing()
            {
                Name = Name,
                locked = Locked
            });
    
        }

        
	}

    void LoadLockthing()
    {
        mGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Locked = mGameManager.LoadUnlockThing(new UnlockThing() { Name = Name, locked = Locked }).locked;
    }
    void LoadLevel()
    {
        Application.LoadLevel("Level" + PermenentVariables.Level);
    }

    void UnlockThing()
    {
        Locked = mGameManager.IsLockedOrUnlocked(Name);
    }

    void SetMaterialLockedOrNot()
    {
        if(Locked)
        {

            renderer.material = LockedMaterial;
        }
        else if(!Locked)
        {
            renderer.material = UnLockedMaterial;
       }

        if (CurrentView == 0)
        {
            transform.FindChild("Text").gameObject.SetActive(true);
        }
        if (CurrentView != 0)
        {
            transform.FindChild("Text").gameObject.SetActive(false);
        }
    }

    void CheckViewAndSwitchIfChanged()
    {
        mLastView = CurrentView;
        AreBusyAnimating = true;
        Invoke("AreNotBusyAnymore",WaitTime);
        if(CurrentView < -2)
        {
            if (Last)
            {
                CurrentView = HighestValue;
            }
        }
        if (CurrentView == -2)
        {
            ScaleAndSetPostionOverTime(new Vector3(0,0,0), new Vector3(0f, 1.143867f, 3.285506f), 1f);
        }
        if(CurrentView == -1)
        {
            renderer.enabled = true;
            ScaleAndSetPostionOverTime(LeftAndRightScale, new Vector3(-5.661687f, 1.143867f, -1.756212f), 2f);
        }
        if (CurrentView == 0)
        {
            ScaleAndSetPostionOverTime(CenterScale, new Vector3(-10.13573f, 1.143867f, -1.756212f), 2f);
        }
        if (CurrentView == 1)
        {
            renderer.enabled = true;
            ScaleAndSetPostionOverTime(LeftAndRightScale, new Vector3(-14.57369f, 1.143867f, -1.756212f), 2f);
        }
        if (CurrentView == 2)
        {
            ScaleAndSetPostionOverTime(new Vector3(0, 0, 0), new Vector3(-20.66671f, 1.143867f, 3.285506f), 1f);
          
        }
        if(CurrentView > 2)
        {
            if (Last)
            {
                CurrentView = SmallestValue;
            }
        }

        
    }

    void AreNotBusyAnymore()
    {
        AreBusyAnimating = false;
    }

  

    void ScaleAndSetPostionOverTime(Vector3 pScale,Vector3 pPosition,float pTime)
    {
        iTween.MoveTo(gameObject, pPosition, pTime);
        iTween.ScaleTo(gameObject,pScale,pTime);
    }
}
