namespace TSPE
{
    using System.Collections.Generic;
    using TSPE.Utils;

    public class Manager
    {
        private readonly IList<Entity> entities;

        public Manager()
        {
            entities = new List<Entity>();
        }

        public void AddEntity(
            double mass,
            Vector inertiaTensor,
            Quaternion inertiaTensorRotation,
            Vector velocity,
            Vector position,
            Vector angularVelocity,
            Quaternion rotation
        )
        {
            Entity entity = new Entity(
                this,
                mass,
                inertiaTensor,
                inertiaTensorRotation,
                velocity,
                position,
                angularVelocity,
                rotation
            );
        }
    }
}
