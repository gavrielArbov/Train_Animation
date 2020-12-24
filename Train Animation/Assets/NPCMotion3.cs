using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCMotion3 : MonoBehaviour
{
    float speed;
    float minDist;
    private bool isHurt;


    public GameObject theDestination;
    public GameObject theDestination2;

    NavMeshAgent theAgent;

    public int pivotPoint;


    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        minDist = 2;
        isHurt = false;
        animator = GetComponent<Animator>();
        theAgent = GetComponent<NavMeshAgent>();
        theAgent.Warp(theDestination2.transform.position);
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
        if (isHurt)
        {
            StartCoroutine(Die());

        }
        else
        {
            GetComponent<Animation>().Play("BasicMotions@Walk01");

            //animator.SetBool("Walk", true);
            //animator.SetBool("SprintJump", false);
            //animator.SetBool("SprintSlide", false);


            if (pivotPoint == 0)
                theAgent.SetDestination(theDestination.transform.position);



            if (pivotPoint == 1)
                theAgent.SetDestination(theDestination2.transform.position);



            //print(pivotPoint);






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
        if (other.tag == "BrownTable")
            pivotPoint = 0;


        if (other.tag == "GoldChair")
            pivotPoint = 1;

    }

}

