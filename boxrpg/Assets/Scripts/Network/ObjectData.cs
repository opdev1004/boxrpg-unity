using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
	public Vector3 location;
	public Quaternion rotation;

    // Start is called before the first frame update
    void Start()
	{
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		location = transform.position;
		rotation = transform.rotation;
		string json = JsonUtility.ToJson(this);
		print(json);
    }
}
