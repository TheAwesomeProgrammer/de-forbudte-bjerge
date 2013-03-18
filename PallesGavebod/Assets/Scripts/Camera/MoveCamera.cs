using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{

    private Transform mPlayer;
    private Vector3 mLastPlayerRotation;

	// Use this for initialization
	void Start () {
        mPlayer = GameObject.FindGameObjectWithTag("Player").transform;
	    mLastPlayerRotation = mPlayer.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
        SetCameraAccordenlyToPlayer();
	}

    void SetCameraAccordenlyToPlayer()
    {
       
        if (mLastPlayerRotation != mPlayer.eulerAngles)
        {
            Vector3 tDiffRotation = mLastPlayerRotation - mPlayer.eulerAngles;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y - tDiffRotation.y ,transform.eulerAngles.z);
        }
        Vector3 tWantedPostion = new Vector3(mPlayer.position.x-1,mPlayer.position.y+1.5f,mPlayer.position.z );
        transform.position = Vector3.Slerp(transform.position, tWantedPostion, 1f);

        Quaternion tLookPostion = Quaternion.LookRotation(mPlayer.position - transform.position);

       

      


        mLastPlayerRotation = mPlayer.eulerAngles;
     
    }
}
