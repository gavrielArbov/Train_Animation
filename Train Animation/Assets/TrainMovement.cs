using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrainMovement : MonoBehaviour
{
    Animator animator;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > 30f)
        {
            animator.SetTrigger("Forward");
            
           
        }
        if (timer > 60f)
        {
            animator.SetTrigger("Backward");
            timer = 0;
            
        }
        //print(timer);
    }
}
