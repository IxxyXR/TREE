using UnityEngine;
using System.Collections;

namespace TREESharp{

	public class TreePingPlayEmit : TreePing {

		public ParticleSystem parti;
        public int amount;
        public float fadeTime   ;

		void Start(){
	
		}

		public override void Ping(){
            //parti.Emit(amount);
            StartCoroutine(Fader());
		}

        IEnumerator Fader() {

            float count = 0;
            while (count < fadeTime) {
                count += Time.deltaTime;
                float c = count / fadeTime;
                int toEmit = (int)((1-c)*amount);
                parti.Emit(toEmit);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

	}
}
