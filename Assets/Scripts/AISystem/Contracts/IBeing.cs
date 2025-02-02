using UnityEngine;

namespace AISystem.Contracts
{
    public interface IBeing
    {
        Vector3 m_position { get; }
        Vector3 m_forward { get; }
        Vector3 m_headPosition { get; }
    }
}
