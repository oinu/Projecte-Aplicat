using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger_Script : MonoBehaviour {

	public List<GameObject> teleports;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Player")
		{
			foreach (GameObject t in teleports)
			{
				if(!t.activeSelf)t.SetActive(true);
			}
		}
	}
}
