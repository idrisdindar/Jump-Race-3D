using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer line;
    [SerializeField]GameObject spinZone;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, transform.parent.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            line.SetPosition(1, new Vector3(0, (hit.point.y - transform.position.y), 0));
            spinZone.transform.position = new Vector3(0,hit.point.y,0);
            spinZone.transform.localPosition = new Vector3(0, spinZone.transform.localPosition.y, 0);
            if (hit.transform.CompareTag("PerfectTrampoline"))
            {
                line.startColor = Color.green;
            }
            else if (hit.transform.CompareTag("Trampoline") || hit.transform.CompareTag("PerfectTrampoline"))
            {
                if ((hit.point - transform.position).magnitude < 5f)
                {
                    line.startColor = Color.green;
                }
                else
                    line.startColor = Color.yellow;
            }
            else
                line.startColor = Color.red;
        }
        else
        {
            line.startColor = Color.red;
        }
    }
}
