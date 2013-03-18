using UnityEngine;
using System.Collections;

public class AirPusher : MonoBehaviour
{
    public float MaxPushForce;
    public float MinPushForce;
    public float PushForce;
    public float AccPushForce;

    public bool Activated { get; set; }

    private PlayerMovements mPlayerMovements;


    private float mPushForce;
    private float mAccPushForce;

    private float mPushHeight = 1.5f;

    private bool mPush;

	// Use this for initialization
	void Start ()
	{
	    mPushForce = PushForce;
	}
	

    
	// Update is called once per frame
	void Update () {
           
           
            mPlayerMovements = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>();

    

        if (mPush)
        {
            if (mPushForce < MaxPushForce)
            {
                mPushForce += AccPushForce;
            }
            mPlayerMovements.AddForce(mPushForce);   
        }
        else
        {
            if (mPushForce > MinPushForce)
            {
                mPushForce -= AccPushForce;
            }
        }
	}
    void OnTriggerEnter(Collider pCollider)
    {
        if (pCollider.tag == "Player")
        {
            mPushForce = pCollider.GetComponent<PlayerMovements>().ReturnMoveDirection().y;
        }
    }

    void OnTriggerStay(Collider pCollider)
    {
         

        if(pCollider.tag == "Player")
        {
            float tNumberToMultiplyPushForceWith = (transform.collider.bounds.max.y* 2) -
                         Mathf.Abs(mPlayerMovements.transform.position.y - transform.parent.transform.position.y);
            tNumberToMultiplyPushForceWith /= transform.collider.bounds.max.y;

            if (pCollider.GetComponent<PlayerMovements>().ReturnMoveDirection().y < 0)
            {
               
                mPushForce = ((PushForce * tNumberToMultiplyPushForceWith) + pCollider.GetComponent<PlayerMovements>().ReturnMoveDirection().y);
              if(mPushForce < 0)
              {
                  mPushForce = 0;
              }
           
            }
            else
            {
                mPushForce = (PushForce*tNumberToMultiplyPushForceWith);
            }
       
            
          //  print("tNumberToMultiplyPushForceWith " + tNumberToMultiplyPushForceWith);
            mPush = true;
        
        }
    }
     void OnTriggerExit(Collider pCollider)
     {
           if(pCollider.tag == "Player")
        {
            mPush = false;

        }
     }




   
}
