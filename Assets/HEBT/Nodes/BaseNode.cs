using Assets.HEBT.Nodes.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    [Serializable]
    public class BaseNode<T>
    {
        [SerializeReference]
        public List<BaseNode<T>> _children;

        public List<BaseNode<T>> GetChildren() { return null; }

        public ExecutionResponse Execute(T args) { return null; }

        public string GetId() { return ""; }

        public void Reorder(List<string> ids)
        {
            _children.Sort((x, y) => ids.IndexOf(x.GetId()) - ids.IndexOf(y.GetId()));
            _children.ForEach(x => x.Reorder(ids));
        }
    }
}