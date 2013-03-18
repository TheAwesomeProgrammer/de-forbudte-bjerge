using UnityEngine;
using System.Collections;

public class TNT : MonoBehaviour
{

    public GameObject Explosion;
    public Material TNT3Sec;
    public Material TNT2Sec;
    public Material TNT1Sec;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator CountDownToExplosion()
    {
        renderer.material = TNT3Sec;
        yield return new WaitForSeconds(1);
        renderer.material = TNT2Sec;
        yield return new WaitForSeconds(1);
        renderer.material = TNT1Sec;
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("Explosion").transform.position = transform.position;
        GameObject.FindGameObjectWithTag("Explosion").GetComponent<Detonator>().Explode();
        Destroy(gameObject);
    }

    public void Explode()
    {
        GameObject.FindGameObjectWithTag("Explosion").transform.position = transform.position;
        GameObject.FindGameObjectWithTag("Explosion").GetComponent<Detonator>().Explode();
        Destroy(gameObject);
    }
}
