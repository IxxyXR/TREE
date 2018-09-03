using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//[RequireComponent (typeof (TREESharp.TREEModBuild))]
//[RequireComponent (typeof (TREESharp.TREEModXForm))]

public class RandomBug : MonoBehaviour {

	GameObject tree;

	public bool rebuild;
	public string name = "Tree";
	public GameObject defaultJoint;

	public string 
	joints= "10",
	rads= "1",
	angles= "0",
	length= "1",
	divs= "1",
	start = "0",
	end = "-1";

	public string[] xFormSelector;
	public string[] xFormTransform;

	public string[] swapSelector;
	public GameObject[] swapGeo;



	TREESharp.TREEModBuild build;
	TREESharp.TREEModXForm xForm;
	TREESharp.TREEModCtrl ctrl;
	TREESharp.TREEModSwapGeo swap;


//	public bool reScale = false;

	// Use this for initialization
	void Awake () {
		rebuild = true;
	}

	void Update () {
        if (Input.anyKeyDown)
        {

            rebuild = true;
        }
		if (rebuild) {
			Form ();
			reForm ();
			reFormXForm ();
			rebuild = false;
			copyXForms ();
            tree.transform.parent = this.transform;
//			reScale = true;
		}

	}

	public void copyXForms(){
		tree.transform.rotation = this.transform.rotation;
		tree.transform.position = this.transform.position;

	}

	public void Form(){
		if (tree == null) {
			tree = new GameObject ();
			tree.transform.parent = this.transform.parent;
			tree.name = name;
		} else {
			Destroy (tree);
			tree = new GameObject ();
			tree.transform.parent = this.transform.parent;
			tree.name = name;
		}

		build = tree.AddComponent<TREESharp.TREEModBuild> ();
		ctrl =  tree.GetComponent<TREESharp.TREEModCtrl> ();
		xForm = tree.AddComponent<TREESharp.TREEModXForm> ();
		swap =  tree.AddComponent<TREESharp.TREEModSwapGeo> ();

		if (defaultJoint != null) {
			build.defaultJoint = defaultJoint;
		}

		ctrl.autoPopulate = false;
		ctrl.mods = new TREESharp.TREEMod[3];
		ctrl.mods [0] = build;
		ctrl.mods [1] = swap;
		ctrl.mods [2] = xForm;
		xForm.animating = true;
		xForm.selectJoints = new string[ xFormSelector.Length];
		////xForm.transformJoints = new string[xFormTransform.Length];
		xForm.timeScale = Mathf.PI * 2;
		swap.selectJoints = new string[ swapSelector.Length];
		swap.jointGeo = new TREESharp.Joint[swapGeo.Length];
	}

	public void reForm(){
		build.joints = rando (joints);
		build.rads = rando (rads);
		build.angles = rando (angles);
		build.length = rando (length);
		build.divs = rando (divs);
		build.start = rando (start);
		build.end = rando (end);
	}

	public string rando(string st){
		string[] ST = st.Split (new string[] { "," }, System.StringSplitOptions.None);
		string ret = "";
		for (int i = 0; i < ST.Length; i++) {
			string[] ST2 = ST[i].Split (new string[] { ">" }, System.StringSplitOptions.None);
			if (ST2.Length > 1) {
				float a = float.Parse (ST2 [0]);
				float b = float.Parse (ST2 [1]);
				float val = (float)Mathf.Round (Random.Range (a, b));
				ret += val;
			} else
				ret += ST2 [0];
			if (i < ST.Length - 1)
				ret += ",";
		}
		return ret;
	}

	public void reFormXForm(){
		for (int i = 0; i < xFormSelector.Length; i++) {
			xForm.selectJoints [i] = randoXform (xFormSelector [i]);
			////xForm.transformJoints [i] = randoXMod (xFormTransform [i]);
		}
		for (int i = 0; i < swapSelector.Length; i++) {
			swap.selectJoints [i] = randoXform (swapSelector [i]);
			GameObject g = swapGeo [i].transform.GetChild ((int)Mathf.Floor (Random.Range (0, swapGeo [i].transform.childCount))).gameObject;
			swap.jointGeo [i] = g.GetComponent<TREESharp.Joint> ();
		}

	}

	public string randoXform(string st){
		
		string ret = "";

		string[] ST = st.Split (new string[] { "|" }, System.StringSplitOptions.None);
		ret += ST [0];

		for (int i = 1; i < ST.Length; i++) {
			ret += "|";
			string[] ST3 = ST[i].Split (new string[] { "," }, System.StringSplitOptions.None);

			for (int j = 0; j < ST3.Length; j++) {

				string[] ST4 = ST3[j].Split (new string[] { ">" }, System.StringSplitOptions.None);

				if (ST4.Length > 1) {
					float a = float.Parse (ST4 [0]);
					float b = float.Parse (ST4 [1]);
					int val = (int)Mathf.Round (Random.Range (a, b));
					ret += val;
				} else
					ret += ST4 [0];
				if (j < ST3.Length - 1)
					ret += ",";
			}

		}
		return ret;
	}

	public string randoXMod(string st){
		string ret = "";
		string[] ST = st.Split (new string[] { "," }, System.StringSplitOptions.None);
		for (int i = 0; i < ST.Length; i++) {
			string[] ST3 = ST[i].Split (new string[] { ":" }, System.StringSplitOptions.None);
			ret += ST3 [0];
			ret += ":";

			for (int j = 1; j < ST3.Length; j++) {

				string[] ST4 = ST3[j].Split (new string[] { ">" }, System.StringSplitOptions.None);

				if (ST4.Length > 1) {
					float a = float.Parse (ST4 [0]);
					float b = float.Parse (ST4 [1]);
					float val = Random.Range (a, b);
					ret += val;
				} else
					ret += ST4 [0];
				
			}
				ret += ",";

		}
		return ret;
	}


}
