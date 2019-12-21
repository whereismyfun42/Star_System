using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject AsteroidPrefab;
    public int numberToSpawn;
    public readonly Vector3[] rotDirections = { Vector3.up, Vector3.down };
    private float rotationSpeed = 0.2f;
    private float movementSpeed = 0.2f;
    public static float xMinRange = -120.0f;
    public static float xMaxRange = 120f;
    public static float yMinRange = -70.0f;
    public static float yMaxRange = 20.0f;
    public static float zMinRange = -120.0f;
    public static float zMaxRange = 120.0f;

    private void Start()
    {
        EntityManager manager = World.Active.EntityManager;
        NativeArray<Entity> entities = new NativeArray<Entity>(numberToSpawn, Allocator.Temp);
        manager.Instantiate(AsteroidPrefab, entities);

        for (int i = 0; i < numberToSpawn; i++)
        {
            float xPosition = Random.Range(xMinRange, xMaxRange);
            float yPosition = Random.Range(yMinRange, yMaxRange);
            float zPosition = Random.Range(zMinRange, zMaxRange);

            Translation spawnPos = new Translation { Value = new Vector3(xPosition, yPosition, zPosition) };
            Translation spawnPos2 = new Translation { Value = new Vector3(xPosition, yPosition + 0.23f, zPosition) };
            Translation spawnPos3 = new Translation { Value = new Vector3(xPosition, yPosition + 0.23f, zPosition) };
            Rotation spawnRot = new Rotation { Value = Quaternion.identity };

            AsteroidRotationData AsteroidData = new AsteroidRotationData
            {
                rotateSpeed = rotationSpeed,
                rotationDirection = rotDirections[Random.Range(0,2)],
                moveSpeed = movementSpeed
            };

          
            manager.SetComponentData(entities[i], spawnPos);
            
            manager.SetComponentData(entities[i], spawnRot);
            
            manager.SetComponentData(entities[i], AsteroidData);            
           
        }

        entities.Dispose();
    }

   
}
