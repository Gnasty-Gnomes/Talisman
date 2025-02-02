using UnityEngine;
using AISystem.Systems;

namespace AISystem.BehaviourTrees
{
    public partial class BehaviourInput
    {
        public GameObject m_go;
        public AIKnowledge m_aIKnowledge;
        public AIMovement m_aIMovement;

        public Blackboard m_blackboard = new Blackboard();
    }

}