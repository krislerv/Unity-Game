using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public bool followX;
	public bool followY;
	public bool followZ;
	
	void Update () {
		transform.position = new Vector3 (followX ? target.position.x : transform.position.x, 
		                                  followY ? target.position.y : transform.position.y, 
		                                  followZ ? target.position.z : transform.position.z);
	}
}
