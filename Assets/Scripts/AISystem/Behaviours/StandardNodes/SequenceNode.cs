using System.Threading.Tasks;
using AISystem.BehaviourTrees;

namespace AISystem.Behaviours.StandardNodes
{
    public class SequenceNode : CompositeNode
    {
        int m_currentIndex = 0;

        protected override NodeState Update(float dt)
        {
            for (int i = m_currentIndex; i < m_children.Length; ++i)
            {
                var state = m_children[i].Evaluate(dt);
                if (state == NodeState.SUCCESS)
                {                    
                    ++m_currentIndex;
                    continue;
                }                
                return state;
            }
            return NodeState.SUCCESS;
        }

        protected override void BeginNode()
        {
            base.BeginNode();
            m_currentIndex = 0;
        }
    }
}
