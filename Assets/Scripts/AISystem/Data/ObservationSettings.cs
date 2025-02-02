using UnityEngine;

namespace AISystem.Data
{
    [System.Serializable]
    public class ObservationSettings : SensorSettings
    {
        public float m_viewDistance = 20f;
        public LayerMask m_opticMask = int.MaxValue;
        public float m_hAngle = 90f;
        public float m_vAngle = 100f;
    }
}
