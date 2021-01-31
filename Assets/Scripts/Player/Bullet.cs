using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] public float _speed;

    [SerializeField] private GameObject _managerObj;
    [SerializeField] private BulletPool _manager;
    [SerializeField] private Player _player;
    [SerializeField] private Rigidbody2D _rb;

    public Vector2 _moveDir = Vector2.zero;

    private bool _pickup = false;
    private bool _outOfBounds = false;

    private float _timer = 0.0f;

    private CameraFollow _camera;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (tag == "PB") {
            _managerObj = GameObject.FindGameObjectWithTag("Manager");
        }
        else if (tag == "EB") {
            _managerObj = GameObject.FindGameObjectWithTag("EnemyManager");
        }
        _manager = _managerObj.GetComponent<BulletPool>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_outOfBounds)
        {
            _timer += Time.deltaTime;
            if (_timer > 3.0f)
            {
                _timer = 0.0f;
                _outOfBounds = false;
                _pickup = true;
                transform.position = new Vector3(Random.Range(-_camera._xSize/2,
                    _camera._xSize/2), Random.Range(-_camera._ySize/2, _camera._ySize/2));
                transform.rotation = Quaternion.identity;
            }
        } 
    }

    void FixedUpdate()
    {
        if (!_OOB() && _pickup != true)
        {
            _Move();
        }
    }

    private void _Move()
    {
        _rb.AddForce(_moveDir * _speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, 5.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "PB")
        {
            if (collision.gameObject.tag == "Player" && _pickup == true)
            {
                _pickup = false;
                _manager.ResetBullet(gameObject);
                _Reset();
            }
            else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall")
            {
                transform.position = new Vector3(_camera._xSize, _camera._ySize, 0.0f);
                _rb.velocity = Vector2.zero;
                _outOfBounds = true;
            }
        }
        else if (tag == "EB")
        {
            if (collision.gameObject.tag == "Player")
            {
                _manager.ResetBullet(gameObject);
                _Reset();
            }
        }
    }

    private void _Reset()
    {
        _pickup = false;
    }

    private bool _OOB()
    {
        return (transform.position.x <= -_camera._xSize/2 || transform.position.x >= _camera._xSize /2
            ||transform.position.y <= -_camera._ySize/2 || transform.position.y >= _camera._ySize/2);
    }

    private Vector2 _RandomDir()
    {
        float x = 0.0f;
        float y = 0.0f;

        while (x == 0.0f)
        {
            x = Random.Range(-1.0f, 1.0f);
        }
        while (y == 0.0f)
        {
            y = Random.Range(-1.0f, 1.0f);
        }

        return new Vector2(x, y);
    }
        
}
