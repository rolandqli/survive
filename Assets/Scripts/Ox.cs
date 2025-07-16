using UnityEngine;

public class Ox : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    protected override void Move()
    {
        // Gets direction vector then moves
        Transform initial_position = EnemyRB.transform;
        EnemyRB.linearVelocity = -(initial_position.position - player.position).normalized * 3;

    }
}
