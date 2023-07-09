using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandTest : MonoBehaviour
{
    [System.Serializable]
    public struct HandData
    {
        [Range(0,1)] public float gripAmount;
		[Range(0, 1)] public float triggerAmount;
    }

    public HandData leftHand;
    public HandData rightHand;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    void Update()
    {
        leftHandAnimator.SetFloat("Grip", leftHand.gripAmount);
        leftHandAnimator.SetFloat("Trigger", leftHand.triggerAmount);

        rightHandAnimator.SetFloat("Grip", rightHand.gripAmount);
		rightHandAnimator.SetFloat("Trigger", rightHand.triggerAmount);
    }
}
