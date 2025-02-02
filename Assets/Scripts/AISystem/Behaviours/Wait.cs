using UnityEngine;
using AISystem.BehaviourTrees;

namespace AISystem.Behaviours
{
    public class Wait : ActionNode
    {
        public float m_duration = 1f;
        float m_startTime;

        protected override void BeginNode()
        {
            base.BeginNode();
            m_startTime = Time.time;
        }

        protected override NodeState Update(float dt)
        {
            if (Time.time - m_startTime >= m_duration)
            {
                return NodeState.SUCCESS;
            }

            return NodeState.RUNNING;
        }
    }
}
