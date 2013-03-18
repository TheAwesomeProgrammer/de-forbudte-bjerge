using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    renderer.material.mainTextureOffset += new Vector2(.01f * Time.deltaTime, 0);
	}

    void OnCollisionStay(Collision pCollisionObject)
    {
        Collider pCollider = pCollisionObject.collider;
        Debug.Log("TAG "+pCollider.tag);
        if(pCollider.tag == "Player" || pCollider.tag == "PlayerPart")
        {
            Destroy(pCollider.gameObject);
        }
    }
}
