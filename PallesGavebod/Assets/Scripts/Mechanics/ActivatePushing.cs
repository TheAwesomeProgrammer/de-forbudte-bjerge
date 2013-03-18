using UnityEngine;
using System.Collections;

public class ActivatePushing : MonoBehaviour
{


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider pCollider)
    {
    if(pCollider.tag == "Player")
    {
        Activate();
        
    }
    }
    void Activate()
    {
        GameObject.Find("PushingUpArea").GetComponent<AirPusher>().Activated = true;
    }
}
