using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public playerController _PlayerController;
    public GameObject Instance;
    public List<GameObject> Levels;
    
    public Vector3 offset = new Vector3(10, 0, 0);
    private int firstLevelIndex = 0;
    [SerializeField] private int numOfLevels; 
    private int spawnLimit = 10;
    public CircleSpawn CircleSpawn;
    
    private void Update()
    {
        if (numOfLevels == spawnLimit)
        {
            numOfLevels--;
            Destroy(Levels[firstLevelIndex]);
            firstLevelIndex++;
        }
        
        
    }

    public void Spawn()
    {
        
        Debug.Log("Spawn");
        Vector3 SpawnPos = Levels[Levels.Count - 1].transform.position + offset;
        Levels.Add(Instantiate(Instance, SpawnPos, Quaternion.identity));
        numOfLevels++;
    }
}

