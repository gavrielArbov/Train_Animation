using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCMotion : MonoBehaviour
{
    float speed;
    float minDist;
    private bool isHurt;


    public GameObject theDestination;
    public GameObject theDestination2;

    NavMeshAgent theAgent;

    public int pivotPoint;

    Rigidbody m_Rigidbody;

    Transform otherTransform;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        minDist = 2;
        isHurt = false;
        pivotPoint = -1;
        theAgent = GetComponent<NavMeshAgent>();
        //theAgent.Warp(theDestination2.transform.position);
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void Hurt()
    {
        isHurt = true;
    }

    IEnumerator Die()
    {
        GetComponent<Animation>().Play("die");
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isHurt)
        {
            StartCoroutine(Die());

        }
        else
        {

            if (pivotPoint == -1)
            {
                GetComponent<Animation>().Play("idle");
                if (timer > 10f)
                    pivotPoint = 0;
            }
            if (pivotPoint == 0)
            {
                GetComponent<Animation>().Play("walk");
                theAgent.SetDestination(theDestination.transform.position);

            }
            if (pivotPoint == 1)
            {
                GetComponent<Animation>().Play("idle");
                //transform.SetParent(theDestination.transform, false);
                //transform.position = new Vector3(theDestination.transform.position.x-1, transform.position.y, theDestination.transform.position.z-1);
                theAgent.transform.position = new Vector3(theDestination.transform.position.x-1, transform.position.y, theDestination.transform.position.z-1);
                theAgent.Warp(new Vector3(theDestination.transform.position.x - 1, transform.position.y, theDestination.transform.position.z - 1));

            }
            if (pivotPoint == 2)
            {
                GetComponent<Animation>().Play("walk");

                theAgent.SetDestination(theDestination2.transform.position);
            }
            if(pivotPoint == 3)
            {
                GetComponent<Animation>().Play("idle");
                theAgent.transform.position = new Vector3(theDestination2.transform.position.x - 5, transform.position.y, theDestination2.transform.position.z - 5);

            }
            if (timer > 55f && pivotPoint == 1)
            {
                //GetComponent<Animation>().Play("walk");
                //theAgent.SetDestination(theDestination2.transform.position);
                //theAgent.transform.position = new Vector3(theDestination.transform.position.x - 1, transform.position.y, theDestination.transform.position.z - 1);

                pivotPoint = 2;
                //timer = 0;

            }
            if(timer > 100f && pivotPoint == 3)
            {
                timer = 0;
                pivotPoint = 0;
            }



            //print(pivotPoint);

            //print(timer);




            /*
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Ray ray = new Ray(transform.position, transform.forward);
            */
            /*
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.5f, out hit)) // check obstacle
                {
                if (hit.distance < minDist) // change direction
                {
                    //float angle = Random.Range(-100, 100);
                    //transform.Rotate(new Vector3(0, 90, 0));
                }
            }
            */
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Poll")
        {
            pivotPoint = 1;
            //theAgent.transform.parent = other.transform;
            //otherTransform = other.transform;
        }

        if (other.tag == "Bench")
        {
            pivotPoint = 3;
            
        }


    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Poll")
        {
           // transform.SetParent(collision.gameObject.transform, true);
            //transform.localPosition = SomeDefaultPositionInPlayersLocalSpace;
        }
    }
    
}
