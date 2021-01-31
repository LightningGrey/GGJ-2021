using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    private GameObject _bulletClone;
    [SerializeField] private int _maxBullets;

    private Queue<GameObject> _bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        _bulletPool = new Queue<GameObject>();
        _BuildPool();
    }

    private void Init()
    {
        _bulletPool = new Queue<GameObject>();
        _BuildPool();
    }

    private void _BuildPool()
    {

        for (int i = 0; i < _maxBullets; i++)
        {
            _bulletClone = Instantiate(_bullet);
            _bulletClone.transform.SetParent(gameObject.transform);
            _bulletClone.SetActive(false);
            _bulletPool.Enqueue(_bulletClone);
        }

    }

    public GameObject GetBullet()
    {
        _bullet = _bulletPool.Dequeue();
        _bullet.SetActive(true);
        return _bullet;
    }

    public void ResetBullet(GameObject _bullet)
    {
        _bullet.SetActive(false);
        _bulletPool.Enqueue(_bullet);
    }

    public bool isEmpty()
    {
        return (_bulletPool.Count <= 0);
    }

}
