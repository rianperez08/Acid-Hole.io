using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    public float minScale = 1.0f;
    public float maxScale = 100.0f;
    public float largeScaleProbability = 0.1f;
    public AnimationCurve scaleDistribution;
    public int numObjectsToSpawn = 3;

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < numObjectsToSpawn; i++)
        {
            // Choose a random object to spawn
            int index = Random.Range(0, objectsToSpawn.Count);
            GameObject prefab = objectsToSpawn[index];

            // Get the bounds of the box collider
            BoxCollider collider = GetComponent<BoxCollider>();
            Vector3 center = collider.center + transform.position;
            Vector3 size = collider.size;
            Vector3 minBounds = center - size / 2.0f;
            Vector3 maxBounds = center + size / 2.0f;

            // Generate a random point on the surface of the collider
            Vector3 position = new Vector3(Random.Range(minBounds.x, maxBounds.x), maxBounds.y, Random.Range(minBounds.z, maxBounds.z));

            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            float scale = GenerateRandomScale();

            // Spawn the object
            GameObject obj = Instantiate(prefab, position, rotation);
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private float GenerateRandomScale()
    {
        float t = Random.value;
        float scale = Mathf.Lerp(minScale, maxScale, t);

        if (Random.value < largeScaleProbability)
        {
            scale *= 1.55f;
        }

        float distributionValue = scaleDistribution.Evaluate(t);
        scale *= distributionValue;

        return scale;
    }
}