using HEBT;
using HEBT.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletorBehaviorScript : MonoBehaviour
{
    [SerializeField]
    public GameObject flag;

    private HintedExecutionBehaviourTree _behaviourTree;
    private ActionNodeParams _params;

    private class ActionNodeParams
    {
        public Vector3 FlagPosition { get; set; }
        public Vector3 CurrentPosition { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _params = new ActionNodeParams
        {
            FlagPosition = flag.transform.position,
            CurrentPosition = transform.position
        };


        ActionNode<ActionNodeParams> root = new ActionNode<ActionNodeParams>(
            _params,
            (ActionNodeParams actionNodeParams) =>
            {
                actionNodeParams.CurrentPosition = Vector3.MoveTowards(
                    actionNodeParams.CurrentPosition, 
                    actionNodeParams.FlagPosition,
                    0.01f);
            }
        );

        _behaviourTree = new HintedExecutionBehaviourTree(root);
    }

    // Update is called once per frame
    void Update()
    {
        _behaviourTree.Execute();
        transform.position = _params.CurrentPosition;
    }
}
