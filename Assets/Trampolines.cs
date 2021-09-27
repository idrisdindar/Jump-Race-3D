using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolines : MonoBehaviour
{
    public Trampoline[] trampolines;

    private void Start()
    {
        trampolines = GetComponentsInChildren<Trampoline>();
        UiHandler.Instance.SetupSlider(trampolines.Length);
    }
}
