using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _power = 1.0f;
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _managerObj;
    [SerializeField] private BulletPool _manager;
    [SerializeField] private Player _player;
    [SerializeField] private Rigidbody2D _rb;

    private bool _slowdown = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _managerObj = GameObject.FindGameObjectWithTag("Manager");
        _manager = _managerObj.GetComponent<BulletPool>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rb.drag < 1000)
        {
            _Move();
        }
    }

    private void _Move()
    {
        _rb.AddForce(transform.up * _speed, ForceMode2D.Impulse);
        _rb.drag += 5.0f;
    }

}
