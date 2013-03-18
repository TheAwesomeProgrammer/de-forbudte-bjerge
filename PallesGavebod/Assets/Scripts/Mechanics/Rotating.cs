using UnityEngine;
using System.Collections;

public class Rotating : MonoBehaviour
{
    public float RotatingSpeed= 5.0F;

    private bool Visible = false;

	// Use this for initialization
	void Start ()
	{
	    Visible = false;
	}
	
	// Update is called once per frame
	void Update () {
if(Visible)
{
    Rotate();
}
	
	}

    void Rotate()
    {

        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + RotatingSpeed * Time.deltaTime, 0);
    }

    void OnBecameVisible()
    {
        Visible = true;
    }
    void OnBecameInvisible()
    {
        Visible = false;
    }
}
