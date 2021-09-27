using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    [SerializeField]private float bouncingForce = 20f, speed;
    float distToGround;
    bool jumpOnce;
    LineRenderer line;
    [SerializeField]private SwerveInputSystem swerveInputSystem;
    [SerializeField]private float swerveSpeed = 1.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        line = GetComponentInChildren<LineRenderer>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    

    private void Update()
    {
        if (UiHandler.Instance.IsGameStarted && !UiHandler.Instance.IsGameEnded)
        {
            if (swerveInputSystem.ButtonPressed)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                float swerveAmount = swerveSpeed * swerveInputSystem.MoveFactorX;
                transform.Rotate(0, swerveAmount, 0);
            }
        }
            if (IsGrounded())
            {
                anim.SetBool("isGround", true);
            }
            else
            {
                anim.SetBool("isGround", false);
                jumpOnce = true;
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (jumpOnce)
        {
            if (collision.transform.CompareTag("PerfectTrampoline"))
            {
                anim.SetTrigger("Stylish");
                rb.AddForce(Vector3.up * bouncingForce, ForceMode.Impulse);
                Trampoline trampoline = collision.gameObject.GetComponentInParent<Trampoline>();
                if (trampoline.isBreakable) trampoline.BreakCircles();
                UiHandler.Instance.SetSliderValue(trampoline);
                jumpOnce = false;
                return;
            }else if (collision.transform.CompareTag("Trampoline"))
            {
                rb.AddForce(Vector3.up * bouncingForce, ForceMode.Impulse);
                Trampoline trampoline = collision.gameObject.GetComponentInParent<Trampoline>();
                if (trampoline.isBreakable) trampoline.BreakCircles();
                UiHandler.Instance.SetSliderValue(trampoline);
                jumpOnce = false;
                return;
            }
        }
        if (collision.transform.CompareTag("Ending"))
        {
            anim.SetBool("End", true);
            collision.gameObject.GetComponent<Ending>().StartFireworks();
            UiHandler.Instance.LevelFinish();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            UiHandler.Instance.GameOver();
            gameObject.SetActive(false);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.5f);
    }

    
}
