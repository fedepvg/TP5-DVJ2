using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum States
    {
        Idle, Chase, Attack, Last,
    }

    [SerializeField]
    private States EnemyState;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float RotationRate;
    [SerializeField]
    private float ChaseDistance;
    private Rigidbody rigi;
    public Transform PlayerPos;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        Speed = 100000f;
        RotationRate = 700f;
        ChaseDistance = 500f;
    }

    void Update()
    {
        CheckState();
        switch (EnemyState)
        {
            case States.Idle:

                break;
            case States.Chase:
                SetRotation();
                rigi.AddRelativeForce(0, 0, Speed * Time.deltaTime, ForceMode.Force);
                break;
            case States.Attack:
                SetRotation();
                rigi.AddRelativeForce(0, 0, Speed * Time.deltaTime, ForceMode.Force);
                break;
        }
    }

    void CheckState()
    {
        if(Vector3.Distance(transform.position,PlayerPos.position)<ChaseDistance)
        {
            if(GetTargetAhead())
            {
                EnemyState = States.Attack;
            }
            else
            {
                EnemyState = States.Chase;
            }
        }
        else
        {
            EnemyState = States.Idle;
        }
    }

    bool GetTargetAhead()
    {
        Vector3 heading = PlayerPos.position - transform.position;
        float dot = Vector3.Dot(heading, transform.forward);
        if(dot<=0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void SetRotation()
    {
        Quaternion q01 = Quaternion.identity;
        q01.SetLookRotation(PlayerPos.position - transform.position, Vector3.up);
        transform.rotation = q01;
    }

    public States GetState()
    {
        return EnemyState;
    }
}
