using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]


public class BotControlScript : MonoBehaviour
{
	public NodeManager nodeManager;

	public CameraManager cameraManager;

	public bool onTop;
/*	 private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;*/

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = Vector3.zero;
        Vector3 syncVelocity = Vector3.zero;
		bool syncOnTop = false;
        if (stream.isWriting)
        {
			//sync 
			syncOnTop = onTop;
			stream.Serialize(ref syncOnTop);

           /* syncPosition = rigidbody.position;
            stream.Serialize(ref syncPosition);

            syncVelocity = rigidbody.velocity;
            stream.Serialize(ref syncVelocity);*/


			for(int i = 0 ; i < nodeManager.syncList.Count;i++)
			{
				GameObject targetNode = nodeManager.syncList[i];
				networkData data = nodeManager.dataList[i];
				Rigidbody body = targetNode.GetComponent<Rigidbody>();

				//position
				data.syncPosition = targetNode.transform.position;
				stream.Serialize(ref data.syncPosition);



				//velocity

				//check rigibody
				if(body != null){
					data.syncVelocity = body.velocity;
				}else{
					data.syncVelocity = new Vector3(0,0,0);	
				}
				stream.Serialize(ref data.syncVelocity);
			
				//rotate
				data.syncRotation = targetNode.transform.rotation;
				stream.Serialize(ref data.syncRotation);
			}


        }
        else
        {
			//sync 
			stream.Serialize(ref syncOnTop);
			onTop = syncOnTop;

			for(int i = 0 ; i < nodeManager.syncList.Count;i++)
			{
				GameObject targetNode = nodeManager.syncList[i];
				networkData data = nodeManager.dataList[i];
				
				stream.Serialize(ref data.syncPosition);
				stream.Serialize(ref data.syncVelocity);
				stream.Serialize(ref data.syncRotation);

				//sync
				data.syncTime =0.0f;
				data.syncDelay = Time.time - data.lastSynchronizationTime;
				data.lastSynchronizationTime = Time.time;

				//position
				data.syncEndPosition = data.syncPosition + data.syncVelocity * data.syncDelay;
				data.syncStartPosition = targetNode.transform.position;

				//rotation
				data.syncStartRotation = targetNode.transform.rotation;
				data.syncEndRotation = data.syncRotation;
			}

			/*
            stream.Serialize(ref syncPosition);
            stream.Serialize(ref syncVelocity);


            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;

            syncEndPosition = syncPosition + syncVelocity * syncDelay;
            syncStartPosition = rigidbody.position;*/
        }
    }

    void Awake()
    {
		for (int i = 0; i < nodeManager.syncList.Count; i++) {
						GameObject targetNode = nodeManager.syncList [i];
						networkData data = nodeManager.dataList [i];

						data.lastSynchronizationTime = Time.time;
				}
    }

    void Update()
    {
        if (networkView.isMine)
        {
            InputMovement();
        }
        else
        {
           SyncedMovement();
        }
    }


    private void InputMovement()
    {
        
    }

    private void SyncedMovement()
    {

		for(int i = 0 ; i < nodeManager.syncList.Count;i++)
		{
			GameObject targetNode = nodeManager.syncList[i];
			networkData data = nodeManager.dataList[i];

			data.syncTime += Time.deltaTime;

			//position
			targetNode.transform.position = Vector3.Lerp(data.syncStartPosition, data.syncEndPosition, data.syncTime / data.syncDelay);

			//rotate
			targetNode.transform.rotation = Quaternion.Lerp(data.syncStartRotation, data.syncEndRotation, data.syncTime / data.syncDelay);

		}
    }



	
	
	
	
	int moveSpeed = 5;
	
	[DllImport ("UniWii")]
	private static extern void wiimote_start();

	[DllImport ("UniWii")]
	private static extern void wiimote_stop();

	[DllImport ("UniWii")]
	private static extern int wiimote_count();

	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccY(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccZ(int which);

	[DllImport ("UniWii")]
	private static extern float wiimote_getIrX(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrY(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getRoll(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getPitch(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getYaw(int which);

	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonA(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonB(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonUp(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonLeft(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonRight(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonDown(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButton1(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButton2(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonPlus(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonMinus(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonHome(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckStickX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckStickY(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getNunchuckAccZ(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckC(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckZ(int which);
	
	
	public float animSpeed = 1.5f;				// a public setting for overall animator animation speed
	public float lookSmoother = 3f;				// a smoothing setting for camera motion
	
	private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState;	// a reference to the current state of the animator, used for layer 2
	private CapsuleCollider col;					// a reference to the capsule collider of the character
	

	static int idleState = Animator.StringToHash("Base Layer.Idle");	
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");			// these integers are references to our animator's states
	static int jumpState = Animator.StringToHash("Base Layer.Jump");				// and are used to check state for various actions to occur
	static int jumpDownState = Animator.StringToHash("Base Layer.JumpDown");		// within our FixedUpdate() function below
	static int fallState = Animator.StringToHash("Base Layer.Fall");
	static int rollState = Animator.StringToHash("Base Layer.Roll");
	static int waveState = Animator.StringToHash("Layer2.Wave");
	
	bool jumping = false;
	
	public GameObject offset;
	
	 
	
	AvatarController KinectController;
	Animator AnimationController;
	
	
	public bool wiimoteGetButtonA()
	{
		if(wiimote_count()>0)
		{
		return wiimote_getButtonA(0);
		}
		return false;
	}
	
	public bool wiimoteGetButtonB()
	{
		if(wiimote_count()>0)
		{
		return wiimote_getButtonB(0);
		}
		return false;
	}
	
		public bool wiimoteGetButtonLeft()
	{
		if(wiimote_count()>0)
		{
		return wiimote_getButtonLeft(0);
		}
		return false;
	}
	
		public bool wiimoteGetButtonRight()
	{
		if(wiimote_count()>0)
		{
		return wiimote_getButtonRight(0);
		}
		return false;
	}

	void Start ()
	{
		//init 
		onTop = false;

		// initialising reference variables
		anim = gameObject.GetComponent<Animator>();					  
		col = gameObject.GetComponent<CapsuleCollider>();				
		if(anim.layerCount ==2)
			anim.SetLayerWeight(1, 1);

		KinectController = GetComponent<AvatarController>();
		AnimationController = GetComponent<Animator>();
		
		rigidbody.isKinematic = false;
		//rigidbody.useGravity = true;

		//enable camera dummy follow script
		cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
		
		//enable script
		//cameraManager = cameraDummyObject.GetComponent<cameraDummyFollowPlayer>();

			if(cameraManager.Player1.name.Contains("Dummy"))
			{
				cameraManager.Player1 = gameObject;
				cameraManager.Player2 = gameObject;

			}
			else
			{
				cameraManager.Player2 = gameObject;
			}

		if(this.networkView.isMine)
		{
			CameraManager.CurrentPlayer1 = gameObject;
			CameraManager.CurrentPlayer1Controller = this;
		}

		//wii 
		wiimote_start();

	}
	
	bool grounded = false;
	
	void OnCollisionEnter (Collision col)
    {
		if(col.collider.name == "Terrain" || col.collider.name =="DoorMovable_Short_01")
		{
			
			grounded = true;
			anim.SetBool("Grounded", grounded);
			
			jumping = false;
			Debug.Log("Grounded");
		}
	}
	
	void FixedUpdate ()
	{
		if(networkView.isMine)
		{
		
			float h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		
			bool moving = false;
		
		
			if(!jumping)
			{
			
				if(h>0.3f||wiimoteGetButtonRight())
				{
					offset.transform.rotation = Quaternion.Euler(0,90,0);
			
					if(h>0.3f)
					{
						anim.SetFloat("Speed", (h-0.3f)*0.7f);
						rigidbody.velocity = new Vector3(0.7f*moveSpeed,rigidbody.velocity.y,0);
						moving = true;
					}
					else if(wiimoteGetButtonRight())
					{
						anim.SetFloat("Speed", 0.7f);
						rigidbody.velocity = new Vector3(0.7f*moveSpeed,rigidbody.velocity.y,0);
						moving = true;
					}
				}
				else if(h < -0.3f||wiimoteGetButtonLeft())
				{
					offset.transform.rotation = Quaternion.Euler(0,-90,0);
					if(h < -0.3f)
					{
						anim.SetFloat("Speed", (-h-0.3f)*0.7f);	
						rigidbody.velocity = new Vector3(-0.7f*moveSpeed,rigidbody.velocity.y,0);
						moving =true;
					}
					else if(wiimoteGetButtonLeft())
					{
						anim.SetFloat("Speed", 0.7f);
						rigidbody.velocity = new Vector3(-0.7f*moveSpeed,rigidbody.velocity.y,0);
						moving = true;
					}
				}
				else
				{
					if(offset!=null)
						offset.transform.rotation = Quaternion.Euler(0,180,0);
					anim.SetFloat("Speed", 0);	
				}
		
				if(Input.GetButton("ButtonA")||wiimoteGetButtonB()||jumping)
				{
					anim.SetBool("Jump", true);
					jumping = true;
					Debug.Log("Jumping");
					rigidbody.velocity = new Vector3(rigidbody.velocity.x,5,0);
				}
			
//			Debug.Log("move:"+moving+"jump:"+jumping+"Grounded:"+anim.GetBool("Grounded"));
			
				if(!moving&&!jumping&&grounded && !anim.IsInTransition(0))
				{
					AnimationController.enabled = false;
					KinectController.enabled = true;
						//Debug.Log("Animation!!!");
					rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
				}
				else
				{
					AnimationController.enabled = true;
					KinectController.enabled = false;
				}
			}
			else{
				//detect changing direction when the user is jumping! 
				if( (h < -0.3f || wiimoteGetButtonLeft()) && rigidbody.velocity.x > 0){
					offset.transform.rotation = Quaternion.Euler(0,-90,0);
					if(h < -0.3f)
					{
						rigidbody.velocity = new Vector3(-(rigidbody.velocity.x), rigidbody.velocity.y, 0);
					}
					else if(wiimoteGetButtonLeft())
					{
						rigidbody.velocity = new Vector3(-(rigidbody.velocity.x),rigidbody.velocity.y,0);
					}
				}
				else if((h > 0.3f||wiimoteGetButtonRight()) && rigidbody.velocity.x < 0)
				{
					offset.transform.rotation = Quaternion.Euler(0,90,0);
					
					if(h > 0.3f)
					{
						rigidbody.velocity = new Vector3(-(rigidbody.velocity.x), rigidbody.velocity.y, 0);
					}
					else if(wiimoteGetButtonRight())
					{
						rigidbody.velocity = new Vector3(-(rigidbody.velocity.x), rigidbody.velocity.y, 0);
					}
				}
			}
		
		//Debug.Log("Gounded"+anim.GetBool("Grounded"));

								// set our animator's float parameter 'Speed' equal to the vertical input axis				
			anim.SetFloat("Direction", 0); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
		
		
			anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
		
		
		
			currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation
		
			if(anim.layerCount ==2)		
				layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);	// set our layer2CurrentState variable to the current state of the second Layer (1) of animation
		
				
		// STANDARD JUMPING
		
		// if we are currently in a state called Locomotion, then allow Jump input (Space) to set the Jump bool parameter in the Animator to true
			if (currentBaseState.nameHash == locoState)
			{	
				if(Input.GetButtonDown("Jump"))
				{
					anim.SetBool("Jump", true);
					//anim.SetBool("Grounded", false);
					grounded = false;
					anim.SetBool("Grounded", grounded);
				}
				if(!anim.IsInTransition(0))
				{
				}
			}
		
		// if we are in the jumping state... 
			else if(currentBaseState.nameHash == jumpState)
			{
			//  ..and not still in transition..
				if(!anim.IsInTransition(0))
				{				
					// reset the Jump bool so we can jump again, and so that the state does not loop 
					anim.SetBool("Jump", false);
					grounded = false;
					anim.SetBool("Grounded", grounded);
				}
			//anim.SetBool("Grounded", false);
				
			
			
			/*
			// Raycast down from the center of the character.. 
			Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
			RaycastHit hitInfo = new RaycastHit();
			
			if (Physics.Raycast(ray, out hitInfo))
			{
				// ..if distance to the ground is more than 1.75, use Match Target
				if (hitInfo.distance > 1.75f)
				{
					
					// MatchTarget allows us to take over animation and smoothly transition our character towards a location - the hit point from the ray.
					// Here we're telling the Root of the character to only be influenced on the Y axis (MatchTargetWeightMask) and only occur between 0.35 and 0.5
					// of the timeline of our animation clip
					anim.MatchTarget(hitInfo.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), 0.35f, 0.5f);
				}
			}
			*/
			}
		
		
		// JUMP DOWN AND ROLL 
		
		// if we are jumping down, set our Collider's Y position to the float curve from the animation clip - 
		// this is a slight lowering so that the collider hits the floor as the character extends his legs
			else if (currentBaseState.nameHash == jumpDownState)
			{
				col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);
			}
		
		// if we are falling, set our Grounded boolean to true when our character's root 
		// position is less that 0.6, this allows us to transition from fall into roll and run
		// we then set the Collider's Height equal to the float curve from the animation clip
			else if (currentBaseState.nameHash == fallState)
			{
				col.height = anim.GetFloat("ColliderHeight");
			}
		
		// if we are in the roll state and not in transition, set Collider Height to the float curve from the animation clip 
		// this ensures we are in a short spherical capsule height during the roll, so we can smash through the lower
		// boxes, and then extends the collider as we come out of the roll
		// we also moderate the Y position of the collider using another of these curves on line 128
			else if (currentBaseState.nameHash == rollState)
			{
				if(!anim.IsInTransition(0))
				{
					col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);
				
				}
			}
		// IDLE
		
		// check if we are at idle, if so, let us Wave!
			else if (currentBaseState.nameHash == idleState)
			{
				if(Input.GetButtonUp("Jump"))
				{
					anim.SetBool("Wave", true);
				}
			}
		// if we enter the waving state, reset the bool to let us wave again in future
			if(layer2CurrentState.nameHash == waveState)
			{
				anim.SetBool("Wave", false);
			}
		}
		else
		{
			KinectController.enabled = false;
			AnimationController.enabled = false;
		}
	}
}
