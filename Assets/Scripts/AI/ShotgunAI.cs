using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAI : EnemyAIBase
{
    private Vector2 _moveVec;
    public float timeBetweenMoves = 5;
    private float moveTimer = 0;
    private bool moveDir = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        type = EnemyType.Shotgun;
    }

    protected override void Move()
    {
        _moveVec.x = 0.0f;
        if (moveDir)
        {
            _moveVec.y = 1.0f;
        }
        else
        {
            _moveVec.y = -1.0f;
        }


        _moveVec.Normalize();

        _rb.AddForce(_moveVec * speed, ForceMode2D.Impulse);

        moveDir = !moveDir;
    }

    protected override void Attack()
    {
        // shotgun AI's attack
        shotTimer += Time.deltaTime;

        if (shotTimer >= timeBetweenShots)
        {
            _animator.SetBool("Attack", true);
            GameObject _newBullet;

            var offset = -60.0f;
            Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            for (int i = 0; i < 5; i++)
            {
                _newBullet = pool.GetBullet();
                _newBullet.transform.position = this.transform.position;
                _newBullet.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
                _newBullet.GetComponent<Bullet>().moveDir = _newBullet.transform.up;
                _newBullet.GetComponent<Bullet>()._speed = bulletSpeed;
                offset -= 15.0f;
            }

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

