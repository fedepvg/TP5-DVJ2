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
    float Throttle;
    float BaseLinearForce;
    Vector2 MoveInput;
    Vector3 MoveDir = Vector3.zero;
    void Awake()
    {
        rigi = GetComponentInParent<Rigidbody>();
        SpeedZ = 6000f;
        PitchRate = 70;
        RollRate = -70;
        YawRate = 70;
        BaseLinearForce = 300f;
        Physics.gravity = new Vector3(0f,-91.8f, 0f);
    }

    private void FixedUpdate()
    {
        GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref Throttle);

        rigi.AddRelativeForce(new Vector3(0, 0, SpeedZ*Throttle),ForceMode.Force);

        Debug.Log(rigi.velocity.sqrMagnitude);
        Debug.DrawRay(transform.position, transform.forward * 10);
    }

    private void Update()
    {
        GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref Throttle);
        transform.Rotate(InputPitch * PitchRate * Time.deltaTime, InputYaw * YawRate * Time.deltaTime, InputRoll * RollRate * Time.deltaTime, Space.Self);
    }

    void GetInput(ref float InputPitch, ref float InputRoll, ref float InputYaw, ref float Throttle)
    {
        //Throttle = Input.GetAxis("Vertical");
        InputYaw = Input.GetAxis("Horizontal");

        Vector3 mousePos = Input.mousePosition;

        InputPitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        InputRoll = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        InputPitch = -Mathf.Clamp(InputPitch, -1.0f, 1.0f);
        InputRoll = Mathf.Clamp(InputRoll, -1.0f, 1.0f);

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
