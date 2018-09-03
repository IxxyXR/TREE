using UnityEngine;
using System.Collections.Generic;
// ReSharper disable CompareOfFloatsByEqualityOperator

// ReSharper disable once CheckNamespace
namespace TREESharp {
	
	public class TreeTransform : MonoBehaviour {

		public List<Transformer> Transformers = new List<Transformer>();

		readonly List<List<int[]>> selectedJoints = new List<List<int[]>>();
		GameObject root;
		readonly List<List<Vector3>> initialRotation = new List<List<Vector3>>();

		Vector3 sinRotate = Vector3.zero;
		Vector3 sinScale = Vector3.zero;
		Vector3 noiseRotate = Vector3.zero;
		Vector3 rotateOffset = Vector3.zero;
		Vector3 rotate = Vector3.zero;
		Vector3 scale = Vector3.one;

		void MakeDefaults(int amount){
			
			Transformers.Clear();
			for (var i = 0; i < amount; i++) {
				Transformers.Add(new Transformer());
			}
		}
				
		public void Setup(string[] joints, List<Transformer> args, TREE tree) {
			
			if (joints != null) {
				
				MakeDefaults (joints.Length);
				root = tree.gameObject;

				initialRotation.Clear();
				selectedJoints.Clear();

				for (var i = 0; i < joints.Length; i++) {

					var firstList = TREEUtils.makeList(joints [i], tree.GetComponent<TREE>());
					var initRotations = new List<Vector3>();

					foreach (var t in firstList)
					{
						var g = TREEUtils.findJoint(t, 0, root.transform.GetChild(0).gameObject);
						initRotations.Add (g.transform.localEulerAngles);
					}

					initialRotation.Add(initRotations);	
					selectedJoints.Add(firstList);

				}

				Transformers = args;
			}
		}
		
		public void ReturnToInitialState()
		{

			if (selectedJoints.Count==0) return;
			
			GameObject g;

			var jointDict = root.GetComponent<TREE>().jointDictionary;
			
			for (var i = 0; i < selectedJoints.Count; i++) {
				
				for (var j = 0; j < selectedJoints [i].Count; j++) {
					
					var currentJointKey = TREEUtils.arrayToString(selectedJoints[i][j]);

					if (jointDict.Count > 0 && jointDict.ContainsKey(currentJointKey))
					{
						g = jointDict[currentJointKey].gameObject;
					}
					else
					{
						g = TREEUtils.findJoint(
							selectedJoints[i][j],
							0,
							root.transform.GetChild(0).gameObject
						);
					}

					if (g!=null) {g.transform.localEulerAngles = initialRotation[i][j];}
				}
			}
		}

		public void Animate(float timer) {
			
			GameObject g;
			var jointDict = root.GetComponent<TREE>().jointDictionary;
			
			for (var i=0; i<selectedJoints.Count; i++)
			{

				for (var j=0; j<selectedJoints[i].Count; j++) {
					
					var currentJointKey = TREEUtils.arrayToString(selectedJoints[i][j]);
					
					if (jointDict.Count > 0 && jointDict.ContainsKey(currentJointKey))
					{
						g = jointDict[currentJointKey].gameObject;
					}
					else
					{
						g = TREEUtils.findJoint(
							selectedJoints[i][j],
							0,
							root.transform.GetChild(0).gameObject
						);
					}

					if (g != null) {
						
						var joint = g.GetComponent<Joint>();
						var jointNumber = joint.joint;
						var jointOffset = joint.offset;
						var jointOffset2 = joint.offset2;
						var init = initialRotation[i][j];

						sinRotate = Vector3.zero;
		             	noiseRotate = Vector3.zero;
						rotateOffset = Vector3.zero;
						sinScale = Vector3.zero;

						var xform = Transformers[i];
						
						rotate = xform.Rotation;

						if (xform.SinMultiplyR.x != 0 || xform.SinMultiplyR.y != 0 || xform.SinMultiplyR.z != 0)
						{
							
							sinRotate.x =
								(xform.SinMultiply * jointNumber + xform.SinMultiplyR.x) * Mathf.Sin(
									xform.SinSpeedR.x * timer + xform.SinOffsetR.x +
									xform.SinOffsetAxialR.x * jointOffset +
									xform.SinOffsetAxialA.x * jointOffset2 +
									xform.SinFrequencyR.x * jointNumber);
							
							sinRotate.y = (xform.SinMultiply * jointNumber + xform.SinMultiplyR.y) * Mathf.Sin(
								xform.SinSpeedR.y * timer + xform.SinOffsetR.y +
								xform.SinOffsetAxialR.y * jointOffset +
								xform.SinOffsetAxialA.y * jointOffset2 +
								xform.SinFrequencyR.y * jointNumber);
							
							sinRotate.z = (xform.SinMultiply * jointNumber + xform.SinMultiplyR.z) * Mathf.Sin(
								xform.SinSpeedR.z * timer + xform.SinOffsetR.z +
								xform.SinOffsetAxialR.z * jointOffset +
								xform.SinOffsetAxialA.z * jointOffset2 + xform.SinFrequencyR.z * jointNumber);

						}

						if (xform.NoiseMultiply.x != 0 || xform.NoiseMultiply.y != 0 || xform.NoiseMultiply.z != 0)
						{
							
							noiseRotate.x = (xform.NoiseJointMultiply * jointNumber + xform.NoiseMultiply.x) * TREEUtils.Noise(
									xform.NoiseSpeed.x * -timer + xform.NoiseOffset.x +
									xform.NoiseRootOffset.x * jointOffset +
									xform.NoiseOffsetAxial.x * jointOffset2 +
									xform.NoiseFrequency.x * jointNumber);

							noiseRotate.y = (xform.NoiseJointMultiply * jointNumber + xform.NoiseMultiply.y) * TREEUtils.Noise(
								xform.NoiseSpeed.y * -timer + xform.NoiseOffset.y +
								xform.NoiseRootOffset.y * jointOffset +
								xform.NoiseOffsetAxial.y * jointOffset2 +
								xform.NoiseFrequency.y * jointNumber);

							noiseRotate.z = (xform.NoiseJointMultiply * jointNumber + xform.NoiseMultiply.z) * TREEUtils.Noise(
								xform.NoiseSpeed.z * -timer + xform.NoiseOffset.z +
								xform.NoiseRootOffset.z * jointOffset +
								xform.NoiseOffsetAxial.z * jointOffset2 + xform.NoiseFrequency.z * jointNumber);

						}

						if (xform.SinMultiplyS.x != 0 || xform.SinMultiplyS.y != 0 || xform.SinMultiplyS.z != 0)
						{
							
							sinScale.x = (
								 xform.SinScaleMult * jointNumber +
								 xform.SinMultiplyS.x) * Mathf.Sin(
									xform.SinSpeedS.x * timer +
									xform.SinOffsetS.x +
									xform.SinOffsetFromRootOffset.x * jointOffset +
									xform.SinFrequencyS.x * jointNumber
								 );
							
							sinScale.y = (
								 xform.SinScaleMult * jointNumber +
								 xform.SinMultiplyS.y) * Mathf.Sin(
									xform.SinSpeedS.y * timer +
									xform.SinOffsetS.y +
									xform.SinOffsetFromRootOffset.y * jointOffset +
									xform.SinFrequencyS.y * jointNumber);
							
							sinScale.z = (
								 xform.SinScaleMult * jointNumber +
								 xform.SinMultiplyS.z) * Mathf.Sin(
									 xform.SinSpeedS.z * timer +
									 xform.SinOffsetS.z +
									 xform.SinOffsetFromRootOffset.z * jointOffset +
									 xform.SinFrequencyS.z * jointNumber
								 );

						}

						if (xform.SinUniformScale) {
							sinScale.Set(sinScale.x, sinScale.x, sinScale.x);
						}

						if (xform.Length != 0f) {
							if (joint.childJoint != null)
							{
								var off = (
									xform.LengthMult * jointNumber + 
									xform.SinMultiplyLength) * 
									Mathf.Sin(
										xform.SinSpeedLength * timer + 
										xform.SinOffsetLength + 
										xform.SinRootOffsetLength * jointOffset + 
										xform.SinAxisOffsetLength * jointOffset2 + 
										xform.SinFrequencyLength * jointNumber
									);
								joint.childJoint.transform.localPosition = new Vector3(0, xform.Length + off, 0); // ????
								var sc = joint.scalar.transform.localScale;
								joint.scalar.transform.localScale = new Vector3(sc.x, xform.Length + off, sc.z);
							}
						}

						rotateOffset = xform.OffsetRotation * timer;
						g.transform.localEulerAngles = rotate+init+sinRotate+noiseRotate+rotateOffset;

						// g.transform.Rotate (rotateOffset.x,rotateOffset.y,rotateOffset.z);
						scale.Set(
							xform.Scale + xform.ScaleXY.x,
							xform.Scale + xform.ScaleXY.y,
							xform.Scale + xform.ScaleXY.z
						);
						
						var overallScale = sinScale + scale;
					
						if (xform.SinColor!=Color.black) {
							
							var initCol = new Color (
								xform.SinColor.r, 
								xform.SinColor.g, 
								xform.SinColor.b
							);
							
							float hue, s, v;
							Color.RGBToHSV (initCol, out hue, out s, out v);
							var off =
								(xform.LengthColorMult * jointNumber + xform.SinMultiplyColor) * Mathf.Sin(
									xform.SinSpeedColor * timer + 
									xform.SinOffsetColor + xform.SinRootOffsetColor * jointOffset + 
									xform.SinAxisOffsetColor * jointOffset2 +
									xform.SinFrequencyColor * jointNumber
								);
							var fOff = off - Mathf.Floor(off);
							
							if (fOff < 0)
							{
								fOff -= Mathf.Floor(off);
							}

							foreach (Transform child in joint.scalar.transform)
							{
								var meshRenderer = child.gameObject.GetComponent<MeshRenderer>();
								if (meshRenderer != null)
								{
									float alpha = meshRenderer.material.color.a;
									Color color = Color.HSVToRGB(fOff, 1, 1);
									color.a = alpha;
									meshRenderer.material.color = color;
								}
							}
							
							foreach (Transform child in joint.scalar.transform)
							{
								var meshRenderer = child.gameObject.GetComponent<MeshRenderer>();
								if (meshRenderer != null)
								{
									float alpha = meshRenderer.material.color.a;
									Color color = Color.HSVToRGB(fOff, 1, 1);
									color.a = alpha;
									meshRenderer.material.color = color;
								}
							}
							
							foreach (Transform child in joint.rotator.transform)
							{
								var meshRenderer = child.gameObject.GetComponent<MeshRenderer>();
								if (meshRenderer != null)
								{
									float alpha = meshRenderer.material.color.a;
									Color color = Color.HSVToRGB(fOff, 1, 1);
									color.a = alpha;
									meshRenderer.material.color = color;
								}
							}
							// g.GetComponent<Joint>().scalar.transform.GetChild (0).GetComponent<MeshRenderer> ().material.color = Color.HSVToRGB (fOff, 1, 1);
						}

						if (!overallScale.Equals(Vector3.one))
						{
							g.transform.localScale = overallScale;
							// Vector3.Scale(overallScale , new Vector3 (Transforms [i] ["sx"], Transforms [i] ["sy"], Transforms [i] ["sz"]));
						}

					}
				}
			}

		}
		
	}
}
