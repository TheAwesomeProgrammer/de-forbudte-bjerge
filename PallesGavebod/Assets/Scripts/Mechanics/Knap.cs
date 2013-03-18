using UnityEngine;
using System.Collections;

public class Knap : MonoBehaviour
{
    public GameObject ButtonDown;
    public bool LeftWall;
    public bool PlayerCanPresButtonDown;
    public bool PickupItemCanPresButtonDown;
    public bool Disappear;
    public bool Appear;

    public GameObject[] BoxesToDisappearOrAppear;

    public bool Hit { get; set; }

    private Spawner mSpawner;

    




	// Use this for initialization
	void Start () {
        mSpawner = GameObject.Find("Spawner").GetComponent<Spawner>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Hit)
        {
            Hit = false;
            DisAndAppear();
            if (transform.GetChild(0).name == "KnapDown")
            {
            Instantiate(transform.GetChild(0), transform.position - new Vector3(0, -0.263573f, 0), Quaternion.Euler(0, 0, transform.eulerAngles.z));
            mSpawner.SpawnThingsWhenButtonPressed(GetComponent<SpawnInterface>().Id);
            Destroy(gameObject);
            }
        }
	}

    public void DisAndAppear()
    {
        if (GameObject.FindGameObjectsWithTag("DisappearBox").Length > 0)
        {
            foreach (GameObject tGameobject in GameObject.FindGameObjectsWithTag("DisappearBox"))
            {
                if(tGameobject != null && tGameobject.GetComponent<SpawnItem>() != null)
                {
                    if (tGameobject.GetComponent<SpawnItem>().Id == GetComponent<SpawnItem>().Id)
                    {
                        if (Appear)
                        {
                            tGameobject.collider.isTrigger = false;
                            iTween.ColorTo(tGameobject, new Color(1, 1, 1, 1), 0.75f);
                        }
                        if (Disappear)
                        {
                            tGameobject.collider.isTrigger = true;
                            iTween.ColorTo(tGameobject,new Color(1,1,1,0),0.75f);
                        }
                    }
                }
                   

            }
        }
    }

    void OnTriggerEnter(Collider pCollider)
    {
        if(pCollider.tag =="PickupItem" && LeftWall && PickupItemCanPresButtonDown)
        {
            Instantiate(transform.GetChild(0), transform.position - new Vector3(0.301256f,0,0), Quaternion.Euler(0, 0, transform.eulerAngles.z));
            mSpawner.SpawnThingsWhenButtonPressed(GetComponent<SpawnInterface>().Id);
            Destroy(gameObject);
        }
        if (pCollider.tag == "PickupItem" && !LeftWall && PickupItemCanPresButtonDown)
        {
            Instantiate(transform.GetChild(0), transform.position + new Vector3(0.301256f, 0, 0), Quaternion.Euler(0, 0, -90));
            mSpawner.SpawnThingsWhenButtonPressed(GetComponent<SpawnInterface>().Id);
            Destroy(gameObject);
        }
      
       
    }
    

}
