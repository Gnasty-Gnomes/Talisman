using System;
using UnityEngine;

namespace AISystem.BehaviourTrees
{
    [System.Serializable]
    public class BehaviourTree
    {
        BehaviourInput m_input;

        [SerializeReference] public Node m_parent;
        [SerializeReference] public Node[] m_nodes = Array.Empty<Node>();

        public BehaviourTree(Node[] nodes)
        {
            m_nodes = nodes;
            m_parent = m_nodes[0];
        }

        public void SetBehaviourInput(BehaviourInput input)
        {
            m_input = input;
            foreach (Node node in m_nodes)
            {
                node.SetBehaviourInput(input);
            }
        }

        public void Update(float dt)
        {
            m_parent.Evaluate(dt);
        }
    }

}