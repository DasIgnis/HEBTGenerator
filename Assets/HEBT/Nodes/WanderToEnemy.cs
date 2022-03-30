using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.HEBT.Nodes
{
    public class WanderToEnemy : BaseNode
    {
        public void AddChild(BaseNode node) { }

        public ExecutionResponse Execute(IEnvironment args)
        {
            throw new NotImplementedException();
        }

        public List<BaseNode> GetChildren()
        {
            throw new NotImplementedException();
        }

        public string GetId()
        {
            throw new NotImplementedException();
        }

        public void RemoveChildAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Reorder(List<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}
