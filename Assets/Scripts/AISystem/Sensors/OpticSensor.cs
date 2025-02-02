using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using AISystem.Contracts;
using AISystem.Data;

namespace AISystem.Sensors
{
    public class OpticSensor : IOptic
    {
        public event Action<Observation> m_observations;

        ObservationSettings m_observationSettings;
        IManager m_aiManager;
        IBeing m_attachedBeing;
        bool m_activeOptics;

        public OpticSensor(ObservationSettings settings, IManager aIManager, IBeing attachedBeing)
        {
            m_observationSettings = settings;
            m_aiManager = aIManager;
            m_attachedBeing = attachedBeing;
        }

        public void StartOptics()
        {
            m_activeOptics = true;
            OpticsLoop().Forget();
        }

        public void StopOptics()
        {
            m_activeOptics = false;
        }

        private async UniTask OpticsLoop()
        {
            while (m_activeOptics)
            {
                // Precalculate and cache detection values
                float hCos = Mathf.Cos(m_observationSettings.m_hAngle * Mathf.Deg2Rad / 2f);
                float vCos = Mathf.Cos(m_observationSettings.m_vAngle * Mathf.Deg2Rad / 2f);
                Vector3 pos = m_attachedBeing.m_headPosition;

                Vector3 facing = m_attachedBeing.m_forward;
                Vector2 hFacing = new Vector2(facing.x, facing.z).normalized;
                Vector2 vFacing = new Vector2(facing.z, facing.y).normalized;

                IBeing[] nearbyEntities = m_aiManager.GetCloseBeings(m_observationSettings.m_viewDistance, pos);

                foreach (IBeing being in nearbyEntities)
                {
                    // Ignore ourselves
                    if (being == m_attachedBeing) continue;

                    // Check horizontal and vertical angle, and occlusion of this entity.
                    Vector3 offset = being.m_position - pos;
                    if (CheckAngle(new Vector2(offset.x, offset.z), hFacing, hCos) == false ||
                        CheckAngle(new Vector2(offset.z, offset.y), vFacing, vCos) == false ||
                        m_aiManager.CanSeeBeing(being, pos, offset, m_observationSettings.m_viewDistance, m_observationSettings.m_opticMask) == false)
                    {
                        continue;
                    }

                    m_observations?.Invoke(new Observation()
                    {
                        m_position = being.m_position,
                        m_being = being,
                    });
                }

                await UniTask.Yield();
            }
        }

        bool CheckAngle(Vector2 offset, Vector2 facing, float cosAngle)
        {
            return Vector3.Dot(offset.normalized, facing.normalized) >= cosAngle;
        }
    }
}