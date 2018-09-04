using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace TREESharp {
	
public class MakeTree : MonoBehaviour {
	
	TREE tree;
	public GameObject defaultJoint;
	public TreeTransform xForm { get; set; }

	[Tooltip("The number of joints on a particular branch layer ")]
	public string joints = "10";
	
	[Tooltip("How often the branch sprouts new branches (every 1st, 2nd, 5th joint etc) ")]
	public string divs = "1";

	[Tooltip("At which joint the branch starts branching")]
	public string start = "0";
	
	[Tooltip("At which joint the branch stops branching")]
	public string end = "-1";
	
	[Tooltip("The angle that new branches will be rotated to ")]
	public string angles = "0";
	
	[Tooltip("How long each joint on a branch is ")]
	public string length = "1";
	
	[Tooltip("How many branches sprout from each joint ")]
	public string rads = "1";
	
	

	public string[] selectJoints;
	public List<Transformer> Transformers;
	
	[FormerlySerializedAs ("LegacytransformJoints")]
	public string[] LegacyTransformJoints;

	public bool animate = true;

	public float counter { get; set; }
	public float countSpeed { get; set; }

	public float timeScale = 1;

	public bool reSetupXform = true;
	public bool rebuildTree = true;
	
	public void buildTree(){
		
		if (GetComponent<Joint> () != null) {
			if (GetComponent<Joint>().limbs.Count > 0) {
				GetComponent<Joint>().limbs.Clear();
			}
		}

		if (gameObject.GetComponent<TREE>() == null) {
			tree = gameObject.AddComponent<TREE>();
		}
		else
		{
			for (int i=0; i<transform.childCount; i++) {
				Destroy (transform.GetChild (0).gameObject);
			}
			tree = GetComponent<TREE>();
		}
			
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

		tree.jointDictionary.Clear();
		TREEUtils.makeDictionary(tree.gameObject);

	}
	
	void Update ()
	{
		
		if (rebuildTree) {
			rebuildTree = false;
			buildTree();
			reSetupXform = true;
		}
		else if (reSetupXform)
		{
			reSetupXform = false;
			if (gameObject.GetComponent<TreeTransform> () == null) {
				xForm = gameObject.AddComponent<TreeTransform> ();
			} else
				xForm = gameObject.GetComponent<TreeTransform> ();
			
			xForm.Setup(selectJoints, Transformers, tree);
			Animate();
		}
		else if (animate && !reSetupXform && !rebuildTree)
		{
			xForm.Animate(Time.time * timeScale);			
		}
	}

	public void Animate(){
		counter += countSpeed * timeScale;
		xForm.Animate(counter);
	}

	public void Animate(float offset){
		counter += offset;
		xForm.Animate (counter);
	}
		
}
	
}