using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour {

	public GameObject character;
	public float angleVelocity;
	private GameObject obj;
	private Vector3 position;
	private float turnPosition;


	public GameObject GetObj
	{
		get
		{
			return obj;
		}
	}

	// Use this for initialization
	void Start () {
		obj=new GameObject("CameraReference");

		obj.transform.position=character.transform.position;
		obj.transform.forward=character.transform.forward;

		position=obj.transform.position;
		position-=obj.transform.forward.normalized*5;
		position.y+=2;
		this.transform.position=position;
		this.transform.forward=character.transform.position-position;
		this.transform.parent=obj.transform;
		turnPosition=0;

	}
	
	// Update is called once per frame
	void Update () {
		angleVelocity= Input.GetAxis("Mouse X")*10;
		if(angleVelocity!=0)
		{
			obj.transform.Rotate(new Vector3(0,angleVelocity,0),Space.Self);
		}	

		obj.transform.position=character.transform.position;

	}
}
