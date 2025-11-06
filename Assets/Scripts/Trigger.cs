using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Trigger : MonoBehaviour
{
    private playerController _PlayerController;
    private GameManager _GameManager;
    private CircleSpawn circleSpawn;
    private void Start()
    {
        _PlayerController = GameObject.Find("Player").GetComponent<playerController>();
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     circleSpawn = GameObject.FindWithTag("Target").GetComponent<CircleSpawn>();
        //     // circleSpawn.SpawnNextLevel();
        //     _GameManager.Spawn();
        // }
        
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Player") && _PlayerController.count != 0)
        {
            // _PlayerController.Collected = false;
            _GameManager.Spawn();
            Debug.Log(_PlayerController.count);
            
        }

        if (other.gameObject.CompareTag("Player") && !_PlayerController.Collected && _PlayerController.count != 0 )
        {
            _GameManager.Spawn();
            _PlayerController.count--;
            Debug.Log(_PlayerController.count);
            circleSpawn = GameObject.Find("Target_" + _PlayerController.circlesCount + 1 + "(Clone)").GetComponent<CircleSpawn>();
            circleSpawn.SpawnNextLevel();
            Debug.Log(circleSpawn);
        }

        if (other.gameObject.CompareTag("Player") && _PlayerController.count == 0)
        {
            Debug.Log(_PlayerController.count);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
