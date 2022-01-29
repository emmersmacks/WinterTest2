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
    
    protected MonsterState State = MonsterState.idle;
    
    protected MonsterAnimations CurrentAnimation
    {
        get { return (MonsterAnimations)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    void Awake()
    {
        CurrentAnimation = MonsterAnimations.awake;
    }

    public void ChangeState(MonsterState state)
    {
        State = state;

        switch (state)
        {
            case MonsterState.idle:
                CurrentAnimation = MonsterAnimations.idle;
                
                if (IsTargetInSeeRange())
                    ChangeState(MonsterState.followTarget);

                break;
            case MonsterState.goHome:
                if (GoHome())
                    ChangeState(MonsterState.idle);
                
                break;
            case MonsterState.followTarget:
                if (IsMonsterIsLeaveLocation())
                    ChangeState(MonsterState.goHome);
                
                if (IsTargetInAttackRange())
                    ChangeState(MonsterState.attack);
                
                FollowTarget();

                break;
            case MonsterState.attack:
                AttackTarget();

                ChangeState(MonsterState.followTarget);

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

    protected virtual bool IsMonsterIsLeaveLocation()
    { 
        return false;
    }

    private void FollowTarget()
    {
        CurrentAnimation = MonsterAnimations.go;
        GoTo(enemyTarget);
    }
    
    protected bool GoHome()
    {
        sprite.flipX = true;
        return GoTo(homeTarget);
    }

    protected bool GoTo(Transform nowTarget)
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
        ChangeState(MonsterState.goHome);
        yield return new WaitForSeconds(waitTime);
        ChangeState(MonsterState.idle);
    }

    protected float DistanceOfTarget()
    {
        return Vector3.Distance(transform.position, enemyTarget.transform.position);
    }
}
