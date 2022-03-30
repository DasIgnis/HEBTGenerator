using Assets.Behaviours;
using HEBT;
using HEBT.Nodes;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoreInterfaceReference : MonoBehaviour
{
    [CustomEditor(typeof(HintedExecutionBehaviourTree))]
    public class HEBTInspector : Editor
    {
        enum displayFieldType { DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields }
        displayFieldType DisplayFieldType;

        HintedExecutionBehaviourTree t;
        SerializedObject GetTarget;

        void OnEnable()
        {
            t = (HintedExecutionBehaviourTree)target;
            GetTarget = new SerializedObject(t);
        }

        public override void OnInspectorGUI()
        {
            HintedExecutionBehaviourTree tree = (HintedExecutionBehaviourTree)target;

            if (tree.tree == null)
            {
                if (GUILayout.Button("Initial Sequence Node"))
                {
                    tree.tree = new SequenceNode();
                }

                if (GUILayout.Button("Initial Selector Node"))
                {
                    tree.tree = new SelectorNode();
                }
            } else
            {
                DrawNode(tree.tree, null);
            }
        }

        private void DrawNode(BaseNode node, BaseNode parent)
        {
            
            if (node == null ||
                (node.GetType().Name != "SequenceNode"
                && node.GetType().Name != "SelectorNode"))
            {
                GUILayout.BeginVertical(node != null ? node.GetType().Name : "", GUI.skin.GetStyle("HelpBox"));
                GUILayout.Space(15);

                GUILayout.BeginHorizontal();
                MonoScript script = null;
                script = (MonoScript)EditorGUILayout.ObjectField(script, typeof(MonoScript), false);
                if (script != null)
                {
                    var changeIndex = parent.GetChildren().FindIndex(x => x == null || x.GetType().Name == "ActionNode");
                    parent.GetChildren().RemoveAt(changeIndex);
                    BaseNode newNode = Activator.CreateInstance(script.GetClass()) as BaseNode;
                    parent.GetChildren().Insert(changeIndex, newNode);

                }

                if (GUILayout.Button("-"))
                {
                    if (parent != null)
                    {
                        var index = parent.GetChildren().IndexOf(node);
                        parent.RemoveChildAt(index);
                    }
                }

                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                return;
            }

            GUILayout.BeginVertical(node.GetType().Name, GUI.skin.GetStyle("HelpBox"));
            GUILayout.Space(15);


            GUILayout.BeginHorizontal();
            
            GUILayout.Space(15);
            if (GUILayout.Button("+Sequence"))
            {
                node.AddChild(new SequenceNode());
            }

            if (GUILayout.Button("+Selector"))
            {
                node.AddChild(new SelectorNode());
            }
            if (GUILayout.Button("+Action"))
            {
                node.AddChild(new ActionNode());
            }
            if (GUILayout.Button("-"))
            {
                if (parent != null)
                {
                    var index = parent.GetChildren().IndexOf(node);
                    parent.RemoveChildAt(index);
                }
            }
            GUILayout.EndHorizontal();

            EditorGUI.indentLevel++;
            for (int i = 0; i < node.GetChildren().Count; i++)
            {
                DrawNode(node.GetChildren()[i], node);
            }
            EditorGUI.indentLevel--;

            GUILayout.EndVertical();
        }
    }
}
