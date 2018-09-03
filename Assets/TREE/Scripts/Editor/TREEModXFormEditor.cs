using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TREESharp;
using UnityEditor;
using UnityEngine;

public class TREEModXFormEditor : MonoBehaviour {

	[CustomEditor(typeof(TREEModXForm))]
	class TreeBuilderEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			if (GUILayout.Button("Convert"))
			{
				var t = target as TREEModXForm;
				t.Transformers = TREEUtils.ConvertTransformers(t.transformJoints).ToList();
			}
		}
	}
}
