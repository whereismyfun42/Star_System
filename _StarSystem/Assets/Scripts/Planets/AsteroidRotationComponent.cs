using System;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

[Serializable]
public struct AsteroidRotationData : IComponentData
{
    public float rotateSpeed;
    public float3 rotationDirection;
    public float moveSpeed;
}

public class AsteroidRotationComponent : ComponentDataProxy<AsteroidRotationData> { }
