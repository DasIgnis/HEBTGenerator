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
        [SerializeField]
        public string _id;

        public WanderToEnemy()
        {
        }

        public void AddChild(BaseNode node) { }

        public ExecutionResponse Execute(IEnvironment args)
        {
            Debug.Log("IsNear");
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.SUCCESS };
        }

        public List<BaseNode> GetChildren()
        {
            return new List<BaseNode> { };
        }

        public string GetId()
        {
            return _id;
        }

        public void SetId(string id)
        {
            _id = id;
        } 

        public void RemoveChildAt(int index) { }

        public void Reorder(List<string> ids) { }
    }
}
