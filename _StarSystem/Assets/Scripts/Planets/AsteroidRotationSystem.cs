using Unity.Jobs;
using Unity.Collections;
using Unity.Transforms;
using Unity.Burst;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class AsteroidRotationSystem : JobComponentSystem
{
    [BurstCompile]
    private struct AsteroidControlJob : IJobForEach<AsteroidRotationData, Rotation, Translation>
    {
        public float xMinRange;
        public float xMaxRange;
        public float yMinRange;
        public float yMaxRange;
        public float zMinRange;
        public float zMaxRange;
        public float deltaTime;

        public void Execute(ref AsteroidRotationData AsteroidData, ref Rotation AsteroidRotation, ref Translation AsteroidTranslation)
        {
            Quaternion rotation = AsteroidRotation.Value;
            rotation = AsteroidRotation.Value * Quaternion.AngleAxis(180 * AsteroidData.rotateSpeed * deltaTime, AsteroidData.rotationDirection);
            AsteroidRotation.Value = rotation;
            AsteroidTranslation.Value.z += AsteroidData.moveSpeed * deltaTime;
            if (AsteroidTranslation.Value.z > 70f)
            {
                AsteroidData.moveSpeed = -math.abs(AsteroidData.moveSpeed);
            }

            if (AsteroidTranslation.Value.z < -190f)
            {
                AsteroidData.moveSpeed = +math.abs(AsteroidData.moveSpeed);
            }

        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float delta = Time.DeltaTime;

        AsteroidControlJob AsteroidControl = new AsteroidControlJob
        {
            xMinRange = AsteroidSpawner.xMinRange,
            xMaxRange = AsteroidSpawner.xMaxRange,
            yMinRange = AsteroidSpawner.yMinRange,
            yMaxRange = AsteroidSpawner.yMaxRange,
            zMinRange = AsteroidSpawner.zMinRange,
            zMaxRange = AsteroidSpawner.zMaxRange,
            deltaTime = delta,
        };

        JobHandle AsteroidControlHandle = AsteroidControl.Schedule(this, inputDeps);

        return AsteroidControlHandle;
    }
}
