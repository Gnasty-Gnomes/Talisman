using System.Collections.Generic;
using UnityEngine;
using AISystem.Contracts;
using AISystem.Data;

namespace AISystem.Systems
{
    public class AIKnowledge
    {
        readonly Dictionary<IBeing, BeingKnowledge> m_beingKnowledge = new();

        List<IBeing> m_recentlySeen = new List<IBeing>();

        public void ProcessObservations(Observation observation)
        {
            Vector3 estimatedVelocity = Vector3.zero;
            if (m_beingKnowledge.TryGetValue(observation.m_being, out var beingKnowledge))
            {
                estimatedVelocity = observation.m_position - beingKnowledge.m_lastKnownPosition;
            }

            m_beingKnowledge[observation.m_being] = new BeingKnowledge()
            {
                m_being = observation.m_being,
                m_lastKnownPosition = observation.m_position,
                m_estimatedVelocity = estimatedVelocity,
                m_lastUpdated = Time.time,
            };

            if (m_recentlySeen.Contains(observation.m_being) == false)
            {
                m_recentlySeen.Add(observation.m_being);
            }
        }

        public bool CanSeeBeing(out BeingKnowledge beingKnowledge)
        {
            if (m_recentlySeen.Count > 0)
            {
                beingKnowledge = m_beingKnowledge[m_recentlySeen[0]];
                return true;
            }
            beingKnowledge = default;
            return false;
        }

        public bool CanSeePlayer()
        {
            return m_recentlySeen.Count > 0;
        }
    }
}