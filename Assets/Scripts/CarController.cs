using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool boostInput;
    private float currentSteerAngle;
    public Rigidbody carRB;
    public Animator anim;
    bool canDoubleJump;
    public Vector3 boxSize;
    public float maxDis;
    public LayerMask lm;
    private bool boosting;
    public float boost, maxBoost;
    public float boostCost;
    public Camera cam;

    public float motorForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float jumpForce = 5;
    //[SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private float turnTorque;
    [SerializeField] private float fallSpeed;
    //[SerializeField] private bool isFalling = true;
    
    public WheelCollider FLC;
    public WheelCollider FRC;
    public WheelCollider BLC;
    public WheelCollider BRC;

    [SerializeField] private Transform FL;
    [SerializeField] private Transform FR;
    [SerializeField] private Transform BL;
    [SerializeField] private Transform BR;

    private void Start()
    {
        carRB = GetComponent<Rigidbody>();
        carRB.centerOfMass = new Vector3(0, -0.1f, 0);
    }

    bool IsGrounded()
    {
      if (Physics.CheckBox(transform.position, boxSize, transform.rotation, lm))
        {
            canDoubleJump = true;
            return true;
        }
      else
        {
            return false;
        }
    }

    //private void OnTriggerEnter(Collider GroundDetect)
    //{
        //isFalling = false;
        //canDoubleJump = false;
    //}

    //private void OnTriggerExit(Collider GroundDetect)
    //{
        //isFalling = true;
        //canDoubleJump = true;
    //}
    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    public void GetInput(float forwardInput, float turnInput, bool jumpI, bool boostI)
    {
        horizontalInput = turnInput;
        verticalInput = forwardInput;
        jumpInput = jumpI;
        boostInput = boostI;
    }

    private void Update()
    {
        if (jumpInput)
        {
            if (IsGrounded())
            {
                carRB.velocity += transform.TransformDirection(new Vector3(0, 1, 0)) * jumpForce;
                carRB.velocity += transform.forward * jumpForce;
                canDoubleJump = true;
            }
            else if (canDoubleJump && horizontalInput == 0)
            {
                anim.SetTrigger("DoubleJump");
                carRB.velocity += transform.TransformDirection(new Vector3(0, 1, 0)) * jumpForce;
                carRB.velocity += transform.forward * jumpForce;
                canDoubleJump = false;
            }
            else if (canDoubleJump && horizontalInput != 0)
            {
                if (horizontalInput > 0)
                {
                    anim.SetTrigger("RollRight");
                    carRB.velocity += transform.right * jumpForce;
                }
                else if (horizontalInput < 0)
                {
                    anim.SetTrigger("RollLeft");
                    carRB.velocity -= transform.right * jumpForce;
                }
                canDoubleJump = false;
            }
        }

        if (!IsGrounded())
        {
            carRB.AddTorque(transform.up * turnTorque * horizontalInput);
            carRB.AddForce(-Vector3.up * fallSpeed);
        }

        if(boostInput)
        {
            boosting = true;
        }
        else if(!boostInput)
        {
            boosting = false;
        }

        if (boosting && boost != 0)
        {
            carRB.AddForce(transform.forward * 25, ForceMode.Acceleration);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 100, Time.deltaTime * 3);

            boost -= boostCost * Time.deltaTime;
            if (boost < 0) boost = 0;
        }
        else if (!boosting)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 70, Time.deltaTime * 3);
        }
    }

    private void HandleMotor()
    {
        FLC.motorTorque = (verticalInput * motorForce);
        FRC.motorTorque = (verticalInput * motorForce);
        BLC.motorTorque = (verticalInput * motorForce);
        BRC.motorTorque = (verticalInput * motorForce);
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        FLC.steerAngle = currentSteerAngle;
        FRC.steerAngle = currentSteerAngle;
        BLC.steerAngle = - currentSteerAngle;
        BRC.steerAngle = - currentSteerAngle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(FLC, FL);
        UpdateSingleWheel(FRC, FR);
        UpdateSingleWheel(BLC, BL);
        UpdateSingleWheel(BRC, BR);
    }

    private void UpdateSingleWheel(WheelCollider WC, Transform WT)
    {
        Vector3 pos;
        Quaternion rot;
        WC.GetWorldPose(out pos, out rot);
        WT.rotation = rot;
        WT.position = pos;
    }
}