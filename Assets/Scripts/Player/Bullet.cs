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

    public Vector2 moveDir = Vector2.zero;

    public bool pickup = false;
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
            if (tag == "PB")
            {
                _timer += Time.deltaTime;
                if (_timer > 3.0f)
                {
                    _timer = 0.0f;
                    _outOfBounds = false;
                    pickup = true;
                    transform.position = new Vector3(Random.Range(-_camera._xSize / 2,
                        _camera._xSize / 2), Random.Range(-_camera._ySize / 2, _camera._ySize / 2));
                    transform.rotation = Quaternion.identity;
                }
            //} else if (tag == "EB")
            //{
            //    _manager.ResetBullet(gameObject);
            }
        } 
    }

    void FixedUpdate()
    {
        if (!_outOfBounds && !pickup)
        {
            _Move();
        }
    }

    private void _Move()
    {
        _rb.AddForce(moveDir * _speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, 5.5f);
        if (!_outOfBounds && !_InBounds())
        {
            if (tag == "PB") {
                _outOfBounds = true;
                transform.position = new Vector3(_camera._xSize, _camera._ySize, 0.0f);
                _rb.velocity = Vector2.zero;
            } else
            {
                _manager.ResetBullet(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "PB")
        {
            if (collision.gameObject.tag == "Player" && pickup == true)
            {
                pickup = false;
                _manager.ResetBullet(gameObject);
                _Reset();
            }
            else if (collision.gameObject.tag == "Enemy" && pickup == false)
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
                if (collision.gameObject.GetComponent<Player>().alive == false)
                {
                    _manager.ResetBullet(gameObject);
                }
                else if (collision.gameObject.GetComponent<Player>().isDodging == false)
                {
                    collision.gameObject.GetComponent<Player>().OnHit();
                    _manager.ResetBullet(gameObject);
                }
            }
        }
    }

    private void _Reset()
    {
        pickup = false;
    }

    private bool _InBounds()
    {
        return (transform.position.x >= -_camera._xSize/2 && transform.position.x <= _camera._xSize/2
            && transform.position.y >= -_camera._ySize/2 && transform.position.y <= _camera._ySize/2);
    }

        
}
