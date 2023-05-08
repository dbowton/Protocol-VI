using UnityEngine;
using VRButton = UnityEngine.XR.CommonUsages;

public class Calibration : MonoBehaviour
{
	public string desc;
	[SerializeField] int weight = 0;
	[SerializeField] GunData gunData;
	[SerializeField] GameObject referencedObject;
	[SerializeField] TargetHand targetHand = TargetHand.LeftHand;
	public bool IsLeftHand { get { return targetHand.Equals(TargetHand.LeftHand); } }

	public enum TargetHand
	{
		LeftHand,
		RightHand
	}

	bool calibrated = false;

	public bool CheckCalibration()
	{
		if (ControllerManager.leftInput.GetControllerPressed(VRButton.triggerButton, out bool leftPressed) && !calibrated && leftPressed)
		{
			if(targetHand == TargetHand.LeftHand) 
			{
				Vector3 calibratedPos = referencedObject.transform.InverseTransformDirection(ControllerManager.Left.position);
				gunData.UpdatePos(((calibratedPos * weight) + calibratedPos) / (1 + weight));

				calibrated = true;
			}
		}

		if (ControllerManager.rightInput.GetControllerPressed(VRButton.triggerButton, out bool rightPressed) && !calibrated && rightPressed)
		{
			if(targetHand == TargetHand.RightHand)
			{
				Vector3 calibratedPos = referencedObject.transform.InverseTransformDirection(ControllerManager.Right.position);
				gunData.UpdatePos(((calibratedPos * weight) + calibratedPos) / (1 + weight));

				calibrated = true;
			}
		}

		return (calibrated && !leftPressed && !rightPressed);
	}
}
