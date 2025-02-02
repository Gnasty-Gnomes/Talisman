using UnityEngine;

namespace AISystem.Pathing
{
    public struct PathRequest
    {
        public Vector3 m_origin { get; set; }
        public Vector3 m_destination { get; set; }
        public Vector3 m_originDirection { get; set; }
        public Vector3 m_destinationDirection { get; set; }
    }
} 

   

