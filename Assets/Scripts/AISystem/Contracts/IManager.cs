using UnityEngine;
using AISystem.Pathing;

namespace AISystem.Contracts
{
    public interface IManager
    {
        public void RegisterBeing(IBeing entity);
        public void DeregisterBeing(IBeing entity);

        public IBeing[] GetCloseBeings(float distance, Vector3 position);

        public bool CanSeeBeing(IBeing entity, Vector3 origin, Vector3 direction, float viewDistance,
            LayerMask visionMask);

        public Path GeneratePath(PathRequest request);
    }
}
