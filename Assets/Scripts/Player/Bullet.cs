using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _power = 1.0f;
    [SerializeField] public float _speed;

    [SerializeField] private GameObject _managerObj;
    [SerializeField] private BulletPool _manager;
    [SerializeField] private Player _player;
    [SerializeField] private Rigidbody2D _rb;

    public Vector2 _moveDir = Vector2.zero;
    private float _baseDrag;
    private bool _rebound = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _baseDrag = _rb.drag;

        if (tag == "PB") {
            _managerObj = GameObject.FindGameObjectWithTag("Manager");
        }
        else if (tag == "EB") {
            _managerObj = GameObject.FindGameObjectWithTag("EnemyManager");
        }
        _manager = _managerObj.GetComponent<BulletPool>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rb.drag < 100 && !OOB())
        {
            _Move();
        }
    }

    private void _Move()
    {
        _rb.AddForce(_moveDir * _speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        if (_rebound)
        {
          _rb.drag += 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "PB") {
            if (collision.gameObject.tag == "Player" && _rebound == true) {
                _manager.ResetBullet(gameObject);
                Reset();
            }
            else if (collision.gameObject.tag == "Enemy") {
                _moveDir = RandomDir().normalized;
                Debug.Log(_moveDir);
                _rebound = true;

            }
            else if (collision.gameObject.tag == "Wall") {
                transform.position = new Vector3(11.0f, 11.0f, 0.0f);
                _rb.velocity = Vector2.zero;
            }
        }
        else if (tag == "EB") {
            if (collision.gameObject.tag == "Player") {
                _manager.ResetBullet(gameObject);
                Reset();
            }
        }
    }

    private void Reset()
    {
        _rb.drag = _baseDrag;
        _rebound = false;
    }

    private bool OOB()
    {
        return (transform.position.x >= 10.0f || transform.position.x <= -10.0f ||
            transform.position.y <= -10.0f || transform.position.y >= 10.0f);
    }

    private Vector2 RandomDir()
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
