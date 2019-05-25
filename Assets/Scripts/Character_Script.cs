﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Script : MonoBehaviour {

	public GameObject currenCamera;
	public float velocity,jumpForce;
	private Vector3 position,direction;
	private bool inCollision,inAir,swipeForward;
	private GameObject collisionObject,collisionInstance;
	private Transform objTrans;
	// Use this for initialization
	void Start () {
		position= this.transform.position;
		collisionInstance= new GameObject();
		collisionInstance.transform.position = new Vector3(0f,10000f,0f);
		swipeForward=false;
		objTrans= currenCamera.GetComponent<Camera_Script>().GetObj.transform;
	}
	
	// Update is called once per frame
	void Update () {
		#region Movement
		position=this.transform.position;

		if(Input.GetAxis("Vertical")>0)
		{
			if(inCollision && this.transform.position.z>collisionObject.transform.position.z)inCollision=false;
			position+=objTrans.forward.normalized*velocity;
		}

		if(!inCollision && !Vector3.Equals(this.transform.position,position))
		{
			direction=position-this.transform.position;
			this.transform.forward=direction.normalized;
			this.transform.position+=direction;
		}
		#endregion

		if(!inAir && Input.GetAxis("Jump")!=0)
		{
			this.GetComponent<Rigidbody>().AddForce(0,jumpForce,0,ForceMode.Impulse);
			inAir=true;
		}
		else if (inAir)
		{
			float collPoint=collisionObject.transform.position.y+ collisionObject.transform.localScale.y/2;
			float thisPoint= transform.position.y-transform.localScale.y/2;
			if(inCollision)inCollision=!(collPoint<thisPoint);
		}
	}

	private void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag!="Terrain")
		{
			inCollision=true;
			collisionObject=other.gameObject;
		}
		else
		{
			inAir=false;
			collisionObject=collisionInstance;
		}
	}
	private void OnCollisionExit(Collision other) {
		inCollision=false;

	}
}
