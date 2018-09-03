using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TREESharp{
	public class TREEModPing : TREEMod {

		public float delay = 1;
		public bool pong = false;
//		public string[] selectJoints;
//		public TREESharp.Joint[] jointGeo;
		List<List<int[]>> selectedJoints;

		public override void Setup () {
			selectedJoints = TREEUtils.ArgsArrayToJointList (new string[]{"0|-1|0"}, tree);
			rebuild = false;
			Init ();
		}

		public override void Animate(){
			if (rebuild) {
				Setup ();
				Init ();
			}

			if (pong) {
				Init ();
				pong = false;
			}
		}


		void Init(){
			for (int i = 0; i < selectedJoints.Count; i++) {
				for (int j = 0; j < selectedJoints [i].Count; j++) {
					GameObject g = TREEUtils.findJoint (selectedJoints[i][j], 0, tree.transform.GetChild (0).gameObject);
					StartCoroutine (Traverse (g));
				}
			}
		}

		IEnumerator Traverse(GameObject Joint){

			TREESharp.Joint J = Joint.GetComponent<TREESharp.Joint> ();
			TREESharp.TreePing[] P = Joint.GetComponents <TREESharp.TreePing> ();

			yield return new WaitForSeconds (delay*J.scalar.transform.lossyScale.y);

			for (int i = 0; i < P.Length; i++) {
				P[i].Ping ();
			}


			if (J.childJoint != null)
				StartCoroutine (Traverse (J.childJoint));
			for (int i = 0; i < J.limbs.Count; i++) {
				StartCoroutine (Traverse (J.limbs[i]));
			}
		}
	}
}
