using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum States
    {
        Idle, Attack, Escape, Last,
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
    private MachineGun Gun;
    [SerializeField]
    private float EscapeDistance;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        Speed = 60000f;
        RotationRate = 700f;
        ChaseDistance = 300f;
        EscapeDistance = 70f;
        Gun = GetComponentInChildren<MachineGun>();
    }

    void Update()
    {
        CheckState();
        switch (EnemyState)
        {
            case States.Idle:

                break;
            case States.Attack:
                SetRotation();
                rigi.AddRelativeForce(0, 0, Speed * Time.deltaTime, ForceMode.Force);
                Gun.Attack();
                break;
            case States.Escape:
                SetRotation(PlayerPos.rotation);
                rigi.AddRelativeForce(0, 0, Speed * Time.deltaTime, ForceMode.Force);
                break;
        }
    }

    void CheckState()
    {
        if(Vector3.Distance(transform.position, PlayerPos.position) < EscapeDistance)
        {
            EnemyState = States.Escape;
        }
        else if(Vector3.Distance(transform.position, PlayerPos.position) < ChaseDistance)
        {
            if (GetTargetAhead())
            {
                EnemyState = States.Attack;
            }
            else
            {
                EnemyState = States.Escape;
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

    void SetRotation(Quaternion rot)
    {
        transform.rotation = rot;
    }

    public States GetState()
    {
        return EnemyState;
    }
}
