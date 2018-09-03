#pragma warning disable

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TREESharp{
	public class Trait : Object {

		public int id = 0;
		public Mesh ballMesh;
		public Mesh jointMesh;
		public float offset = 1;
		public float offset2 = 0;
		public Material material;
		public float jointScale = 1;
		public bool endJoint = false;
		public int joints = 0;

		public void makeDefault(params GameObject[] c){

			ballMesh =  Traits.Instance.ballMesh; 
			jointMesh = Traits.Instance.jointMesh;
			material =  Traits.Instance.material; 
		}

		public void Apply(Trait p){
			id = p.id;
			ballMesh = p.ballMesh;
			jointMesh = p.jointMesh;
			offset = p.offset;
			offset2 = p.offset2;
			material = p.material;
			jointScale = p.jointScale;
			endJoint = p.endJoint;
		}

		public string ToString(){
			return id.ToString();
		}

	}

	public class Traits : Singleton<Traits>{

		protected Traits () {}

		public Mesh ballMesh;
		public Mesh jointMesh;
		public Material material;

		public void build (){
			GameObject tempSphere = GameObject.CreatePrimitive (PrimitiveType.Cube);
			GameObject tempCylinder = GameObject.CreatePrimitive (PrimitiveType.Cube);
			ballMesh = tempSphere.GetComponent<MeshFilter>().mesh;
			jointMesh = tempCylinder.GetComponent<MeshFilter>().mesh;
			material = tempSphere.GetComponent<Renderer>().material;
			GameObject.Destroy (tempSphere);
			GameObject.Destroy (tempCylinder);
		}
	}
}
