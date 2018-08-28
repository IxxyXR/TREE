using UnityEngine;
using System.Collections;

namespace TREESharp{
	[RequireComponent (typeof (TREEModCtrl))]
	public class TREEMod : MonoBehaviour {

		public TREE tree { get; set; }
		public bool rebuild { get; set; }
		public bool animate { get; set; }
		public virtual void Setup (){
		}
		public virtual void HardReset(){
		}
		public virtual void Animate (){
		}
	}
}
