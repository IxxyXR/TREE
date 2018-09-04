using System.Linq;
using TREESharp;
using UnityEditor;
using UnityEngine;

public class MakeTreeEditor : MonoBehaviour
{

    [CustomEditor(typeof(MakeTree))]
    class TreeBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Convert Legacy Joints"))
            {
                var t = target as MakeTree;
                t.Transformers = TREEUtils.ConvertTransformers(t.LegacyTransformJoints).ToList();
            }
        }
    }
}