using AISystem.BehaviourTrees;

namespace AISystem
{
    public class BehaviourManager
    {
        BehaviourTree m_behaviourTree;

        public BehaviourManager(BehaviourTree bt)
        {
            m_behaviourTree = bt;
        }

        public void Update(float dt)
        {
            m_behaviourTree.Update(dt);
        }
    }
}
