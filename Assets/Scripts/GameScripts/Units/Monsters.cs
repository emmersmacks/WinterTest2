using System;
using System.Collections;
using UnityEngine;

public enum MonsterState : byte
{
    idle = 1,
    goHome,
    followTarget,
    attack,
}

public class Monsters : Unit
{
    public float seeDistance;
    public float attackDistance;
    public Transform enemyTarget;
    public Transform homeTarget;
    public SpriteRenderer sprite;
    public Vector3 movePos;
    private Timer _attackTimerDelay = new Timer(TimeSpan.FromSeconds(3));
    
    protected MonsterState State = MonsterState.idle;
    
    protected MonsterAnimations CurrentAnimation
    {
        get { return (MonsterAnimations)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    public void ChangeState(MonsterState state)
    {
        State = state;

        switch (state)
        {
            case MonsterState.idle:
                CurrentAnimation = MonsterAnimations.idle;

                break;
            case MonsterState.goHome:
                MoveToHome();

                break;
            case MonsterState.followTarget:
                FollowTarget();

                break;
            case MonsterState.attack:
                if (_attackTimerDelay.ResetIfElapsed())
                    AttackTarget();

                break;
            default:
                Debug.LogError("Unexpected state " + state.ToString());
                break;
        }
    }

    protected virtual void AttackTarget() { }

    protected virtual bool IsTargetInAttackRange()
    {
        return DistanceOfTarget() < attackDistance;
    }

    protected virtual bool IsTargetInSeeRange()
    {
        return DistanceOfTarget() < seeDistance;
    }

    private void FollowTarget()
    {
        CurrentAnimation = MonsterAnimations.go;
        MoveTo(enemyTarget);
    }

    public void MoveToHome()
    {
        sprite.flipX = true;
        MoveTo(homeTarget);
    }

    protected bool IsInHome()
    {
        return homeTarget.transform.position.x == transform.position.x;
    }

    protected bool MoveTo(Transform nowTarget)
    {
        movePos = new Vector3(nowTarget.transform.position.x, transform.position.y, 3f);
        transform.position = Vector2.MoveTowards(transform.position, movePos, speed * Time.fixedDeltaTime);

        var isReachTarget = nowTarget.transform.position.x == transform.position.x;

        return isReachTarget;
    }

    protected void SpawnEnemyDestroyObj(float xCoord, float yCoord, GameObject spawnObj)
    {
        GameObject instObj = Instantiate(spawnObj);

        instObj.transform.parent = transform;

        if (!sprite.flipX)
            instObj.transform.localPosition = new Vector3(xCoord, yCoord, -3f);
        else
            instObj.transform.localPosition = new Vector3(-xCoord, yCoord, -3f);

        instObj.transform.parent = null;
    }

    protected IEnumerator WaitAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ChangeState(MonsterState.idle);
    }

    protected float DistanceOfTarget()
    {
        return Vector3.Distance(transform.position, enemyTarget.transform.position);
    }
}
