using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _buffSpeed = 2.0f;
    [SerializeField]
    private GameObject _lazerPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShot = false;
    [SerializeField]
    private bool _isBuffSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void CalculateMovement()
    {
        _spawnManager.count = 1;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShot == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_lazerPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }
    public void Damage()
    {
        _lives --;
        if (_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        _isTripleShot = true;
        StartCoroutine(TripleShotPowerupDownRoutine());
    }
    IEnumerator TripleShotPowerupDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShot = false;
    }
    public void BuffSpeedActive()
    {
        _isBuffSpeed = true;
        _speed = _speed * _buffSpeed;
        StartCoroutine(SpeedPowerupDownRoutine());
    }
    IEnumerator SpeedPowerupDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _speed = _speed / _buffSpeed;
        _isBuffSpeed = false;
    }
}
