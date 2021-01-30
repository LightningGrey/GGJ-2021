using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidAI : EnemyAIBase
{
    public float movementDistance = 5.0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        type = EnemyType.Rapid;
        pool.SetSize(poolSize);
    }

    protected override void Move()
    {
        // rapid AI's movement
        // sinusoidal movement in the x axis

        gameObject.transform.position = startPos + new Vector3(Mathf.Sin(Time.time) * movementDistance, 0.0f, 0.0f);
    }

    protected override void Attack()
    {
        // rapid AI's attack
        shotTimer += Time.deltaTime;

        if (shotTimer >= timeBetweenShots)
        {
            // shoot

            shotTimer = 0;
        }
    }
}
