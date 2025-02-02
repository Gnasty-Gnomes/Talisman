﻿using UnityEngine;
using UnityEngine.AI;
using AISystem.BehaviourTrees;

namespace AISystem.Behaviours
{
    public class SetDestination : ActionNode
    {
        public Vector3 m_target;
        NavMeshAgent m_agent;

        protected override void BeginNode()
        {
            base.BeginNode();
            if (m_agent == null)
            {
                m_agent = m_input.m_go.GetComponent<NavMeshAgent>();
            }
        }

        protected override NodeState Update(float dt)
        {
            if (m_input.m_aIMovement == null)
            {               
                Debug.LogError("Could not find AIMovement on behaviour! Cannot set destination!");
                return NodeState.FAILURE;
            }

            m_input.m_aIMovement?.SetDestination(m_target);
            if (m_input.m_aIMovement.m_atDestination == false)
            {
                return NodeState.RUNNING;
            }
            return NodeState.SUCCESS;
        }
    }
}
