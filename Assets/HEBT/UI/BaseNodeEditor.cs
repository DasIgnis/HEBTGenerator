using Assets.Behaviours;
using HEBT.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.HEBT.UI
{
    [CustomEditor(typeof(BaseNode))]
    class BaseNodeEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            BaseNode node = (BaseNode)target;
            /*serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tree"));
            serializedObject.ApplyModifiedProperties();*/
        }
    }
}
