using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TREESharp{
	public class TREEModSwapJoint : TREEMod {

		public string[] selectJoints;
		public TREESharp.Joint[] jointGeo;
		List<List<int[]>> selectedJoints;

		public override void Setup () {
			selectedJoints = TREEUtils.ArgsArrayToJointList (selectJoints, tree);
			swapGeo ();
			rebuild = false;
		}

		public override void Animate(){
			if (rebuild) {
				Setup ();	
			}
		}

		void swapGeo(){
			for (int i = 0; i < selectedJoints.Count; i++) {
				for (int j = 0; j < selectedJoints [i].Count; j++) {

					GameObject g = TREEUtils.findJoint (selectedJoints [i] [j], 0, tree.transform.GetChild (0).gameObject);
					Joint J = g.GetComponent<Joint> ();
					Joint thisJoint = Instantiate (jointGeo [i]);
					thisJoint.transform.parent = g.transform.parent;
//					TREEUtils.copyTransforms (jointGeo [i].GetComponent<GameObject>(), g);
					thisJoint.transform.position =   new Vector3(g.transform.position.x,g.transform.position.y,g.transform.position.z);
					thisJoint.transform.rotation =   new Quaternion(g.transform.rotation.x,g.transform.rotation.y,g.transform.rotation.z,g.transform.rotation.w);
					thisJoint.transform.localScale = new Vector3(g.transform.localScale.x,g.transform.localScale.y,g.transform.localScale.z);
					if (J.childJoint != null) {
						thisJoint.childJoint = J.childJoint;
						J.childJoint.transform.parent = thisJoint.transform;
					}
					thisJoint.dictionaryName = J.dictionaryName;
					thisJoint.scalar.transform.localScale = J.scalar.transform.localScale;



					thisJoint.joint = J.joint;
					thisJoint.joints = J.joints;
					thisJoint.offset = J.offset;
					thisJoint.offset2 = J.offset2;

					if (g.transform.parent != null) {
						Debug.Log (thisJoint.gameObject.name);
						g.transform.parent.GetComponent<Joint> ().childJoint = thisJoint.gameObject;
					}

					for (int k = 0; k < J.limbs.Count; k++) {
						thisJoint.limbs.Add (J.limbs [k]);
						J.limbs [k].transform.parent = thisJoint.transform;
					}

					Destroy (g);
				}
			}
		}
	}
}
