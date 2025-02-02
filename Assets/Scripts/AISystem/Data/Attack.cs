using UnityEngine;

namespace AISystem.Data
{
    [CreateAssetMenu(menuName = "Talisman/Attack")]
    public class Attack : ScriptableObject
    {
        public string m_attackHandle;
        public float m_attackDistance;
        public float m_attackDuration;
        public bool m_disableRootMotion;
        public FMODUnity.EventReference m_audioReference;
    }
}