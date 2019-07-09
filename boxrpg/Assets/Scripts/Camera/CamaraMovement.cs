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
	private float r = 270.0f;
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
		float mouseX = speedX * Input.GetAxis("Mouse X");
		// calculate angle
		r = r + mouseX;
		if(r > 360){
			r = 0.0f;
		} else if(r < 0) {
			r = 360.0f;
		}

		//float mouseY = speedW * Input.GetAxis("Mouse Y");
		float posX = Mathf.Cos(r * Mathf.Deg2Rad) * offsetDistance;
		float posZ = Mathf.Sin(r * Mathf.Deg2Rad) * offsetDistance;
		float posY = transform.position.y;
		/*
		// logging
		print("Mouse Pos: " + mouseX);
		print("R: " + r);
		print("PosX: " + posX);
		print("PosZ: " + posZ);
		*/
		Vector3 newCamPos = new Vector3 (posX, posY, posZ);
		transform.position = newCamPos;
		transform.LookAt(player.transform);
	}

}
