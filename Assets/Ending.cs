using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField]ParticleSystem[] fireworks;
    public void StartFireworks()
    {
        foreach(var firework in fireworks)
        {
            firework.Play();
        }
    }
}
