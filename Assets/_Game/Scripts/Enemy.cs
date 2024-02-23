using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Idle, Patrol, Attack, Death} 

public class Enemy : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Collider2D collider;

    private EnemyState enemyState;
    private float time;
    private Player target;

    private void Start()
    {
        ChangeState(EnemyState.Idle);
        ChangedDirect(true);
    }

    private void Update()
    {
        UpdateState(enemyState);
    }

    public void ChangeState(EnemyState state)
    {
        enemyState = state;
        switch (state)
        {
            case EnemyState.Idle:
                time = Random.Range(3f, 5f);
                ChangeAnim("idle");
                break;
            case EnemyState.Patrol:
                time = Random.Range(3f, 5f);
                ChangeAnim("run");
                break;
            case EnemyState.Attack:
                ChangeAnim("run");
                break;
            case EnemyState.Death:
                ChangeAnim("hit");
                break;
            default:
                break;
        }
    }

    public void UpdateState(EnemyState state)
    {
        time -= Time.deltaTime;

        switch (state)
        {
            case EnemyState.Idle:
                if(time <= 0)
                {
                    ChangeState(EnemyState.Patrol);
                }
                break;
            case EnemyState.Patrol:
                if(time <= 0)
                {
                    ChangeState(EnemyState.Idle);
                }
                rb.velocity = isRight ? Vector2.right : Vector2.left * moveSpeed;
                break;
            case EnemyState.Attack:
                if (target != null)
                {
                    ChangedDirect(target.transform.position.x > transform.position.x);
                    rb.velocity = isRight ? Vector2.right : Vector2.left * moveSpeed;
                }
                else
                {
                    ChangeState(EnemyState.Patrol);
                }
                break;
            case EnemyState.Death:
                break;
            default:
                break;
        }
    }

    public void SetTarget(Player player)
    {
        this.target = player;

        if (target != null)
        {
            ChangeState(EnemyState.Attack);
        }
        else
        {
            ChangeState(EnemyState.Patrol);
        }
    }

    public override void Hit()
    {
        base.Hit();
        ChangeState(EnemyState.Death);
        ChangeAnim("hit");
        rb.velocity = Vector2.zero;
        collider.isTrigger = true;
        Vector2 force = Random.insideUnitCircle * 2f;
        force.y = Mathf.Abs(force.y) * 2f;
        rb.AddForce(force);
    }
}
