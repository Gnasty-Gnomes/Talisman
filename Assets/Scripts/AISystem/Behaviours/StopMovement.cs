using AISystem.BehaviourTrees;
using System.Diagnostics;

namespace AISystem.Behaviours
{
    public class StopMovement : ActionNode
    {
        protected override NodeState Update(float dt)
        {
            if (m_input.m_aIMovement == null)
            {
                return NodeState.FAILURE;
            }

            m_input.m_aIMovement.Stop();            
            return NodeState.SUCCESS;
        }
    }
}
