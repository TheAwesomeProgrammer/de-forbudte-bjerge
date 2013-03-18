using UnityEngine;
using System.Collections;


public class PickupItem : MonoBehaviour,PickupInterface
{
    public float Speed = 500;
    public float RotationSpeed = 0;
    public float ThrowSpeed = 20000;

    public bool Throwing { get; set; }
    public bool HasBeenThrow { get; set; }
    public bool CanbePickedUp { get; set; }

    private GameObject mPlayer;

    private Quaternion mLookRotation;
    private Vector3 mDirection;

    private bool mMoveToHead;
    private bool IsOverHead;
    private bool MoveToGround;



	// Use this for initialization
	void Start ()
	{
	}

    // Update is called once per frame
	void Update () {

	    mPlayer = GameObject.FindGameObjectWithTag("Player");



     
	    if (Input.GetKeyDown(KeyCode.E) && !IsOthersOnPlayersHead() && CanbePickedUp && mPlayer.GetComponent<PlayerMovements>().controller.isGrounded)
        {
            mPlayer.GetComponent<PlayerMovements>().Freeze = true;
            rigidbody.useGravity = false;
            mMoveToHead = true;
            HasBeenThrow = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && IsOverHead)
        {
            collider.isTrigger = false;
            Throw();
        }
        if (Input.GetKeyDown(KeyCode.Q) && IsOverHead && mPlayer.GetComponent<PlayerMovements>().controller.isGrounded)
        {
            collider.isTrigger = false;
            mMoveToHead = false;
            MoveToGround = true;
            mPlayer.GetComponent<PlayerMovements>().Freeze = true;
            transform.parent = null;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            IsOverHead = false;
        }

        if(MoveToGround)
        {
            LetGo();

        }

        if (mMoveToHead)
        {
            MoveToOverPlayerHead();

        }

	}

    public bool IsOthersOnPlayersHead()
    {
        foreach (GameObject tPickupItem in GameObject.FindGameObjectsWithTag("PickupItem"))
        {
            if(tPickupItem != gameObject && tPickupItem.GetComponent<PickupItem>() != null && 
                (tPickupItem.GetComponent<PickupItem>().mMoveToHead || tPickupItem.GetComponent<PickupItem>().IsOverHead))
            {
                return true;
            }
        }
        return false;
    }


    public void MoveToOverPlayerHead()
    {
          RaycastHit tHitInfo;
          Vector3 tLookAtPos = (mPlayer.transform.position + new Vector3(0, 0.95f, 0) - transform.position);
          rigidbody.velocity = new Vector3(0,0,0);
        
            mDirection = (tLookAtPos).normalized;
            mLookRotation = Quaternion.LookRotation(mDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, mLookRotation, Time.deltaTime * RotationSpeed);
            rigidbody.AddRelativeForce(Vector3.forward * Speed * Time.deltaTime);

       
       if ((Physics.Raycast(transform.position, Vector3.down, out tHitInfo,10)  && (tHitInfo.transform.tag == "Player" ||tHitInfo.transform.tag == "PlayerPart")  || 
            ((mPlayer.transform.position.y - transform.position.y) > -0.9f) && (mPlayer.transform.position.y - transform.position.y) < -1f))
        {
            mMoveToHead = false;
            mPlayer.GetComponent<PlayerMovements>().Freeze = false;
            transform.rotation = mPlayer.transform.rotation;
            transform.position = mPlayer.transform.position + new Vector3(0, 0.95f, 0);
            rigidbody.isKinematic = true;
            collider.isTrigger = true;
            transform.parent = mPlayer.transform;
            IsOverHead = true;
            if(GetComponent<TNT>() != null)
            {
                StartCoroutine(GetComponent<TNT>().CountDownToExplosion()); 
            }
      
        }
       
    }

    public void Throw()
    {
     GameObject.Find("AudioManager").GetComponent<PlaySounds>().PlaySound("Throw");
     transform.parent = null;
     rigidbody.isKinematic = false;
 
     rigidbody.useGravity = true;
     IsOverHead = false;
     mMoveToHead = false;
     Throwing = true;
     rigidbody.AddRelativeForce(-Vector3.forward * ThrowSpeed * Time.deltaTime);
    }
    
    public void LetGo()
    {
        rigidbody.AddRelativeForce(-Vector3.forward * 1000 * Time.deltaTime);
    }

  

    void OnCollisionEnter(Collision pCollision)
    {
      
        if(mPlayer != null && pCollision.collider.tag != "Player" && MoveToGround && !mMoveToHead)
        {
           
        }
        if(pCollision.collider.tag != "Enemy" && pCollision.collider.tag != "Player")
        {
            Throwing = false;
            MoveToGround = false;
        }
        if (pCollision.collider.tag == "Floor" && !mMoveToHead)
        {
            mPlayer.GetComponent<PlayerMovements>().Freeze = false;
        }
        if (pCollision.collider.tag == "Knap")
        {
            Knap tKnap = pCollision.collider.GetComponent<Knap>();
            if(tKnap.PickupItemCanPresButtonDown)
            {
                tKnap.Hit = true;
            }
            

        }
     
    }
}
