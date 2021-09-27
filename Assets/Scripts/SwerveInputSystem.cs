using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveInputSystem : MonoBehaviour
{
    private float lastFrameFingerPositionX;
    private float moveFactorX;
    private bool buttonPressed;

    public float MoveFactorX { get => moveFactorX;}
    public bool ButtonPressed { get => buttonPressed;}

    private void Update()
    {
        if (UiHandler.Instance.IsGameStarted && !UiHandler.Instance.IsGameEnded)
        {
            if (Input.GetMouseButtonDown(0))
            {

                lastFrameFingerPositionX = Input.mousePosition.x;

            }
            else if (Input.GetMouseButton(0))
            {
                buttonPressed = true;
                moveFactorX = Input.mousePosition.x - lastFrameFingerPositionX;
                lastFrameFingerPositionX = Input.mousePosition.x;

            }
            else if (Input.GetMouseButtonUp(0))
            {
                moveFactorX = 0;
                buttonPressed = false;
            }
        }
    }
}
