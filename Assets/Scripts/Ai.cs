using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    float distToGround;
    bool jumpOnce;

    [SerializeField]
    private float bouncingForce = 20f, speed;

    [SerializeField]private Trampoline currentTarget;
    [SerializeField]private Trampoline nextTarget;

    [SerializeField]private bool jumped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {

        if (IsGrounded())
        {
            anim.SetBool("isGround", true);
        }
        else
        {
            anim.SetBool("isGround", false);
            jumpOnce = true;

            if (UiHandler.Instance.IsGameStarted && !UiHandler.Instance.IsGameEnded)
            {
                if (jumped)
                {
                    if (GetNextTarget())
                    {
                        MoveTowardsNextTarget();
                    }
                }
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (jumpOnce)
        {
            if (collision.transform.CompareTag("PerfectTrampoline"))
            {
                if (collision.transform.parent.TryGetComponent(out Trampoline trampoline))
                {
                    currentTarget = trampoline;
                }
                rb.AddForce(Vector3.up * bouncingForce, ForceMode.Impulse);
                anim.SetTrigger("Stylish");
                jumpOnce = false;
                
                return;
            }else if (collision.transform.CompareTag("Trampoline"))
            {
                if (collision.transform.parent.TryGetComponent(out Trampoline trampoline))
                {
                    currentTarget = trampoline;
                }
                rb.AddForce(Vector3.up * bouncingForce, ForceMode.Impulse);
                jumpOnce = false; 
                if (UiHandler.Instance.isGameStarted)
                    jumped = true;
                return;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }

    private bool GetNextTarget()
    {
        if (currentTarget.transform.TryGetComponent(out Trampoline trampoline))
        {
            return nextTarget = trampoline.Target.GetComponent<Trampoline>();
        }
        return false;
    }

    private void MoveTowardsNextTarget()
    {
        Vector3 randomPosition = Random.insideUnitCircle * 2.5f;

        transform.position = Vector3.MoveTowards(transform.position, nextTarget.transform.position, speed * Time.deltaTime);

    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.5f);
    }
}
