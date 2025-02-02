namespace AISystem.BehaviourTrees
{
    [System.Serializable]
    public abstract class Node
    {
        public NodeState m_state { get; set; }

        protected BehaviourInput m_input;

        public void SetBehaviourInput(BehaviourInput input) => m_input = input;

        public NodeState Evaluate(float dt)
        {
            if (m_state != NodeState.RUNNING)
            {
                BeginNode();
            }

            m_state = Update(dt);
            return m_state;
        }

        protected virtual void BeginNode() { }
        protected abstract NodeState Update(float dt);
        public virtual void EndNode() { }
    }
}
