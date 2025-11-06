using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Trigger : MonoBehaviour
{
    private playerController _PlayerController;
    private GameManager _GameManager;

    private void Start()
    {
        _PlayerController = GameObject.Find("Player").GetComponent<playerController>();
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Player") && _PlayerController.Collected)
        {
            // _PlayerController.Collected = false;
            _GameManager.Spawn();
        }

        if (other.gameObject.CompareTag("Player") && !_PlayerController.Collected)
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log(_PlayerController.Collected);
        }
    }
}
