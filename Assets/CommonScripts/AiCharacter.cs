﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCharacter : Character
{
    public bool strolling = false;
    public float strollInterval = 1;
    public float strollRange = 2;
    public float loseTargetRange = 15;
    public float loseTargetTime = 10;
    protected NavMeshAgent nav;
    protected float loseTargetTimer;
   
    private float strollTimer;
    private NavMeshPath path;
    protected override void Awake()
    {
        base.Awake();
        path = new NavMeshPath();
        nav = GetComponent<NavMeshAgent>();

    }

    protected override void FixedUpdate()
    {
        if (!isActive)
        {
            Vector3 posotion = transform.position;
            transform.position -= new Vector3(0, (float)(0.4 * Time.deltaTime),0);
            if (transform.position.y <= -3) Destroy(this.gameObject);
            return;
        }
        base.FixedUpdate();
        AI();
    }

    protected override void AutoLockTarget()
    {
        //Check target lost
        if (target != null)
        {
            Character script = target.GetComponent<Character>();
            if (!script.IsActive || loseTargetTimer >= loseTargetTime)
            {
                loseTargetTimer = 0;
                target = null;
                if (debug) print(tag + ": Lost target");
            }
            else
            {
                float distance = (target.transform.position - transform.position).magnitude;
                if (distance > loseTargetRange)
                    loseTargetTimer += Time.deltaTime;
            }
        }
        else
        {
            base.AutoLockTarget();
        }
        
    }


    protected virtual void Strolling()
    {
        if (nav == null) return;
        strollTimer += Time.deltaTime;
        if (strollTimer > strollInterval)
        {
            if (debug) print(tag + " speed: " + velocity.magnitude);
            float x = Random.Range(-strollRange, strollRange);
            float y = Random.Range(-strollRange, strollRange);
            Vector3 dest = transform.position;
            dest.x += x;
            dest.z += y;
            nav.SetDestination(dest);
            strollTimer = Random.Range(0, strollInterval / 2);
        }
    }

    protected override void Die()
    {
        base.Die();
        if (nav != null) nav.enabled = false;
    }

    //Follow target when target is locked
    protected virtual void FollowTarget()
    {
        //if(Vector3.Distance(target.transform.position, nav.destination) > 0.1)
        float diff = Vector3.Distance(target.transform.position, nav.destination);
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (!nav.pathPending && diff > dist/5)
        {
            nav.CalculatePath(target.transform.position, path);
            nav.SetPath(path);
            //nav.SetDestination(target.transform.position);
        }
    }

    protected virtual void AI()
    {
        if (target != null)
        {
            float distance = (target.transform.position - transform.position).magnitude;
            float angle = Vector3.Angle(target.transform.position - transform.position, transform.forward);
            if (distance >= attackRange * 0.85f)
            {
                if (nav != null && nav.isActiveAndEnabled) FollowTarget();
            }
            else if (angle > attackAngle / 2f)
            {
                TurnToTarget();
            }
            else if(attackTimer >= attackInterval)
            {
                Attack();
                attackTimer = 0;
            }
        }
        else if (nav != null &&  strolling && nav.isActiveAndEnabled)
        {
            if (debug) print("123123~~~~~~~~~~~~~~");
            Strolling();
        }
        else if (nav != null && nav.isActiveAndEnabled)
        {
            if (debug) print("123123~~~~~~~~~~~~~~");
            nav.ResetPath();
        }
    }


}
