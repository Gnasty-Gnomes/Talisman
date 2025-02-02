using UnityEngine;
using AISystem.Systems;
using AISystem.BehaviourTrees;
using AISystem.Data;

namespace AISystem.Behaviours
{
    public class GoToTarget : ActionNode
    {
        public Attack m_attack;
        public Vector3 m_playerHeight = new Vector3(0, 1.8f, 0);

        protected override NodeState Update(float dt)
        {
            if (m_input.m_aIMovement == null)
            {
                Debug.LogError("Failed to find the AIMovement on this behaviour. Cannot set Target position");
                return NodeState.FAILURE;
            }
            
            Vector3 targetPos = m_input.m_blackboard.m_target.m_position - m_playerHeight;
            Vector3 aiPos = m_input.m_go.transform.position;
            var offset = Vector3.Normalize(aiPos - targetPos) * m_attack.m_attackDistance;
            
            m_input.m_aIMovement?.SetDestination(targetPos + offset);
            
            if (m_input.m_aIMovement.m_atDestination == false)
            {                
                return NodeState.RUNNING;
            }            
            return NodeState.SUCCESS;
        }
    }
}
