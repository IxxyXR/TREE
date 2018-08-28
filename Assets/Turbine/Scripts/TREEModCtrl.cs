using UnityEngine;
using System.Collections;

namespace TREESharp{
	public class TREEModCtrl : MonoBehaviour {

		public TREEMod[] mods;
		public bool rebuild = true;
		public bool animate = true;
		TREE tree;
		public bool autoPopulate = true;

		public void Start () {
			Build();
		}

		public void Build(){
			if (autoPopulate) {
				if(mods.Length>0)
					mods = new TREEMod[0];
				mods = GetComponents<TREEMod> ();
			}
			if (this.GetComponent<Joint> () != null) {
				if (this.GetComponent<Joint> ().limbs.Count > 0) {
					this.GetComponent<Joint> ().limbs.Clear ();
				}
			}
			if (this.gameObject.GetComponent<TREE> () == null) {
				tree = this.gameObject.AddComponent<TREE> ();
			}
			else {
				for (int i = 0; i < this.transform.childCount; i++) {
					Destroy (this.transform.GetChild (0).gameObject);
				}
				DestroyImmediate (tree);
				tree = this.gameObject.AddComponent<TREE> ();
			}
			for (int i = 0; i < mods.Length; i++) {
				mods [i].tree = tree;
				mods [i].animate = false;
			}

			StartCoroutine(reBuild());
			rebuild = false;
		}

		IEnumerator reBuild()
		{
			for (int i = 0; i < mods.Length; i++) {
				mods [i].tree = tree;
				if (i > 0)
					mods [i - 1].animate = true;
				mods[i].HardReset();
				mods [i].Setup ();
				yield return null;
			}
			if(mods.Length>0)
				mods [mods.Length - 1].animate = true;
		}

		
		// Update is called once per frame
		public void Update () {
			if (rebuild) {
				Build ();
			}
			if (animate) {
				for (int i = 0; i < mods.Length; i++) {
					mods [i].Animate ();
				}
			}
		}
	}
}
