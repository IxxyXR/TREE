using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TREESharp{
	
	public class TREEModXForm : TREEMod {
	
		public TreeTransform xForm {get;set;}

		public string[] selectJoints;
		public List<Transformer> Transformers;
		public string[] transformJoints;

		public float counter { get; set; }
		public float countSpeed { get; set; }

		public float timeScale = 1;

		public bool animating = false;

		public override void Setup(){

			if (gameObject.GetComponent<TreeTransform>() == null) {
				xForm = gameObject.AddComponent<TreeTransform>();
			} else {
				xForm.ReturnToInitialState();
				DestroyImmediate(xForm);
				DestroyImmediate(gameObject.AddComponent<TreeTransform>());
				xForm = gameObject.AddComponent<TreeTransform>();
			}
			if(tree!=null)
				xForm.Setup(selectJoints, Transformers, tree);
			Step ();
			rebuild = false;
		}

		public override void HardReset(){
			DestroyImmediate(xForm);
			if (gameObject.GetComponent<TreeTransform>()==null) {
				xForm = gameObject.AddComponent<TreeTransform>();
			} else {
				xForm = gameObject.GetComponent<TreeTransform>();
			}
		}
			
		public override void Animate() {
			if (animating && animate && !rebuild)
				xForm.Animate (Time.time * timeScale);
			if (rebuild) {
				Setup();
			}
		}

		public void Step(){
			counter += countSpeed * timeScale;
			xForm.Animate (counter);
		}

		public void Step(float offset){
			counter += offset;
			xForm.Animate (counter);
		}
			
	}
}