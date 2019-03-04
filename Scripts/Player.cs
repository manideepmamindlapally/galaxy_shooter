using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _normalspeed = 5.0f;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _fireNext = 0.0f;
    [SerializeField]
    private float _tripleShootTime = 10.0f;
    [SerializeField]
    private float _speedBoostTime = 10.0f;
    [SerializeField]
    private float _shieldTime = 10f;

    [SerializeField]
    private GameObject _laserPreFab;
    [SerializeField]
    private GameObject _tripleLaserPreFab;
    [SerializeField]
    private GameObject _playerExplosionPreFab;
    [SerializeField]
    private GameObject _shieldAnimation;

    private GameManager _gameManager;
    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserShootAudio;

    [SerializeField]
    private bool _canTripleShoot = false;
    [SerializeField]
    private bool _canSpeedBoost = false;
    [SerializeField]
    private bool _canUseShield = false;
    [SerializeField]
    private GameObject[] _engineFailure;

    public bool hitEnemy = false;

    private int _hitCount = 0;

    [SerializeField]
    private int lives = 3;

    

	// Use this for initialization
	void Start () {
        //set the player at (0,0,0)
        transform.position = new Vector3(0, 0, 0);
        //Access Game Manager
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //access UI Manager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.UpdateLives(lives);
    }
	
	// Update is called once per frame
	void Update () {
        //moving the player
        Movement();

        //shoot a laser when space key pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) Laser();

        //check Enemy Collisions
        CheckEnemyCollision();
    }
    
    private void Movement()
    {
        //include speedboost function
        _speed = _normalspeed;
        if (_canSpeedBoost) _speed *= 2f;

        //move the player
        float directionX = Input.GetAxis("Horizontal");
        float directionY = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * directionX * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * directionY * _speed * Time.deltaTime);

        //restrict movement in Y
        if (transform.position.y > 0) transform.position = new Vector3(transform.position.x, 0, 0);
        else if (transform.position.y < -4.2f) transform.position = new Vector3(transform.position.x, -4.2f, 0);

        //warp in x plane
        if (transform.position.x > 9.4f) transform.position = new Vector3(-9.4f, transform.position.y, 0);
        if (transform.position.x < -9.4f) transform.position = new Vector3(9.4f, transform.position.y, 0);
    } 

    private void Laser()
    {
        if(Time.time > _fireNext)
        {
            //play laser audio
            AudioSource.PlayClipAtPoint(_laserShootAudio, Camera.main.transform.position);
            //check triple shoot
            if (_canTripleShoot)
                Instantiate(_tripleLaserPreFab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            //normal shoot
            else
                Instantiate(_laserPreFab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            _fireNext = Time.time + _fireRate;
        }
    }

    //check enemycollisions
    private void CheckEnemyCollision()
    {
        //when enemy is hit
        if (hitEnemy)
        {
            if(_canUseShield)
            {
                _shieldAnimation.SetActive(false);
                _canUseShield = false;
                hitEnemy = false;
            }
            else
            {
                _hitCount++;

                if(_hitCount == 1)
                {
                    int random = Random.Range(0, 2);
                    _engineFailure[random].SetActive(true);
                }
                else if(_hitCount == 2)
                {
                    foreach(GameObject engine in _engineFailure)
                    {
                        engine.SetActive(true);
                    }
                }

                lives--;
                hitEnemy = false;
                _uiManager.UpdateLives(lives);

                //lives  check
                if (lives <= 0)
                {
                    //instantiate animation
                    Instantiate(_playerExplosionPreFab, transform.position, Quaternion.identity);
                    //tell the UIManager that the player died
                    _gameManager.EndGame();
                    //destroy
                    Destroy(this.gameObject);
                }
            }
        }
    }

    //enable triple shoot for the player
    public void TripleShootPowerUpOn()
    {
        _canTripleShoot = true;
        //start coroutine
        StartCoroutine(TripleShootPowerDown());
    }

    //enable speed boost
    public void SpeedBoostOn()
    {
        _canSpeedBoost = true;
        //start coroutine
        StartCoroutine(SpeedBoostDown());
    }

    //enable shield
    public void ShieldOn()
    {
        _canUseShield = true;
        _shieldAnimation.SetActive(true);
        StartCoroutine(ShieldDown());
    }

    //powerdown triple shoot
    public IEnumerator TripleShootPowerDown()
    {
        yield return new WaitForSeconds(_tripleShootTime);
        _canTripleShoot = false;
    }

    //powerdown speed boost
    public IEnumerator SpeedBoostDown()
    {
        yield return new WaitForSeconds(_speedBoostTime);
        _canSpeedBoost = false;
    }

    //power down shield
    public IEnumerator ShieldDown()
    {
        yield return new WaitForSeconds(_shieldTime);
        _shieldAnimation.SetActive(false);
        _canUseShield = false;
    }
}
