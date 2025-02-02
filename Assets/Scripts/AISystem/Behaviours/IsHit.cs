using AISystem.Data;
using AISystem.BehaviourTrees;

namespace AISystem.Behaviours
{
    public class IsHit : ConditionalNode
    {
        protected override bool Compare(float dt)
        {
            if (m_input.m_aIMovement.m_isHit)
            {
                return true;
            }            
            return false;
        }
    }
}
