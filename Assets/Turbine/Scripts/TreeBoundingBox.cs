using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TreeBoundingBox : MonoBehaviour {

	public Vector3 max = Vector3.zero;
	public Vector3 min = Vector3.zero;

	Renderer[] rends;
	// Use this for initialization
	void Start () {
		
	}

	public void Init(){
		rends = GetComponentsInChildren<Renderer> ();
	}

	public void checkBounds(){
		for (int i = 0; i < rends.Length; i++) {
			expand (rends [i].bounds.center + rends [i].bounds.extents);
			contract (rends [i].bounds.center - rends [i].bounds.extents);
		}
		Debug.Log (min + "," + max);
	}

	void expand(Vector3 vec){
		max = new Vector3(
			vec.x > max.x ? vec.x : max.x,
			vec.y > max.y ? vec.y : max.y,
			vec.z > max.z ? vec.z : max.z);
	}
	void contract(Vector3 vec){
		min = new Vector3(
			vec.x < min.x ? vec.x : min.x,
			vec.y < min.y ? vec.y : min.y,
			vec.z < min.z ? vec.z : min.z);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
