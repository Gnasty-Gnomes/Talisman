using AISystem.BehaviourTrees;

namespace AISystem.Behaviours.StandardNodes
{
    public class InverterNode : DecoratorNode
    {
        protected override NodeState Update(float dt)
        {
            var state = m_child.Evaluate(dt);
            if (state == NodeState.RUNNING)
            {
                return NodeState.RUNNING;
            }
            return state == NodeState.SUCCESS ? NodeState.FAILURE : NodeState.SUCCESS;
        }
    }
}
