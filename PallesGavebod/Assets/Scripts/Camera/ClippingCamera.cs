using UnityEngine;
using System.Collections;

public class ClippingCamera : MonoBehaviour
{

    public float ClosesZoomDistance;
    public float LongestZoomDistance;
    public float LowestYValue;
    public float HighestYValue;

    public bool Zoom { get; set; }
    public bool ZoomBack { get; set; }
    

    private Transform mTarget;

    private Camera mCamera;

	// Use this for initialization
	void Start ()
	{
	    mTarget = GameObject.FindGameObjectWithTag("Player").transform;
        mCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
    void Update()
    {

        /*    Vector3 tRelativePos = transform.position - (mTarget.position);

            RaycastHit tHit;
            if (Physics.Raycast(mTarget.position, tRelativePos, out tHit, Vector3.Distance(transform.position, mTarget.position)))
            {
                if (tHit.collider.tag == "Floor" && tHit.transform.position.y > mTarget.position.y)
                {
                    ZoomIn();
                }
            }
            if (Physics.Raycast(mTarget.position, -tRelativePos, out tHit, Vector3.Distance(transform.position,mTarget.position)))
            {
                if (tHit.collider.tag == "Floor")
                {
                    ZoomOut();
                }
            }*/

        if (Zoom)
        {
            ZoomIn();
        }
        if (ZoomBack)
        {
            ZoomOut();
        }

    }

    void ZoomIn()
    {
      

        if (transform.localPosition.y > LowestYValue)
        {
            transform.localPosition += new Vector3(0, -.1f, 0);
        }
    
       
    }

    void ZoomOut()
    {
       
           
            if (transform.localPosition.y < HighestYValue)
            {
              
                transform.localPosition += new Vector3(0, +.1f, 0);
            }
                
              
          
        
    }
    
}
