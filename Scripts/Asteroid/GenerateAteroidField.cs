using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAteroidField : MonoBehaviour
{
    [SerializeField] Transform asteroidPrefab;
    [SerializeField] int fieldRadius = 100;
    [SerializeField] int asteroidCount = 500;

    void Start()
    {
        for (int i = 0; i < asteroidCount; i++)
        {
            Instantiate(asteroidPrefab, Random.insideUnitSphere*fieldRadius, Quaternion.identity);
        }
    }


    void Update()
    {
        
    }
}
