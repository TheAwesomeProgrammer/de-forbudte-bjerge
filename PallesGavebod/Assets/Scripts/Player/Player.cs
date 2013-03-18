using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int Health = 3;

    public float InbeatableTime = 2.0F;
    public float DamageTakenTime = 0.5F;

    public GameObject ButtonDown;

    public bool Inbeatable { get; set; }

   

    private Spawner mSpawner;

    private float mInbeatableTime;

    
	// Use this for initialization
	void Start ()
	{
        
	    mSpawner = GameObject.Find("Spawner").GetComponent<Spawner>();
	    mInbeatableTime = InbeatableTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
        IsDead();
        CheckForCameraBoxes();
	}

    public IEnumerator TakeDamage(int pDamage)
    {
        if (!Inbeatable)
        {
            Health -= pDamage;
            ShowPlayerTakenDamage();
            Inbeatable = true;
            yield return new WaitForSeconds(mInbeatableTime);
            Inbeatable = false;
        }
        if (Health == 2)
            {
                Destroy(GameObject.Find("Life1"));
            }
            if (Health == 1)
            {
                Destroy(GameObject.Find("Life2"));
            }
            if (Health == 0)
            {
                Destroy(GameObject.Find("Life3"));
            }
    }

 

    public virtual void IsDead()
    {
        if (Health <= 0 || transform.position.y <= -30)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    IEnumerator ShowPlayerTakenDamage()
    {
        yield return new WaitForSeconds(DamageTakenTime);

    }

    void CheckForCameraBoxes()
    {
        RaycastHit tHitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out tHitInfo, 1) || 
            Physics.Raycast(transform.position, Vector3.right, out tHitInfo, 2) ||
            Physics.Raycast(transform.position, Vector3.up, out tHitInfo, 2) ||
            Physics.Raycast(transform.position, Vector3.left, out tHitInfo, 2))
        {
            if(tHitInfo.collider.tag == "ZoomCamera")
            {
                GetComponentInChildren<ClippingCamera>().ZoomBack = false;
                GetComponentInChildren<ClippingCamera>().Zoom = true;
            }
            if (tHitInfo.collider.tag == "ZoomOutCamera")
           {
                    
                    GetComponentInChildren<ClippingCamera>().Zoom = false;
                    GetComponentInChildren<ClippingCamera>().ZoomBack = true;
           }

        }
    }

   void OnControllerColliderHit(ControllerColliderHit pHit)
    {
        if (pHit.transform.tag == "Knap" && transform.position.y > pHit.transform.position.y && 
            Vector3.Distance(transform.position, pHit.transform.position) < 0.5f && 
            pHit.transform.GetComponent<Knap>().PlayerCanPresButtonDown)
        {
            Instantiate(ButtonDown, new Vector3(pHit.transform.position.x, pHit.transform.position.y + 0.26426f, pHit.transform.position.z), ButtonDown.transform.rotation);
            pHit.transform.GetComponent<Knap>().DisAndAppear();
            mSpawner.SpawnThingsWhenButtonPressed(pHit.collider.GetComponent<SpawnInterface>().Id);
            Destroy(pHit.gameObject);
        }


        if (pHit.transform.tag == "Knap" && transform.position.y > pHit.transform.position.y && 
            Vector3.Distance(transform.position, pHit.transform.position) < 0.5f && 
            pHit.transform.GetComponent<Knap>().PlayerCanPresButtonDown)
        {
            pHit.transform.GetComponent<Knap>().DisAndAppear();
            Instantiate(ButtonDown, new Vector3(pHit.transform.position.x, pHit.transform.position.y + 0.26426f, pHit.transform.position.z), ButtonDown.transform.rotation);
            mSpawner.SpawnThingsWhenButtonPressed(pHit.collider.GetComponent<SpawnInterface>().Id);
            Destroy(pHit.gameObject);
        }


        

       

        if (pHit.transform.tag == "KnapDisappear")
        {
         
        }

        if(pHit.collider.tag == "Lava")
        {
            Application.LoadLevel(Application.loadedLevelName);

        }
    }

  


}
