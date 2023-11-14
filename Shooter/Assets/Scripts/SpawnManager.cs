using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private GameObject _speedPowerupPrefab;

    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private bool _stopSpawning = false;

    public static int count;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine(SpawnTripleShotPowerup());
        //StartCoroutine(SpawnSpeedPowerup());
        StartCoroutine(SpawnPowerup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator SpawnPowerup()
    {
        while (_stopSpawning == false)
        {
            int random = Random.Range(0, 2);
            Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
            Instantiate(_powerups[random], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }
    //IEnumerator SpawnTripleShotPowerup()
    //{
    //    while (_stopSpawning == false)
    //    {
    //        Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
    //        Instantiate(_tripleShotPowerupPrefab, posToSpawn, Quaternion.identity);
    //        yield return new WaitForSeconds(Random.Range(3f, 7f));

    //    }
    //}
    //IEnumerator SpawnSpeedPowerup()
    //{
    //    while (_stopSpawning == false)
    //    {
    //        Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
    //        Instantiate(_speedPowerupPrefab, posToSpawn, Quaternion.identity);
    //        yield return new WaitForSeconds(Random.Range(5f, 10f));
    //    }
    //}
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        var obj = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in obj)
        {
            Destroy(enemy);
        }
    }
}
