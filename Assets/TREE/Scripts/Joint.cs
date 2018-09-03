using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TREESharp{
public class Joint : MonoBehaviour {

	public List<GameObject> limbs;
	public Trait trait{ get; set; }
	public GameObject childJoint;
	public int joint = 0;
	public int joints = 0;
	public int offset = 0;
	public int offset2 = 0;
	public int[] dictionaryName;

	public GameObject scalar;
	public GameObject rotator;
	public GameObject tip;

	void Awake(){
		limbs = new List<GameObject> ();

		trait = new Trait ();// gameObject.AddComponent<Trait> ();
	}

	public void Construct(){
		trait.makeDefault ();
		_construct ();
	}

	public void Construct(Trait p){
		trait.Apply (p);
		_construct ();
	}

	public void setTrait(Trait t){
		trait = t;
	}

	public void setScale(float height){
		trait.jointScale = height;
		scalar.transform.localScale = new Vector3 (1, trait.jointScale, 1);
	}
//
//	public void setDictionaryName(params int[] name){
//
//	}

	private void _construct(){
		
		GameObject ballMesh =  TREEUtils.makePart(trait.ballMesh, trait.material);

		GameObject jointMesh = TREEUtils.makePart(trait.jointMesh, trait.material);
		jointMesh.transform.localPosition = new Vector3(0,.5f,0);	
		jointMesh.transform.localScale = new Vector3(1,1,1);	

		scalar = new GameObject ();// trait.scalar;
		scalar.name = "scalar";
		rotator = new GameObject ();//trait.rotator;
		rotator.name = "rotator";

		jointMesh.transform.parent = scalar.transform;
		scalar.transform.localScale = new Vector3 (1, trait.jointScale, 1);

		ballMesh.transform.parent = rotator.transform;

		scalar.transform.parent = rotator.transform;
		rotator.transform.parent = transform;

		if (trait.endJoint) {
			GameObject ballMesh2 = TREEUtils.makePart (trait.ballMesh, trait.material);
			ballMesh2.transform.parent = rotator.transform;
			ballMesh2.transform.localPosition = new Vector3 (0,trait.jointScale, 0);
		}

	}


}
}
