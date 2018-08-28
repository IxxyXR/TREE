using UnityEngine;
using System.Collections;

namespace TREESharp{


	public class TREEModBuildRandom : TREEMod {

		public GameObject defaultJoint;
		GameObject tempJoint;

		public string joints= "10",rads= "1",angles= "0",length= "1",divs= "1",start = "0",end = "-1";


		// Use this for initialization
		public override void Setup(){

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

			rebuild = false;

			if(tempJoint==null)
				tempJoint = Instantiate(TREEUtils.findJoint (new int[]{ 0, 0, 0}, 0, tree.gameObject).gameObject);

			GameObject g = TREEUtils.findJoint (new int[]{ 0, 0, (int)Random.Range(0,9) }, 0, tree.gameObject).gameObject;
			GameObject thisRoot = tree.Branch (10, g.transform);
			g.GetComponent<Joint> ().limbs.Add (thisRoot);

			for (int i = 0; i < 10; i++) {
				g = TREEUtils.findJoint (new int[]{ 0, 0, (int)Random.Range(0,9) }, 0, tree.gameObject).gameObject;
				thisRoot = tree.Branch (10, g.transform);
				g.GetComponent<Joint> ().limbs.Add (thisRoot);

				tree.jointDictionary.Clear ();
				TREEUtils.makeDictionary (tree.gameObject);
			}

//
//			GameObject p = Instantiate (tempJoint);
//			TREEUtils.appendBranch (g, p);
//
//			tree.jointDictionary.Clear ();
//			TREEUtils.makeDictionary (tree.gameObject);
//
//			p = Instantiate (tempJoint);
//			g = TREEUtils.findJoint (new int[]{ 0, 0, (int)Random.Range(0,10) }, 0, tree.gameObject).gameObject;
//
//			TREEUtils.appendBranch (g, p);

			tree.jointDictionary.Clear ();
			TREEUtils.makeDictionary (tree.gameObject);

//			p.transform.Rotate (new Vector3 (30, 30, 30));

		}
		
		// Update is called once per frame
		public override void Animate () {
		
		}
	}
}
//
//for(var i = 30 ; i < 99 ; i++){
//	var t = tree.FIND([0,0,i]);
//	var amt = 1+Math.round(Math.random()*(i/2));
//	if(Math.random()>.95){
//		var b = (Math.round(Math.random()*4))+1;
//		for(var j = 0 ; j < b ; j++){
//			tree.appendBranch(t,{amount:amt,ry:Math.random()*pi*4,rz:Math.random(),sc:4})
//		}
//	}
//}
//
////add twigs
//
//var lay = tree.reportLayers();
//
//for(var i = 0 ; i < lay[1].length ; i++){
//	var b = (Math.round(Math.random()*4))+1;
//	for(var j = 0 ; j < b ; j++){
//		var t = tree.findJoint(lay[1][i],Math.floor(Math.random()*lay[1][i].joints));
//		var amt = 1+Math.round(Math.random()*10);
//		tree.appendBranch(t,{amount:amt,sc:3,ry:Math.random()*6})
//	}
//}
//
//tree.makeDictionary();
//
//setSliders({"var1":0.5,"var2":0.12,"var3":0.2,"var4":-0.34,"var5":-0.1,"var6":0.2,"var7":-0.08});  

