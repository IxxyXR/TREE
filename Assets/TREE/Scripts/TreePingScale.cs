using UnityEngine;
using System.Collections;

namespace TREESharp{

	public class TreePingScale : TreePing {

		Vector3 init;
		public Vector3 scaleTo;
		float counter = 0;
		public float speed = 1;
		void Start(){
			init = this.transform.localScale;
		}

		public override void Ping(){
			StartCoroutine (scale ());
		}

		IEnumerator scale(){
			while(counter<1){
				counter += Time.deltaTime;
				this.transform.localScale = Vector3.Lerp (scaleTo, init, counter);
				yield return new WaitForSeconds (Time.deltaTime * speed);
			}
			counter = 0;
		}
	}
}
