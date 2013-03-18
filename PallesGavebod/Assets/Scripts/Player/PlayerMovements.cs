using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

    public float Speed = 6.0F;
    public float SprintSpeed;
    //    public float SprintTime;
    public float JumpSpeed = 8.0F;
    public float Gravity = 20.0F;
    public float RotationSpeed = 10.0F;

    public bool Freeze { get; set; }

    public CharacterController controller { get; set; }

    private float mSpeed;
    private float mPushForce;
   

    private PlaySounds mAudioManager;

    private Vector3 moveDirection = Vector3.zero;



	// Use this for initialization
	void Start ()
	{
	   
        mSpeed = Speed;
	    mAudioManager = GameObject.Find("AudioManager").GetComponent<PlaySounds>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
   
    if(!Freeze)
    {
  
          Move();
      
        
    }


	}

    public Vector3 ReturnMoveDirection()
    {
        return moveDirection;
    }

    public void AddForce(float pForce)
    {
        mPushForce = pForce;
    }

    void Move()
    {

        controller = GetComponent<CharacterController>();
        float tVerticalMovement = Input.GetAxis("Vertical");

    

        if (!controller.isGrounded)
        {
            moveDirection = new Vector3(0, moveDirection.y, -tVerticalMovement * mSpeed * Time.deltaTime);
            moveDirection = transform.TransformDirection(moveDirection);
        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, -tVerticalMovement * mSpeed * Time.deltaTime);
            moveDirection = transform.TransformDirection(moveDirection);


            if (Input.GetButton("Jump"))
            {
                moveDirection.y = JumpSpeed;
                
                mAudioManager.PlaySound("Jump");
            }
         
            

        }
        if (mPushForce > 0)
        {
            moveDirection.y  = mPushForce;
            mPushForce = 0;
        }

       

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y + RotationSpeed * Time.deltaTime,transform.eulerAngles.z), Time.time);
         
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - RotationSpeed * Time.deltaTime, transform.eulerAngles.z), Time.time);
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            mSpeed = SprintSpeed;
        }
        else
        {
            mSpeed = Speed;
        }

        moveDirection.y -= Gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
