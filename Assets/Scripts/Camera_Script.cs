using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour {

	public GameObject character;
	private float angleVelocity;
	private GameObject obj,rayObject,collisionObject;
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

		rayObject= new GameObject("RayCastObject");
		rayObject.transform.parent=obj.transform;
		rayObject.transform.position= new Vector3(character.transform.position.x,
		character.transform.position.y+character.transform.localScale.y,character.transform.position.z);


	}
	
	// Update is called once per frame
	void Update () {
		angleVelocity= Input.GetAxis("Mouse X")*10;
		if(angleVelocity!=0)
		{
			obj.transform.Rotate(new Vector3(0,angleVelocity,0),Space.Self);
		}	

		obj.transform.position=character.transform.position;

		rayObject.transform.position= new Vector3(character.transform.position.x,
		character.transform.position.y+character.transform.localScale.y+0.5f,character.transform.position.z);
		rayObject.transform.forward= obj.transform.forward;

		RaycastHit hit;
		if(Physics.Raycast(rayObject.transform.position,rayObject.transform.forward,out hit))
			{
				if(hit.transform.tag=="Transportar")
				{
					if(collisionObject==null)
					{
						collisionObject= hit.transform.GetChild(0).gameObject;
						collisionObject.SetActive(true);
					}
					else if(!GameObject.Equals(collisionObject,hit.transform.GetChild(0).gameObject))
					{
						collisionObject.SetActive(false);
						collisionObject= hit.transform.GetChild(0).gameObject;
						collisionObject.SetActive(true);
					}
					if(Input.GetMouseButtonDown(0))
					{
						character.GetComponent<Character_Script>().SetTeleportGoal(hit.transform.position);
						Destroy(hit.transform.gameObject);
					}
				}
				else
				{
					if(collisionObject!=null)
					{
						collisionObject.SetActive(false);
						collisionObject=null;
					}
				}
			}

		

	}
}
