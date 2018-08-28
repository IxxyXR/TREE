using UnityEngine;
using System.Collections;

namespace TREESharp{

	public class TreePingPlaySound : TreePing {

		AudioSource audi;
		public float pitchShift = 1;


		void Start(){
			audi = this.GetComponent<AudioSource> ();
		}

		public override void Ping(){
			audi.pitch = this.GetComponent<Joint> ().scalar.transform.lossyScale.y * pitchShift;
			audi.Play ();
			Debug.Log ("play");
		}

	}
}
