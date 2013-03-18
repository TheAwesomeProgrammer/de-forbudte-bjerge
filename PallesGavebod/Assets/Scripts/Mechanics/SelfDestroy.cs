using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour
{

    public float LifeTime;

	// Use this for initialization
	void Start () {
	Destroy(gameObject,LifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
