using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class CircleSpawn : MonoBehaviour
{
    public playerController _PlayerController;
    public GameObject Instance;
    public static List<GameObject> Levels = new List<GameObject>();
    

    
    // [SerializeField] private Collider Collider;
    
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = 0f;
    public float maxY = 30f;
    public float zOffset = 100f;

    private int circlesSpawned = 1;
    private int spawnLimit = 10;
    private int targetIndex = 0;
    

    private void Start()
    {
        _PlayerController = GameObject.Find("Player").GetComponent<playerController>();
        
        if (!Levels.Contains(gameObject))
        {
            Levels.Add(gameObject);
            gameObject.name = "Target_0"; 
        }

    }

    private void Update()
    {
        _PlayerController.circlesCount = circlesSpawned;
        if (circlesSpawned == spawnLimit)
        {
            Destroy(Levels[targetIndex]);
            targetIndex++;
            circlesSpawned--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _PlayerController.Collected = true;
            Debug.Log("Player Detected");
            
            if (_PlayerController.count >= 5)
            {
                _PlayerController.count = 7;
            }

            if (_PlayerController.count < 5)
            {
                _PlayerController.count += 1.5f;
            }
            
            SpawnNextLevel();
        }
    }

    public void SpawnNextLevel()//(InputAction.CallbackContext context)
    {
        Vector3 basePosition;

        if (Levels.Count == 0)
        {
            basePosition = Vector3.zero;
        }
        else
        {
            basePosition = Levels[Levels.Count -1].transform.position;
        }

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float newZ = basePosition.z + zOffset;

        Vector3 spawnPosition = new Vector3(randomX, randomY, newZ);
        GameObject newInstance = Instantiate(Instance, spawnPosition, Quaternion.identity);
        newInstance.name = "Target_" + circlesSpawned ;
        Levels.Add(Instantiate(newInstance));
        Destroy(Levels[Levels.Count - 2]);
        
        

        circlesSpawned++;
    }
    
    // private void OnEnable()
    // {
    //     spawnAction.action.started += SpawnNextLevel;
    // }
    //
    // private void OnDisable()
    // {
    //     spawnAction.action.started -= SpawnNextLevel;
    // }
    //
    //
    
}