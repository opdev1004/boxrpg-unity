using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovement : MonoBehaviour
{
	public GameObject player;
	public float speedX = 1.0f;
	//private float speedY = 1.0f;
	private float offsetDistance = 5.0f;
	// Angle
	private float r = 0.0f;
	private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

	void LateUpdate()
	{
		// mouse movement value
		float mouseX = speedX * Input.GetAxis("Mouse X") * 0.1f;
		// calculate angle
		r = r + mouseX;
		if(r > 360){
			r = 0.0f;
		} else if(r < 0) {
			r = 360.0f;
		}

			//float mouseY = speedW * Input.GetAxis("Mouse Y");

		float posX = Mathf.Sin(r) * offsetDistance;
		float posZ = Mathf.Cos(r) * offsetDistance;
		float posY = transform.position.y;

		Vector3 newCamPos = new Vector3 (posX, posY, posZ);
		transform.position = newCamPos;
		transform.LookAt(player.transform);


		/*
		// Add position value
		transform.position = new Vector3(transform.position.x + posX, posY, transform.position.z + posZ);

		// if x and z position is out of range, fix them
		
		if(transform.position.x > offsetDistance){
			transform.position = new Vector3(offsetDistance, transform.position.y, transform.position.z);
		} else if(transform.position.x < -offsetDistance){
			transform.position = new Vector3(-offsetDistance, transform.position.y, transform.position.z);
		}
		if(transform.position.z > offsetDistance){
			transform.position = new Vector3(transform.position.x, transform.position.y, offsetDistance);
		} else if(transform.position.z < -offsetDistance){
			transform.position = new Vector3(transform.position.x, transform.position.y, -offsetDistance);
		}
		

		*/

		//transform.rotate(aaa);
	}

}
