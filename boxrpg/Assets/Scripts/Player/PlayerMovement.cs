using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//object variables
	public GameObject cameraObj;
	//Rigidbody camRigidbody;
	Rigidbody m_Rigidbody;
	BoxCollider m_BoxCollider;

	//turn speed in radians per second
    public float movementSpeed = 1.0f;

	// jumping variables
	public float jumpDuration = 0.2f;
	public float jumpForce = 8.0f;
	private List<KeyCode> jumpKey = new List<KeyCode>{ KeyCode.Space, KeyCode.JoystickButton0 };
    bool isJumping;

	// camera variables
	public float camSpeedX = 1.0f;
	public float camSpeedY = 1.0f;
	// default offset distance is 5.0f
	private float offsetDistance = 5.0f;
	// Angle of 2D, X and Z
	private float rx;
	// Angle of 3D, 
	private float ry;


    // Start is called before the first frame update
    void Start()
    {
		//camRigidbody = cameraObj.GetComponent<Rigidbody>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_BoxCollider = GetComponent<BoxCollider>();
		rx = 0.0f;
		ry = 75.0f;
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
		float mouseY = camSpeedY * Input.GetAxis("Mouse Y");

		// calculate 2D rotational angle
		rx = rx + mouseX;
		ry = ry + mouseY;
		if(rx > 360){
			rx = 0.0f;
		} else if(rx < 0) {
			rx = 360.0f;
		}
		// Limit vertical angle, 0 is where the top and bigger number get closer to ground
		if(ry > 85){
			ry = 85.0f;
		} else if(ry < 5) {
			ry = 5.0f;
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
		Vector3 rotationFactor = new Vector3 (0, rx, 0);
		Quaternion playerRotation = Quaternion.Euler(m_Rigidbody.rotation * rotationFactor);
		m_Rigidbody.MoveRotation(playerRotation);

		// calculate camera's new position
		float camPosX = offsetDistance * Mathf.Sin(ry * Mathf.Deg2Rad) * -Mathf.Sin(rx * Mathf.Deg2Rad);
		float camPosZ = offsetDistance * Mathf.Sin(ry * Mathf.Deg2Rad) * -Mathf.Cos(rx * Mathf.Deg2Rad);
		float camPosY = offsetDistance * Mathf.Cos(ry * Mathf.Deg2Rad);
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
		// calculate total movement vector
		Vector3 objMovement = (objForward * movement.z + objRight * movement.x) * 0.2f * movementSpeed;
		// move if input is happening.
		m_Rigidbody.MovePosition(m_Rigidbody.position + objMovement);
		cameraObj.transform.position = cameraObj.transform.position + objMovement;
	}

    //Moves the character up while it is jumping.
    void Jump()
    {
        if (isJumping)
        {
            if (jumpDuration > 0)
            {
                Vector3 jumpVector = new Vector3(0, jumpForce, 0);
                m_Rigidbody.MovePosition(m_Rigidbody.position + jumpVector * Time.deltaTime);
                jumpDuration -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }
}
