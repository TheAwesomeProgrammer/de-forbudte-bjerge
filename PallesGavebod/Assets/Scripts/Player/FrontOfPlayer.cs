using UnityEngine;
using System.Collections;

public class FrontOfPlayer : MonoBehaviour
{
    private Enemy mEnemy;

    private int mPickupItemsCount;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

    void OnTriggerEnter(Collider pCollider)
    {
       if(mEnemy == null)
       {
           PickupItem tPickupItem = pCollider.GetComponent<PickupItem>();
            
          
           if (tPickupItem != null && mPickupItemsCount <= 0)
           {
               mPickupItemsCount++;
               tPickupItem.CanbePickedUp = true;
           }
       }
      
        
    }



     void OnTriggerExit(Collider pCollider)
     {
         if (mEnemy == null)
         {
             PickupItem tPickupItem = pCollider.GetComponent<PickupItem>();

             if (tPickupItem != null)
             {
                 mPickupItemsCount--;
                 tPickupItem.CanbePickedUp = false;
             }
         }
     }
}
