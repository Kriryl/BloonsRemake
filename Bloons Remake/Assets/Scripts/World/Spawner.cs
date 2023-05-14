using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Bloon defaultBloon;

    public float rate = 0.5f;

    private float nextSpawnTime = 0f;

    private void Start()
    {
        nextSpawnTime = Time.time + (1 / rate);
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Bloon newBloon = Instantiate(defaultBloon, transform.position, transform.rotation);

            newBloon.bloonType = BloonType.Ceramic;
            newBloon.Init();
            nextSpawnTime = Time.time + (1 / rate);
        }
    }
}
