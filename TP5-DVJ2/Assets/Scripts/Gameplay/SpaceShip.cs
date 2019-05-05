﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : ShipBase
{
    private Rigidbody rigi;
    [SerializeField]
    private float SpeedZ;
    [SerializeField]
    private float PitchRate;
    [SerializeField]
    private float RollRate;
    [SerializeField]
    private float YawRate;
    float InputPitch;
    float InputRoll;
    float InputYaw;
    float Throttle;
    private MachineGun Gun;
    int Fuel;
    float FuelTimer;

    void Awake()
    {
        rigi = GetComponentInParent<Rigidbody>();
        SpeedZ = 8000f;
        PitchRate = 70;
        RollRate = -70;
        YawRate = 70;
        Physics.gravity = new Vector3(0f,-91.8f, 0f);
        Gun = GetComponentInChildren<MachineGun>();
        Health = 200;
        FuelTimer = 0;
        Fuel = 100;
    }

    private void FixedUpdate()
    {
        if (Health > 0 && Fuel>0)
        {
            GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref Throttle);

            rigi.AddRelativeForce(new Vector3(0, 0, SpeedZ * Throttle), ForceMode.Force);
        }
    }

    private void Update()
    {
        if(Health<=0)
        {
            CameraManager.Instance.SwitchToExplosionCamera(transform.position);
            Explode();
            GameManager.Instance.GameOver();
        }
        else
        {
            GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref Throttle);
            transform.Rotate(InputPitch * PitchRate * Time.deltaTime, InputYaw * YawRate * Time.deltaTime, InputRoll * RollRate * Time.deltaTime, Space.Self);
            Gun.Attack();
            FuelTimer += Time.deltaTime;
            if(FuelTimer>1)
            {
                FuelTimer = 0;
                Fuel--;
            }
            Debug.Log(Fuel);
        }
    }

    void GetInput(ref float InputPitch, ref float InputRoll, ref float InputYaw, ref float Throttle)
    {
        InputRoll = Input.GetAxis("Horizontal");

        Vector3 mousePos = Input.mousePosition;

        InputPitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        InputYaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        InputPitch = -Mathf.Clamp(InputPitch, -1.0f, 1.0f);
        InputYaw = Mathf.Clamp(InputYaw, -1.0f, 1.0f);

        bool AccInput;
        float Target = Throttle;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Target = 1.0f;
            AccInput = true;
        }
        else
        { 
            Target = 0f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                AccInput = true;
            else
                AccInput = false;
        }
        

        if (AccInput)
        {
            Throttle = Mathf.MoveTowards(Throttle, Target, Time.deltaTime * 0.5f);
        }
        else
        {
            Throttle = Mathf.MoveTowards(Throttle, Target, Time.deltaTime * 0.25f);
        }
        
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Floor" || LayerMask.LayerToName(other.gameObject.layer) == "Water")
        {
            Health = 0;
        }

        if(other.tag=="EnemyBullet")
        {
            Health -= 50;
        }

        if(other.tag=="Enemy")
        {
            Health = 0;
        }
        Debug.Log("Health: " + Health);
    }

    public int GetFuel()
    {
        return Fuel;
    }

    public int GetHealth()
    {
        return Health;
    }
}
