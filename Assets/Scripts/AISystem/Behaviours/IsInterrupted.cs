using AISystem.Data;
using AISystem.BehaviourTrees;

namespace AISystem.Behaviours
{
    public class IsInterrupted : ConditionalNode
    {
        protected override bool Compare(float dt)
        {
            if (m_input.m_aIMovement.m_isInterrupted)
            {
                return true;
            }            
            return false;
        }
    }
}
