using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAI : EnemyAIBase
{
    private Vector2 _moveVec;
    public float timeBetweenMoves = 5;
    private float moveTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        type = EnemyType.Sniper;
        pool.SetSize(poolSize);
        _moveVec = Vector2.zero;
        _rb.gravityScale = 0;
    }

    protected override void Move()
    {
        // sniper AI's movement
        // every x amount of time, pick a random angle and lerp towards it

        //pick random movevec
        _moveVec.x = Random.Range(-1.0f, 1.0f);
        _moveVec.y = Random.Range(-1.0f, 1.0f);

        Debug.Log(_moveVec.ToString());
        _moveVec.Normalize();

        _rb.AddForce(_moveVec * speed, ForceMode2D.Impulse);
    }

    protected override void Attack()
    {
        // shotgun AI's attack
        shotTimer += Time.deltaTime;

        if (shotTimer >= timeBetweenShots)
        {
            // shoot

            shotTimer = 0;
        }
    }

    protected override void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        moveTimer += Time.fixedDeltaTime;

        // friction
        _rb.AddForce(-_rb.velocity * 1.1f * Time.fixedDeltaTime, ForceMode2D.Impulse);

        if (moveTimer >= timeBetweenMoves)
        {
            _rb.velocity = Vector2.zero;
            Move();
            moveTimer = 0;
        }

        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, 5.0f);
    }
}
