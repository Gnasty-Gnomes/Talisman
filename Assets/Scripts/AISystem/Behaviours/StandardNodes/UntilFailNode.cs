using AISystem.BehaviourTrees;
namespace AISystem.Behaviours.StandardNodes
{
    public class UntilFailNode : DecoratorNode
    {
        protected override NodeState Update(float dt)
        {
            var childState = m_child.Evaluate(dt);
            if (childState == NodeState.FAILURE)
            {
                return NodeState.FAILURE;
            }

            return NodeState.RUNNING;
        }
    }
}
