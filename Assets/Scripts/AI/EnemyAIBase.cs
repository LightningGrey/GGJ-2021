using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Rapid, Shotgun, Sniper };

public class EnemyAIBase : MonoBehaviour
{
    public EnemyType type;
    protected BulletPool pool;
    public int poolSize;
    public float speed;
    public float timeBetweenShots;
    protected float shotTimer = 0;
    protected Vector3 startPos;
    public Rigidbody2D _rb;


    protected virtual void Start()
    {
        pool = new BulletPool();
        startPos = gameObject.transform.position;
    }

    protected virtual void Move()
    {
        gameObject.SetActive(false);
        throw new System.Exception("Move function not overriden, disabling gameObject.");
    }

    protected virtual void Attack()
    {
        gameObject.SetActive(false);
        throw new System.Exception("Attack function not overriden");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
        Attack();
    }
}
