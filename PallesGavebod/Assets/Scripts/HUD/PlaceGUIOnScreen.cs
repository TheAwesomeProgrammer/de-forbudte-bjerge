using UnityEngine;
using System.Collections;

public class PlaceGUIOnScreen : MonoBehaviour
{

    public Vector2 Position;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

        Vector3 tWantedPostion = GameObject.Find("Camera").GetComponent<Camera>().ViewportToScreenPoint(Position);

        guiTexture.pixelInset = new Rect(tWantedPostion.x,tWantedPostion.y,guiTexture.pixelInset.width, guiTexture.pixelInset.height);
	}
}
