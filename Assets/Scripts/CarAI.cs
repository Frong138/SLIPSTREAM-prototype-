using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private GameObject tracker;

    private CarController carController;
    private Vector3 targetPos;
    public int cubeNum;
    public GameObject respawnPos;
    public GameObject[] targCubes = new GameObject[42];
    public int targCubeTrack;
    public float randomX;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        randomX = targCubes[targCubeTrack].transform.position.x + Random.Range(-12, 12);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        tracker.transform.position = targCubes[targCubeTrack].transform.position;
        tracker.transform.rotation = targCubes[targCubeTrack].transform.rotation;

        SetTargetPos(tracker.transform.position);
        float forwardInput = 0f;
        float turnInput = 0f;
        bool jump = false;
        bool boost = false;

        float reacgedTargDis = 1f;
        float distToTarg = Vector3.Distance(transform.position, targetPos);
        Vector3 dirToTarg = (targetPos - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, dirToTarg);
        if (distToTarg > reacgedTargDis)
        {
            if (dot > 0)
            {
                forwardInput = 1f;
            }
            else
            {
                float reverseDis = 25f;
                if (distToTarg > reverseDis)
                {
                    forwardInput = 1f;
                }
                else
                {
                    forwardInput = -1f;
                }
            }

            ApplySteer();
            turnInput = ApplySteer();

            //float angleToDir = Vector3.SignedAngle(transform.forward, dirToTarg, Vector3.up);

            //if (angleToDir > 18)
            //{
            //    turnInput = 1f;
            //}
            //else if (angleToDir < -18)
            //{
            //    turnInput = -1f;
            //}
            //else
            //{
            //    turnInput = 0f;
            //}
        }
        else
        {
            jump = false;
            forwardInput = 0f;
            turnInput = 0f;
        }

        carController.GetInput(forwardInput, turnInput, jump, boost);       
    }

    public float ApplySteer()
    {
        float regY = targCubes[targCubeTrack].transform.position.y;
        float regZ = targCubes[targCubeTrack].transform.position.z;
        Vector3 randomVector = new Vector3(randomX, regY, regZ);
        Vector3 relativeVector = transform.InverseTransformPoint(randomVector);       
        float newSteer = relativeVector.x / relativeVector.magnitude;
        return newSteer;
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tracker)
        {           
            tracker.GetComponent<BoxCollider>().enabled = false;
            respawnPos.transform.position = other.transform.position;
            respawnPos.transform.rotation = other.transform.rotation;
            targCubeTrack += 1;
            if (targCubeTrack == (cubeNum))
            {
                targCubeTrack = 0;
            }
            yield return new WaitForSeconds(0);
            tracker.GetComponent<BoxCollider>().enabled = true;
            randomX = targCubes[targCubeTrack].transform.position.x + Random.Range(-12, 12);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Bounds"))
        {
            transform.position = respawnPos.transform.position;
            transform.rotation = respawnPos.transform.rotation;
        }
    }

    public void SetTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
}
