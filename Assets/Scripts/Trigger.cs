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
        if (other.gameObject.CompareTag("Player"))
        {
            _PlayerController.count -= 1f;
            
        }
        
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Player") && _PlayerController.count != 0 && _PlayerController.Collected)
        {
            _PlayerController.Collected = false;
            Debug.Log(_PlayerController.count);
            
            Debug.Log("AAAAA");
        }

        else if(_PlayerController.count % 2 == 0)
        {
            Debug.Log("BBBBBB");
            _GameManager.Spawn();
            Debug.Log(_PlayerController.count);
            circleSpawn = GameObject.FindWithTag("Target").GetComponent<CircleSpawn>();
            circleSpawn.SpawnNextLevel();
            Debug.Log(circleSpawn);
            
        }

        if (other.gameObject.CompareTag("Player") && _PlayerController.count <= 0)
        {
            Debug.Log(_PlayerController.count);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
