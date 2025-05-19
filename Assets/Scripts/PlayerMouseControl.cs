using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseControl : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float moveSpeed = 10;


    private CharacterController characterController;
    private Vector3 targetPosition;
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
    }

    void Update()
    {
        float distToTarget = Vector3.Distance(transform.position, targetPosition);
        if(distToTarget > 1f && PlayerHealth.isAlive)
        {
            Vector3 targetDirection = Vector3.Normalize( targetPosition - transform.position);
            characterController.Move(targetDirection * moveSpeed * Time.deltaTime);
            transform.LookAt(targetPosition);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 500, layerMask))
            {
                targetPosition = hit.point;
            }
        }
    }
}
