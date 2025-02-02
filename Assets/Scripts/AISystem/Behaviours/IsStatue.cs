using AISystem.Data;
using AISystem.BehaviourTrees;

namespace AISystem.Behaviours
{
    public class IsStatue : ConditionalNode
    {
        protected override bool Compare(float dt)
        {
            if (m_input.m_aIMovement.m_isStatue)
            {                
                return true;
            }            
            return false;
        }
    }
}
