using AISystem.Data;

namespace AISystem.Sensors
{
    public interface IOptic
    {
        event System.Action<Observation> m_observations;

        void StartOptics();
        void StopOptics();
    }
}
   

   

   

