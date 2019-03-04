using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerUpPrefab;
    [SerializeField]
    private GameObject _playerPrefab;

    private GameObject[] _balanceEnemies;

    [SerializeField]
    private float _enemySpawnInterval = 5f;
    [SerializeField]
    private float _powerUpSpawnInterval = 15f;

    private Coroutine _lastEnemySpawn = null;
    private Coroutine _lastPowerUpSpawn = null;
    
    private bool _isGameOn = false;

    private IEnumerator EnemySpawnWait()
    {
        while (_isGameOn)
        {
            Instantiate(_enemyPrefab, new Vector3(Random.Range(-8.5f, 8.5f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(_enemySpawnInterval);
        }
    }

    private IEnumerator PowerUpSpawnWait()
    {
        while (_isGameOn)
        {
            int randomPowerUpId = Random.Range(0, 3);
            float randomInterval = Random.Range(_powerUpSpawnInterval*0.8f, _powerUpSpawnInterval*1.2f);
            Instantiate(_powerUpPrefab[randomPowerUpId], new Vector3(Random.Range(-8.5f, 8.5f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    //initialize a new game
    public void InitGame()
    {
        //turn the game on
        _isGameOn = true;
        //spawn a player
        Instantiate(_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //initiate spawnning enimies and powerups
        //store these routines
        _lastEnemySpawn = StartCoroutine(EnemySpawnWait());
        _lastPowerUpSpawn = StartCoroutine(PowerUpSpawnWait());
    }

    //end a current game
    public void EndGame()
    {
        //turn the game off
        _isGameOn = false;
        //stop the existing coroutines
        StopCoroutine(_lastEnemySpawn);
        StopCoroutine(_lastPowerUpSpawn);
        //find the existing enemies
        _balanceEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        //destroy them
        foreach (GameObject enemy in _balanceEnemies) Destroy(enemy.gameObject);
    }
}
