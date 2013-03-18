using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int AttackDamage = 1;
    public float attackRange = 5;
    public float LookRange = 10.0F;
    public float HiddenRange = 2.5F;
    public float MAXLIFE = 100;
    public float attackInterval = 1.5f;

    public float AccSpeed = 5;
    public float MaxSpeed = 10.0F;

    public bool HasBeenHidding { get; set; }
    public bool StopFollowingPlayer { get; set; }

    public GameObject Blood;

    protected float distanceFromTarget;
    protected float life;

    protected bool IsHiding = false;
    protected bool mGrounded = false;

    protected int mDirection;

    protected Player player;
    
	// Use this for initialization
	public void  Start ()
    {
	    life = MAXLIFE;	    
       
	}
	
	// Update is called once per frame
	 public void  Update ()
	{
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player != null)
        {
            distanceFromTarget = Vector3.Distance(player.transform.position, transform.position);
        }
     
            MoveToPlayerOrHide();
     
	     IsDead();
	    Attack();
	}


   public virtual void takeDamage(int pDamage)
   {
       life -= pDamage;
       //Instantiate(Blood, transform.position, transform.rotation);

   }

   protected virtual void MoveToPlayerOrHide()
   {
       Vector3 tPlayerPos = player.transform.position;
       transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
      RotateTowardsPlayer();

    

      if (distanceFromTarget > HiddenRange && !IsHiding && !HasBeenHidding && mGrounded )
      {
          Debug.Log("HidingAndOutsideRange");
          transform.position = new Vector3(transform.position.x,tPlayerPos.y-1,transform.position.z);
          rigidbody.isKinematic = true;
          IsHiding = true;
          HasBeenHidding = true;
      }

      else if (distanceFromTarget < LookRange && !IsHiding && !StopFollowingPlayer && Physics.Raycast(transform.position + transform.forward * 0.4f, Vector3.down, 2))
      {
   print("HELO");

          if (rigidbody.velocity.magnitude < MaxSpeed)
          {
              
              rigidbody.AddRelativeForce(Vector3.forward * AccSpeed * Time.deltaTime);
          }

      }
       else if (distanceFromTarget < HiddenRange && IsHiding)
      {
          Debug.Log("HidingAndInsideRange");
          transform.position = new Vector3(transform.position.x, tPlayerPos.y, transform.position.z);
          rigidbody.isKinematic = false;
          IsHiding = false;
      }

   }

    void RotateTowardsPlayer()
    {
        Vector3 tPlayerPos = player.transform.position;
        tPlayerPos.y = transform.position.y;
        transform.LookAt(tPlayerPos);
    }


   public virtual void  IsDead()
   {
       if (life <= 0)
       {
           Destroy(gameObject);
       }
   }

    

   public virtual void Attack()
    {
        if (distanceFromTarget < attackRange && player != null )
        {
                StartCoroutine(player.TakeDamage(AttackDamage));
        }
    }

   public virtual void OnCollisionEnter(Collision collisionObject)
   {
       Collider pCollider = collisionObject.collider;
       Debug.Log("Tag "+pCollider.tag);
       if ((pCollider.tag == "Player" || pCollider.tag == "PlayerPart" || pCollider.tag == "Explosion") && (pCollider.transform.position.y > transform.position.y))
       {
           takeDamage(1);
       }
     

    
   }

    public virtual void OnCollisionStay(Collision collisionObject)
   {
       Collider pCollider = collisionObject.collider;

       if(pCollider.tag == "Floor")
       {
           mGrounded = true;
       }
       if (pCollider.tag == "PickupItem" && pCollider.GetComponent<PickupItem>().Throwing)
       {
          takeDamage(1);
       }
       if (pCollider.tag == "Lava")
       {
           takeDamage(1);
       }

   }

    void OnTriggerEnter(Collider pCollider)
    {
        if(pCollider.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }
   public virtual void OnCollisionExit(Collision collisionObject)
   {
       Collider pCollider = collisionObject.collider;

       if (pCollider.tag == "Floor")
       {
           mGrounded = false;
       }
   }

   void OnDrawGizmos()
   {
       Gizmos.DrawWireSphere(transform.position, attackRange);
       Gizmos.DrawWireSphere(transform.position,LookRange);
       Gizmos.DrawWireSphere(transform.position, HiddenRange);
   }
   
}
