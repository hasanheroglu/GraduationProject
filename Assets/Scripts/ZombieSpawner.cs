using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private float spawnWaitDuration = 60f; 
        
    private bool _isSpawning;
    private bool _waitForNextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        _isSpawning = true;
        _waitForNextSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSpawning)
        {
            Spawn();
        }
    }

    public void StartSpawning()
    {
        _isSpawning = true;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }

    public void Spawn()
    {
        if (!_waitForNextSpawn)
        {
            CreatureFactory.GetZombie(transform.position);
            StartCoroutine(WaitForNextSpawn());
        }
    }

    private IEnumerator WaitForNextSpawn()
    {
        _waitForNextSpawn = true;
        yield return new WaitForSeconds(spawnWaitDuration);
        _waitForNextSpawn = false;
    }
}
