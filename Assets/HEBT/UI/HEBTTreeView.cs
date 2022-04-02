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
        HintedExecutionBehaviourTree t;
        SerializedObject GetTarget;

        private static List<string> nodes = new List<string> { "ActionNode", "SequenceNode", "SelectorNode" };
        private static string[] nodesArr = nodes.ToArray();

        private static Dictionary<string, int> selectorIndexes = new Dictionary<string, int>();

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
                var id = Guid.NewGuid().ToString();
                if (GUILayout.Button("Initial Sequence Node"))
                {
                    tree.tree = new SequenceNode(new List<BaseNode> { }, id);
                    selectorIndexes.Add(id, 1);
                }

                if (GUILayout.Button("Initial Selector Node"))
                {
                    tree.tree = new SelectorNode(new List<BaseNode> { }, id);
                    selectorIndexes.Add(id, 2);
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
                DrawActionNode(node, parent);
                return;
            }

            GUILayout.BeginVertical(node.GetType().Name, GUI.skin.GetStyle("HelpBox"));
            GUILayout.Space(15);


            GUILayout.BeginHorizontal();

            if (!selectorIndexes.ContainsKey(node.GetId()))
            {
                selectorIndexes.Add(node.GetId(), nodes.IndexOf(node.GetType().ToString()));
            }

            selectorIndexes[node.GetId()] = EditorGUILayout.Popup(selectorIndexes[node.GetId()], nodesArr);
            if (GUILayout.Button("Refactor"))
            {
                ChangeFieldType(node, parent, selectorIndexes[node.GetId()]);
            };

            GUILayout.Space(100);
            if (GUILayout.Button("+"))
            {
                var id = Guid.NewGuid().ToString();
                node.AddChild(new SequenceNode(new List<BaseNode> { }, id));
                selectorIndexes.Add(id, 1);
            }
            if (GUILayout.Button("-"))
            {
                if (parent != null)
                {
                    selectorIndexes.Remove(node.GetId());
                    var ind = parent.GetChildren().IndexOf(node);
                    parent.RemoveChildAt(ind);
                }
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.SelectableLabel(node.GetId(), EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            EditorGUI.indentLevel++;
            for (int i = 0; i < node.GetChildren().Count; i++)
            {
                DrawNode(node.GetChildren()[i], node);
            }
            EditorGUI.indentLevel--;

            GUILayout.EndVertical();
        }

        private void DrawActionNode(BaseNode node, BaseNode parent)
        {
            GUILayout.BeginVertical(node != null ? node.GetType().Name : "", GUI.skin.GetStyle("HelpBox"));
            GUILayout.Space(15);

            GUILayout.BeginHorizontal();
            MonoScript script = null;
            script = (MonoScript)EditorGUILayout.ObjectField(script, typeof(MonoScript), false);
            if (script != null)
            {
                var changeIndex = parent.GetChildren().FindIndex(x => x == node);

                //parent.GetChildren().RemoveAt(changeIndex);
                BaseNode newNode = Activator.CreateInstance(script.GetClass()) as BaseNode;
                var id = Guid.NewGuid().ToString();
                newNode.SetId(id);
                parent.GetChildren()[changeIndex] = newNode;
                selectorIndexes[id] = 0;
                //parent.GetChildren().Insert(changeIndex, newNode);

            }

            if (GUILayout.Button("-"))
            {
                if (parent != null)
                {
                    selectorIndexes.Remove(node.GetId());
                    var index = parent.GetChildren().IndexOf(node);
                    parent.RemoveChildAt(index);
                }
            }

            GUILayout.EndHorizontal();

            if (node != null)
            {
                EditorGUILayout.SelectableLabel(node.GetId(), EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            }

            GUILayout.EndVertical();
        }

        private void ChangeFieldType(BaseNode node, BaseNode parent, int typeInd)
        {
            BaseNode newNode;

            switch (typeInd)
            {
                case 1:
                    newNode = new SequenceNode(node.GetChildren(), node.GetId());
                    break;
                case 2:
                    newNode = new SelectorNode(node.GetChildren(), node.GetId());
                    break;
                default:
                    newNode = new ActionNode();
                    newNode.SetId(node.GetId());
                    break;
            }

            if (parent == null)
            {
                HintedExecutionBehaviourTree tree = (HintedExecutionBehaviourTree)target;
                tree.tree = newNode;
            }
            else
            {
                var changeIndex = parent.GetChildren().FindIndex(x => x == node);
                parent.GetChildren()[changeIndex] = newNode;
            }
        }
    }
}
