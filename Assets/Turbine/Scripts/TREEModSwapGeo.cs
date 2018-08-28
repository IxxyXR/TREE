using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TREESharp{
	public class TREEModSwapGeo : TREEMod {

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
					GameObject duplicateRotator = Instantiate (jointGeo [i].rotator.transform.GetChild(0)).gameObject;
					GameObject duplicateScalar = Instantiate (jointGeo [i].scalar);
					duplicateScalar.name = "scalar_" + i + "_" + j;
					duplicateRotator.name = "rotator" + i + "_" + j;

					Transform t = g.GetComponent<TREESharp.Joint> ().rotator.transform;

					TREEUtils.copyTransforms (duplicateScalar, g.GetComponent<TREESharp.Joint> ().scalar.gameObject);
					TREEUtils.copyTransforms (duplicateRotator, g.GetComponent<TREESharp.Joint> ().rotator.gameObject);

					Destroy (g.GetComponent<TREESharp.Joint> ().scalar.gameObject);
					Destroy (g.GetComponent<TREESharp.Joint> ().rotator.transform.GetChild(0).gameObject);
					duplicateRotator.transform.parent =  t;
					duplicateScalar.transform.parent =  t;

					g.GetComponent<TREESharp.Joint> ().scalar = duplicateScalar;
//
					if (g.GetComponent<TREESharp.Joint> ().tip != null) {
						GameObject tip = Instantiate (jointGeo [i].rotator.transform.GetChild(0)).gameObject;
						TREEUtils.copyTransforms (tip, g.GetComponent<TREESharp.Joint> ().tip.gameObject);
						tip.transform.parent =  t ;
						Destroy (g.GetComponent<TREESharp.Joint> ().tip);
					}

				}
			}
		}
	}
}
