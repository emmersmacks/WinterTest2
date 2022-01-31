using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogr : Monsters
{
    public float downAttackDistance;
    public Vector3 nowPos;
    public GameObject boom;

    void Start()
    {
        enemyTarget = GameObject.FindWithTag("Player").transform;
        homeTarget = GameObject.FindWithTag("OgrHome").transform;
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        speed = 0.5f;
        health = 500;
        seeDistance = 8;
        attackDistance = 3;
        downAttackDistance = 2;
    }

    void Update()
    {
        TryFlip();

        if (!IsInHome() && State == MonsterState.goHome)
            ChangeState(MonsterState.goHome);

        else if (IsCanAttackTarget() || State == MonsterState.attack)
            ChangeState(MonsterState.attack);

        else if (State == MonsterState.attackInProcess)
            ChangeState(MonsterState.attackInProcess);

        else if (IsTargetInSeeRange())
            ChangeState(MonsterState.followTarget);

        else if (IsInHome())
            ChangeState(MonsterState.idle);
    }

    private void TryFlip()
    {
        sprite.flipX = transform.position.x > enemyTarget.transform.position.x;
    }

    public void GoHome()
    { 
        ChangeState(MonsterState.goHome);
    }

    protected override void AttackTarget()
    {
        var canDownAttack = DistanceOfTarget() < downAttackDistance;

        if (canDownAttack)
        {
            CurrentAnimation = MonsterAnimations.downHit;
            StartCoroutine(WaitAnimation(0.5f));
            StartCoroutine(WaitDownHit());
        }
        else
        {
            CurrentAnimation = MonsterAnimations.hit;
            StartCoroutine(WaitAnimation(0.4f));
            StartCoroutine(WaitSpawnDestroyObj(1f, -1.1f, boom));
        }
    }

    private IEnumerator WaitDownHit()
    {
        StartCoroutine(WaitSpawnDestroyObj(0.5f, -1.1f, boom));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(WaitSpawnDestroyObj(-0.5f, -1.1f, boom));
    }

    private IEnumerator WaitSpawnDestroyObj(float xCoord, float yCoord, GameObject instObject)
    {
        yield return new WaitForSeconds(0.4f);
        SpawnEnemyDestroyObj(xCoord, yCoord, instObject);
    }
}

public enum MonsterAnimations
{
    idle,
    go,
    hit,
    jumpHit,
    downHit,
    awake,
}
