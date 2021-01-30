using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAI : EnemyAIBase
{
    public float movementDistance = 5.0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        type = EnemyType.Shotgun;
        pool.SetSize(poolSize);
    }

    protected override void Move()
    {
        // shotgun AI's movement
        // sinusoidal movement in the y axis

        gameObject.transform.position = startPos + new Vector3(0.0f, Mathf.Sin(Time.time * speed) * movementDistance, 0.0f);
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
}
