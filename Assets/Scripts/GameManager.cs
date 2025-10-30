using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject Instance;
    public List<GameObject> Levels;
    
    public InputActionReference spawnAction;
    public Vector3 offset = new Vector3(10, 0, 0);
    [SerializeField] private int firstLevelIndex = 0;
    [SerializeField] private int numOfLevels;
    [SerializeField] private int spawnLimit = 10;
    private void Update()
    {
        if (numOfLevels == spawnLimit)
        {
            numOfLevels--;
            Destroy(Levels[firstLevelIndex]);
            firstLevelIndex++;
        }
    }

    private void OnEnable()
    {
        spawnAction.action.started += Spawn;
    }

    private void OnDisable()
    {
        spawnAction.action.started -= Spawn;
    }

    private void Spawn(InputAction.CallbackContext context)
    {
        Debug.Log("Spawn");
        Vector3 SpawnPos = Levels[Levels.Count - 1].transform.position + offset;
        Levels.Add(Instantiate(Instance, SpawnPos, Quaternion.identity));
        numOfLevels++;
    }
}

