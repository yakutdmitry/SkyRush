using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class CircleSpawn : MonoBehaviour
{
    public GameObject Instance;
    public List<GameObject> Levels = new List<GameObject>();
    public InputActionReference spawnAction;
    
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = 0f;
    public float maxY = 30f;
    public float zOffset = 100f;

    private int circlesSpawned = 1;
    private int spawnLimit = 10;
    private int targetIndex = 0;

    private void Update()
    {
        if (circlesSpawned == spawnLimit)
        {
            Destroy(Levels[targetIndex]);
            targetIndex++;
            circlesSpawned--;
        }
    }

	

    public void SpawnNextLevel(InputAction.CallbackContext context)
    {
        Vector3 basePosition;

        if (Levels.Count == 0)
        {
            basePosition = Vector3.zero;
        }
        else
        {
            basePosition = Levels[Levels.Count - 1].transform.position;
        }

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float newZ = basePosition.z + zOffset;

        Vector3 spawnPosition = new Vector3(randomX, randomY, newZ);

        Levels.Add(Instantiate(Instance, spawnPosition, Quaternion.identity));
        

        circlesSpawned++;
    }
    
    private void OnEnable()
    {
        spawnAction.action.started += SpawnNextLevel;
    }

    private void OnDisable()
    {
        spawnAction.action.started -= SpawnNextLevel;
    }
    
    
    
}