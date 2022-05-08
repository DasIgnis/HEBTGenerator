using Assets.Behaviours;
using Assets.HEBT.Nodes.Models;
using HEBT.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.HEBT.Nodes
{
    public class WanderToEnemy : ActionNode
    {
        private NavMeshAgent _agent;

        public override ExecutionResponse Execute(IEnvironment args, ref string currentNode)
        {
            Debug.Log("WanderTo");
            if (NotCurrent(ref currentNode))
            {
                return new ExecutionResponse { Status = BaseNodeExecutionStatus.SKIP };
            }
            var env = args.GetEnvironmentVariables() as RadiantAIEnvironmentParams;

            _agent = env.Me.GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.SetDestination(env.Chest.transform.position);

            if (Vector2.Distance(env.Me.transform.position, env.Chest.transform.position) < 1)
            {
                _agent.ResetPath();
                return new ExecutionResponse { Status = BaseNodeExecutionStatus.SUCCESS };
            }
            return new ExecutionResponse { Status = BaseNodeExecutionStatus.RUNNING, ExecutingActionNodeId = _id };
        }

        public override void Interrupt()
        {
            if (_agent != null)
            {
                _agent.ResetPath();
            }
        }
    }
}
