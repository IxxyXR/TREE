using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TREESharp{
//	[RequireComponent (typeof (TREE))]

	public class TREEModBuild : TREEMod {
		
		public GameObject defaultJoint;

		public string joints= "10",rads= "1",angles= "0",length= "1",divs= "1",start = "0",end = "-1";

		public override void Setup(){
			
//			if (this.GetComponent<Joint> () != null) {
//				if (this.GetComponent<Joint> ().limbs.Count > 0) {
//					this.GetComponent<Joint> ().limbs.Clear ();
//				}
//			}
//
//			if (this.gameObject.GetComponent<TREE> () == null) {
//				tree = this.gameObject.AddComponent<TREE> ();
//			}
//			else {
//				for (int i = 0; i < this.transform.childCount; i++) {
//					Destroy (this.transform.GetChild (0).gameObject);
//				}
//				tree = GetComponent<TREE> ();
//			}

			tree.setDefaultJoint(defaultJoint);

			tree.generate (
				"joints",	joints,
				"rads",		rads,
				"angles",	angles,
				"length",	length,
				"divs",		divs,
				"start",	start,
				"end",		end
			);
			tree.jointDictionary.Clear ();
			TREEUtils.makeDictionary (tree.gameObject);
			rebuild = false;

		}
			
		public override void Animate () {
			if (rebuild) {
				Setup ();
//				if(this.GetComponent<TREEModCtrl> ()!=null)
//					this.GetComponent<TREEModCtrl> ().Build ();
			}
		}
	}
}