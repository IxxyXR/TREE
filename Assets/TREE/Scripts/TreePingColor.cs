using UnityEngine;
using System.Collections;

namespace TREESharp{

	public class TreePingColor : TreePing {

		Color init;
		public Color color;
		float counter = 0;
		MeshRenderer[] rend;
		public float speed = 1;

		void Start(){
			rend = new MeshRenderer[1];
		}

		public override void Ping(){
			rend[0] = this.GetComponent<Joint> ().scalar.transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ();
			init = rend [0].material.color;
			StartCoroutine (scale ());
		}

		IEnumerator scale(){
			while(counter<1){
				counter += Time.deltaTime;
				for (int i = 0; i < rend.Length; i++) {
					if(rend[i]!=null)
						if(rend[i].material!=null)
							rend[i].material.color = Color.Lerp (color, init, counter);

				}
				yield return new WaitForSeconds (Time.deltaTime*speed);
			}
			counter = 0;
		}
	}
}
