using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HEBT.Nodes
{
    public class ActionNode<T> : BaseNode
    {
        private T _args;
        private event Action<T> _action;
        private IAsyncResult _asyncResult;

        public ActionNode(T args, Action<T> action)
        {
            _args = args;
            _action = action;
        }

        public BaseNodeExecutionStatus Execute()
        {
            if (_asyncResult == null)
            {
                _asyncResult = _action.BeginInvoke(
                    _args,
                    (IAsyncResult result) =>
                    {
                        _action.EndInvoke(result);
                    },
                    null
                 );
                return BaseNodeExecutionStatus.RUNNING;
            }

            if (_asyncResult.IsCompleted)
            {
                _asyncResult = null;
                return BaseNodeExecutionStatus.SUCCESS;
            }


            return BaseNodeExecutionStatus.RUNNING;
        }

        public List<BaseNode> GetChildren()
        {
            return new List<BaseNode>();
        }
    }
}