using AISystem.Contracts;
using UnityEngine;

namespace AISystem.Data
{ 
    public struct BeingKnowledge
    {
        public IBeing m_being;
        public Vector3 m_lastKnownPosition;
        public Vector3 m_estimatedVelocity;
        public float m_lastUpdated;
    }
}
