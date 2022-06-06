using Assets.Behaviours;
using HEBT;
using HEBT.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class StoreInterfaceReference : MonoBehaviour
{
    [CustomEditor(typeof(HintedExecutionBehaviourTree))]
    public class HEBTInspector : Editor
    {
        HintedExecutionBehaviourTree t;
        SerializedObject GetTarget;

        private static Random random = new Random();

        private static List<string> nodes = new List<string> { "ActionNode", "SequenceNode", "SelectorNode", "TimeMonitorNode" };
        private static string[] nodesArr = nodes.ToArray();

        private static Dictionary<string, int> selectorIndexes = new Dictionary<string, int>();
        private static Dictionary<string, int> hashedIndexes = new Dictionary<string, int>();
        private static Dictionary<string, bool> isCollapsed = new Dictionary<string, bool>();

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
                DrawNode(tree.tree, new List<BaseNode>());
            }
        }

        private void DrawNode(BaseNode node, List<BaseNode> parents)
        {
            if (!isCollapsed.ContainsKey(node.GetId())) isCollapsed.Add(node.GetId(), false);
            isCollapsed[node.GetId()] = EditorGUILayout.Foldout(isCollapsed[node.GetId()], node.GetType().Name);

            GUILayout.BeginVertical();

            if (!isCollapsed[node.GetId()])
            {
                DrawReorderingBtnGroup(node, parents);

                DrawChangeTypeGroup(node, parents.Count == 0 ? null : parents.Last(), true);

                if (node == null || node.GetType().Equals(typeof(ActionNode)) || node.GetType().IsSubclassOf(typeof(ActionNode)))
                {
                    DrawMonoScriptGetter(node, parents.Count == 0 ? null : parents.Last());
                }

                DrawIdGroup(node);
            }

            EditorGUI.indentLevel++;
            for (int i = 0; i < node.GetChildren().Count; i++)
            {
                List<BaseNode> parentNodes = new List<BaseNode> (parents);
                parentNodes.Add(node);
                DrawNode(node.GetChildren()[i], parentNodes);
            }
            EditorGUI.indentLevel--;

            GUILayout.EndVertical();
        }

        private void DrawChangeTypeGroup(BaseNode node, BaseNode parent, bool canAddChildren)
        {
            GUILayout.BeginHorizontal();

            if (!selectorIndexes.ContainsKey(node.GetId()))
            {
                hashedIndexes.Add(node.GetId(), nodes.IndexOf(node.GetType().ToString()));
                selectorIndexes.Add(node.GetId(), nodes.IndexOf(node.GetType().ToString()));
            }

            hashedIndexes[node.GetId()] = selectorIndexes[node.GetId()];
            selectorIndexes[node.GetId()] = EditorGUILayout.Popup(selectorIndexes[node.GetId()], nodesArr);
            if (hashedIndexes[node.GetId()] != selectorIndexes[node.GetId()])
            {
                ChangeFieldType(node, parent, selectorIndexes[node.GetId()]);
            };

            GUILayout.Space(50);
            if (canAddChildren)
            {
                if (GUILayout.Button("+", GUILayout.Width(25)))
                {
                    var id = typeof(SequenceNode).Name + "_" + GetRandomHexNumber(4);
                    node.AddChild(new SequenceNode(new List<BaseNode> { }, id));
                    selectorIndexes.Add(id, 1);
                }
            }
            if (GUILayout.Button("-", GUILayout.Width(25)))
            {
                if (parent != null)
                {
                    selectorIndexes.Remove(node.GetId());
                    var ind = parent.GetChildren().IndexOf(node);
                    parent.RemoveChildAt(ind);
                }
            }

            GUILayout.EndHorizontal();
        }

        private void DrawMonoScriptGetter(BaseNode node, BaseNode parent)
        {
            GUILayout.BeginHorizontal();

            MonoScript script = null;
            script = (MonoScript)EditorGUILayout.ObjectField(script, typeof(MonoScript), false);
            if (script != null)
            {
                var changeIndex = parent.GetChildren().FindIndex(x => x == node);

                BaseNode newNode = Activator.CreateInstance(script.GetClass()) as BaseNode;
                newNode.SetId(node.GetId());
                parent.GetChildren()[changeIndex] = newNode;
                selectorIndexes[node.GetId()] = 0;

            }

            GUILayout.EndHorizontal();
        }

        private void DrawIdGroup(BaseNode node)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.SelectableLabel(node.GetId(), EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            if (GUILayout.Button("\u21BB", GUILayout.Width(50)))
            {
                UpdateNodeId(node);
            }

            GUILayout.EndHorizontal();
        }

        private void DrawReorderingBtnGroup(BaseNode node, List<BaseNode> parents)
        {
            if (parents.Count == 0)
            {
                return;
            }

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            var parent = parents.Last();
            int indexInParent = parent.GetChildren().IndexOf(node);

            if (parent != t.tree && GUILayout.Button("\u2190", GUILayout.Width(25)))  //previous level
            {
                var grandpa = parents.ElementAt(parents.Count - 2);
                grandpa.GetChildren().Insert(grandpa.GetChildren().IndexOf(parent), node);
                parent.RemoveChildAt(indexInParent);
            }
            if (indexInParent != 0 && GUILayout.Button("\u2192", GUILayout.Width(25)))  //next level
            {
                var newParent = parent.GetChildren()[indexInParent - 1];
                if (!newParent.GetType().IsSubclassOf(typeof(ActionNode)))
                {
                    newParent.AddChild(node);
                    parent.RemoveChildAt(indexInParent);
                };
            }
            if (indexInParent != 0 && GUILayout.Button("\u2191", GUILayout.Width(25)))  //up
            {
                (parent.GetChildren()[indexInParent], parent.GetChildren()[indexInParent - 1]) = (parent.GetChildren()[indexInParent - 1], parent.GetChildren()[indexInParent]);
            }
            if (indexInParent != (parent.GetChildren().Count - 1) && GUILayout.Button("\u2193", GUILayout.Width(25)))  //down
            {
                (parent.GetChildren()[indexInParent], parent.GetChildren()[indexInParent + 1]) = (parent.GetChildren()[indexInParent + 1], parent.GetChildren()[indexInParent]);
            }
            GUILayout.EndHorizontal();
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
                case 3:
                    newNode = new TimeMonitorNode(node.GetChildren(), node.GetId());
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

        private void UpdateNodeId(BaseNode node)
        {
            var id = node.GetType().Name + "_" + GetRandomHexNumber(4);
            node.SetId(id);
        }

        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }
    }
}
