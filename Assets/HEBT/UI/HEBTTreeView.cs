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

        private static List<string> nodes = new List<string> { "ActionNode", "SequenceNode", "SelectorNode" };
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

            if (!isCollapsed.ContainsKey(node.GetId())) isCollapsed.Add(node.GetId(), false);
            isCollapsed[node.GetId()] = EditorGUILayout.Foldout(isCollapsed[node.GetId()], node.GetType().Name);

            GUILayout.BeginVertical();

            if (!isCollapsed[node.GetId()])
            {
                DrawReorderingBtnGroup(node, parent);


                GUILayout.BeginHorizontal();

                if (!selectorIndexes.ContainsKey(node.GetId()))
                {
                    hashedIndexes.Add(node.GetId(), nodes.IndexOf(node.GetType().ToString()));//
                    selectorIndexes.Add(node.GetId(), nodes.IndexOf(node.GetType().ToString()));
                }

                hashedIndexes[node.GetId()] = selectorIndexes[node.GetId()];
                selectorIndexes[node.GetId()] = EditorGUILayout.Popup(selectorIndexes[node.GetId()], nodesArr);
                if (hashedIndexes[node.GetId()] != selectorIndexes[node.GetId()])
                {
                    ChangeFieldType(node, parent, selectorIndexes[node.GetId()]);
                };

                GUILayout.Space(50);
                if (GUILayout.Button("+", GUILayout.Width(25)))
                {
                    var id = typeof(SequenceNode).Name + "_" + GetRandomHexNumber(4);
                    node.AddChild(new SequenceNode(new List<BaseNode> { }, id));
                    selectorIndexes.Add(id, 1);
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

                DrawIdGroup(node);
            }

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
            //GUILayout.BeginVertical(node != null ? node.GetType().Name : "", GUI.skin.GetStyle("HelpBox"));
            //GUILayout.Space(15);
            if (!isCollapsed.ContainsKey(node.GetId())) isCollapsed.Add(node.GetId(), false);
            isCollapsed[node.GetId()] = EditorGUILayout.Foldout(isCollapsed[node.GetId()], node.GetType().Name);

            if (!isCollapsed[node.GetId()])
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

                if (GUILayout.Button("-", GUILayout.Width(25)))
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
                    DrawIdGroup(node);
                }
            }

            //GUILayout.EndVertical();
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

        private void DrawReorderingBtnGroup(BaseNode node, BaseNode parent)
        {
            if (parent == null)
            {
                return;
            }

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            int indexInParent = parent.GetChildren().IndexOf(node);

            if (parent != t.tree && GUILayout.Button("\u2190", GUILayout.Width(25)))  //previous level
            {
                
            }
            if (indexInParent != 0 && GUILayout.Button("\u2192", GUILayout.Width(25)))  //next level
            {

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
