using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{

    public Material NormalMaterial;
    public Material HoverMaterial;

    public bool ShouldThomasRun;
    public bool ShouldUseMaterials;
    public bool Scale;
    public bool MoveTo;

    public float ScaleBy;
    public float ScaleTime;
    public float MoveToTime;

    public float MaxRandomTime;
    public float MinRandomTime;

    public float TimeToInActivity;

    public Vector3 MoveToVector3;


    public bool KeyPressed { get; set; }
    public bool MouseEnter { get; set; }
    public bool MouseExit { get; set; }
    public bool Activity { get; set; }

    protected Vector3 mStartPosition;
    protected Vector3 mStartScale;

    protected float mStartTime;
    protected float mRandomNonActivityTime;

    protected ThomasMenuRunning mMenuRunning;

	// Use this for initialization
	public virtual void Start ()
	{
	    mStartPosition = transform.position;
	    mStartScale = transform.localScale;
	    mStartTime = Time.time;
	    mRandomNonActivityTime = Time.time + Random.Range(MinRandomTime, MaxRandomTime);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if(ShouldThomasRun)
        {
            mMenuRunning = GameObject.Find("ThomasLarsen").GetComponent<ThomasMenuRunning>();
        }
        MakeAnimationSinceNoActivity();
   
	}

    protected virtual void MakeAnimationSinceNoActivity()
    {
        if(Activity)
        {
            mRandomNonActivityTime = Random.Range(MinRandomTime, MaxRandomTime) + Time.time;
        }
  
        if(mRandomNonActivityTime < 0 && !Activity )
        {
            mRandomNonActivityTime =  Random.Range(MinRandomTime, MaxRandomTime) + Time.time;
        }
        else if (Time.time > mRandomNonActivityTime && !Activity)
        {
            StartCoroutine(DoActivity());
            mRandomNonActivityTime = float.MinValue;
        }
    }

    protected virtual IEnumerator DoActivity()
    {
        yield return null;
    }

    protected virtual void OnMouseEnter()
    {
        Activity = true;
        MouseEnter = true;
        MouseExit = false;

        if(ShouldUseMaterials)
        {
            renderer.material = HoverMaterial;
        }
     if (Scale)
     {
         ScaleUp();
     }
  
    }


    protected virtual void OnMouseExit()
    {
        MouseEnter = false;
        MouseExit = true;

        if (ShouldUseMaterials)
        {
            renderer.material = NormalMaterial;
        }
        if (Scale)
        {
            ScaleBack();
        }
        Invoke("SetActivityFalse",TimeToInActivity);
    }

    protected virtual void SetActivityFalse()
    {
        Activity = false;
    }

    protected void ScaleUp()
    {
        iTween.ScaleTo(gameObject, new Vector3(transform.localScale.x + ScaleBy, transform.localScale.y + ScaleBy,transform.localScale.z + ScaleBy), ScaleTime);
    }

    protected void ScaleBack()
    {
        iTween.ScaleTo(gameObject, mStartScale, ScaleTime);
    }

    protected void MoveToPosition()
    {
        iTween.MoveTo(gameObject, MoveToVector3,MoveToTime);
    }
    protected void MoveBackToStartPosition()
    {
       iTween.MoveTo(gameObject,mStartPosition,MoveToTime);
    }

    protected virtual void OnMouseDown()
    {
        KeyPressed = true;
        if (ShouldThomasRun)
        {
            mMenuRunning.Run = true;
        }
    }
}
