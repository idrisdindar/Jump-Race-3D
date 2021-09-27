using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    LineRenderer line;
    [SerializeField]GameObject target;
    public bool isBreakable;
    [SerializeField] List<GameObject> circles = new List<GameObject>();
    public GameObject Target { get => target;}

    private void Awake()
    {
       line = GetComponentInChildren<LineRenderer>();
    }

    private void Start()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target.transform.position);
    }

    public void BreakCircles()
    {
        int circleCount = circles.Count;
        for (int i = circleCount - 1; i > circleCount - 3; i--)
        {
            var circle = circles[circles.Count - 1];
            circle.GetComponent<Rigidbody>().useGravity = true;
            circle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Destroy(circle, 1f);
            circles.Remove(circle);
        }
    }
}
