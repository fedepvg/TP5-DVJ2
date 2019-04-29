using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
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
    float InputAcc;
    float BaseLinearForce;
    Vector2 MoveInput;
    Vector3 MoveDir = Vector3.zero;
    void Awake()
    {
        rigi = GetComponentInParent<Rigidbody>();
        SpeedZ = 7000f;
        PitchRate = 700;
        RollRate = -700;
        YawRate = 700;
        BaseLinearForce = 300f;
        Physics.gravity = new Vector3(0f,-91.8f, 0f);
    }

    private void Start()
    {
        rigi.AddRelativeForce(new Vector3(0, 0, BaseLinearForce * SpeedZ * InputAcc * Time.deltaTime), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref InputAcc);


        rigi.AddRelativeTorque(InputPitch * PitchRate * Time.deltaTime, InputYaw * YawRate * Time.deltaTime, InputRoll * RollRate * Time.deltaTime, ForceMode.Force);

        rigi.AddRelativeForce(new Vector3(0, 0, BaseLinearForce * SpeedZ*InputAcc*Time.deltaTime),ForceMode.Force);

        Debug.Log(rigi.velocity.sqrMagnitude);
        Debug.DrawRay(transform.position, transform.forward * 10);
    }

    void GetInput(ref float InputPitch, ref float InputRoll, ref float InputYaw, ref float InputAcc)
    {
        InputAcc = Input.GetAxis("Vertical");
        InputYaw = Input.GetAxis("Horizontal");

        Vector3 mousePos = Input.mousePosition;

        InputPitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        InputRoll = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        InputPitch = -Mathf.Clamp(InputPitch, -1.0f, 1.0f);
        InputRoll = Mathf.Clamp(InputRoll, -1.0f, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer)=="Floor")
        {
            rigi.useGravity = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Floor")
        {
            rigi.useGravity = true;
        }
    }
}
