using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{

    public bool Explode { get; set; }

    private float mHowLongToExplode;

    private float mTimeToExplodeIn = float.MinValue;

    private bool mHasExplodedOtherTNTs;

	// Use this for initialization
	void Start ()
	{
	    mHowLongToExplode = GetComponent<Detonator>().destroyTime;
	}
	
	// Update is called once per frame
    void Update()
    {

        if (Explode)
        {
            mTimeToExplodeIn = Time.time + mHowLongToExplode;
        }

        if (Time.time > mTimeToExplodeIn)
        {
            Explode = false;
        }
    if(Explode)
    {
         foreach (GameObject tEnemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (Vector3.Distance(tEnemy.transform.position, transform.position) < GetComponent<Detonator>().size)
                {
                    Destroy(tEnemy);
                }
            }
        if(!mHasExplodedOtherTNTs)
        {
            foreach (GameObject tTNT in GameObject.FindGameObjectsWithTag("TNT"))
            {
                if (Vector3.Distance(tTNT.transform.position, transform.position) < GetComponent<Detonator>().size)
                {
                    tTNT.GetComponent<TNT>().Explode();
                }
            }
            mHasExplodedOtherTNTs = true;
        }
         

         foreach (GameObject tRock in GameObject.FindGameObjectsWithTag("Rock"))
         {
             if (Vector3.Distance(tRock.transform.position, transform.position) < GetComponent<Detonator>().size)
             {
                 Destroy(tRock);
             }
         }
            if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <
                GetComponent<Detonator>().size + 1)
            {

                StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TakeDamage(1));
            }
    }
       
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,GetComponent<Detonator>().size);
    }
}
