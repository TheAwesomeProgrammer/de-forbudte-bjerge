using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator OnTriggerEnter(Collider pCollider)
    {
        Collider tCollider = pCollider;
        if(tCollider.tag == "Player")
        {
            PermenentVariables.EndTime = Time.time;
            yield return new WaitForSeconds(0.5f);
            Application.LoadLevel("MellemLevelMenu");
        }
    }
}
