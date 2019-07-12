using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//object variables
	public GameObject cameraObj;
	Rigidbody camRigidbody;
	Rigidbody m_Rigidbody;
	BoxCollider m_BoxCollider;

	//turn speed in radians per second
    public float turnSpeed = 0f;
    public float movementSpeed = 8.0f;

	// jumping variables
	public float jumpDuration = 0.5f;
	public float jumpForce = 2.0f;
	private List<KeyCode> jumpKey = new List<KeyCode>{ KeyCode.Space, KeyCode.JoystickButton0 };
    bool isJumping;

    //Quaternion m_Rotation = Quaternion.identity;

	// camera variables
	public float camSpeedX = 1.0f;
	public float camSspeedY = 1.0f;
	// default offset distance is 5.0f
	private float offsetDistance = 5.0f;
	// Angle
	private float r;
	// holding player's position to update camera'
	// private Vector3 previousPlayerPos;


    // Start is called before the first frame update
    void Start()
    {
		camRigidbody = cameraObj.GetComponent<Rigidbody>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_BoxCollider = GetComponent<BoxCollider>();
		r = 0.0f;
    }

	/**
	* Instead of being called before every rendered frame like Update(),
	* FixedUpdate is called before the physics system solves any collisions and other interactions that have happened.
	* By default it is called exactly 50 times every second.
	*/
    void FixedUpdate()
    {
		RotationWithCam();
		Movement();
		Jump();
    }

    //runs every frame
    void Update()
    {
        Vector3 boxCastHalfExtents = new Vector3(m_BoxCollider.size.x / 2.05f, m_BoxCollider.size.y / 2.05f, m_BoxCollider.size.z / 2.05f);

        if (!isJumping)
        {
            foreach (KeyCode key in jumpKey)
            {
                if (Input.GetKeyDown(key))
                {
                    //check if the player is standing on a solid object
                    if (Physics.BoxCast(m_Rigidbody.position, boxCastHalfExtents, Vector3.down, Quaternion.LookRotation(Vector3.down), m_BoxCollider.size.y * 0.05f))
                    {
                        isJumping = true;
                        jumpDuration = 0.5f;
                    }
                }
            }
        } 
    }


	void RotationWithCam(){
		// store mouse movement input value
		float mouseX = camSpeedX * Input.GetAxis("Mouse X");
		// float mouseY = speedW * Input.GetAxis("Mouse Y");

		// calculate angle
		r = r + mouseX;
		if(r > 360){
			r = 0.0f;
		} else if(r < 0) {
			r = 360.0f;
		}

		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			if(offsetDistance > 1.0f){
				offsetDistance = offsetDistance - 0.5f;
			}
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0){
			if(offsetDistance < 7.1f){
				offsetDistance = offsetDistance + 0.5f;
			}
		}

		// Change player and camera's rotation
		Vector3 rotationFactor = new Vector3 (0, r, 0);
		Quaternion playerRotation = Quaternion.Euler(m_Rigidbody.rotation * rotationFactor);
		m_Rigidbody.MoveRotation(playerRotation);

		// calculate camera's new position
		float camPosX = -Mathf.Sin(r * Mathf.Deg2Rad) * offsetDistance;
		float camPosZ = -Mathf.Cos(r * Mathf.Deg2Rad) * offsetDistance;
		float camPosY = camRigidbody.position.y;
		Vector3 newCamPos = new Vector3 (transform.position.x + camPosX, camPosY, transform.position.z + camPosZ);

		// change the position of camera
		cameraObj.transform.position = newCamPos;
		cameraObj.transform.LookAt(transform);
	}


	void Movement(){
		// get input
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        movement.Normalize();
		// get forward and right vector values
		Vector3 objForward = transform.forward;
		Vector3 objRight = transform.right;
		objForward.Normalize();
		objRight.Normalize();
		// move if input is happening.
		m_Rigidbody.MovePosition(m_Rigidbody.position + objForward * movement.z + objRight * movement.x);	
	}

    //Moves the character up while it is jumping.
    void Jump()
    {
        if (isJumping)
        {
            if (jumpDuration > 0)
            {
                Vector3 jumpVector = new Vector3(0, jumpForce, 0);
                m_Rigidbody.MovePosition(m_Rigidbody.position + jumpVector * movementSpeed * Time.deltaTime);
                jumpDuration -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }
}
