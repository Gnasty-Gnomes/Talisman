using AISystem.BehaviourTrees;

namespace AISystem.Behaviours.StandardNodes
{
    public class SelectorNode : CompositeNode
    {
        int m_currentIndex = 0;

        protected override NodeState Update(float dt)
        {
            for (int i = m_currentIndex; i < m_children.Length; ++i)
            {
                var state = m_children[i].Evaluate(dt);
                if (state == NodeState.FAILURE)
                {
                    continue;
                }
                return state;
            }                
            return NodeState.FAILURE;
        }

        protected override void BeginNode()
        {
            base.BeginNode();
            m_currentIndex = 0;
        }
    }
}
