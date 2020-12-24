using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCMotion2 : MonoBehaviour
{
    float speed;
    float minDist;
    private bool isHurt;

    private float timer;
    public GameObject theDestination;
    public GameObject theDestination2;

    NavMeshAgent theAgent;

    public int pivotPoint;

    Animator animator;
    FixedJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        minDist = 2;
        isHurt = false;

        animator = GetComponent<Animator>();
        theAgent = GetComponent<NavMeshAgent>();

        pivotPoint = -1;
    }



    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (pivotPoint == -1)
        {
            GetComponent<Animation>().Play("Z_Idle");
            if (timer > 10f)
                pivotPoint = 0;
        }
        if (pivotPoint == 0)
        {
            GetComponent<Animation>().Play("Z_Run_InPlace");
            theAgent.SetDestination(theDestination.transform.position);
        }
                



        if (pivotPoint == 1)
        {
            GetComponent<Animation>().Play("Z_Idle");
            transform.position = new Vector3(theDestination.transform.position.x+1, transform.position.y, theDestination.transform.position.z+1);
            theAgent.Warp(new Vector3(theDestination.transform.position.x - 1, transform.position.y, theDestination.transform.position.z - 1));

        }
        if (pivotPoint == 2)
        {
            GetComponent<Animation>().Play("Z_Run_InPlace");
            theAgent.SetDestination(theDestination2.transform.position);
        }
        if (pivotPoint == 3)
        {
            GetComponent<Animation>().Play("Z_Idle");
            theAgent.transform.position = new Vector3(theDestination2.transform.position.x + 5, transform.position.y, theDestination2.transform.position.z + 5);

        }
        if (timer > 55f && pivotPoint == 1)
        {
            pivotPoint = 2;
            //timer = 0;

        }
        if (timer > 96f && pivotPoint == 3)
        {
            timer = 0;
            pivotPoint = 0;
        }



        //print(timer);
        //print(pivotPoint);








    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Poll")
        {
            //theAgent.transform.parent = other.transform;
            pivotPoint = 1;
            //////


            //joint = other.gameObject.AddComponent<FixedJoint>();
            //joint.connectedBody = this.gameObject.GetComponent<Rigidbody>();
            //transform.SetParent(other.transform, true);
        }

        if (other.tag == "Bench")
        {
            pivotPoint = 3;

        }




    }

}
