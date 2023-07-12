using UnityEngine;

using VRButton = UnityEngine.XR.CommonUsages;

public class ControllerInitializer : MonoBehaviour
{
    [SerializeField] GameObject Oculus_Quest_Rift_S_Controller;
    [SerializeField] GameObject Oculus_Quest2_Controller;
    [SerializeField] GameObject default_Controller;

	[SerializeField] GameObject hand;

	[SerializeField] bool isLeftHand = true;

    [HideInInspector] public GrabPoint heldPoint;
    [HideInInspector] public GameObject controllerObject;

	private bool usingHands = true;

	public bool UseHands 
	{ 
		get { return usingHands; } 
		set 
		{
			if (usingHands && !value)
			{
				controllerObject.SetActive(true);
				hand.SetActive(false);
			}
			else if(!usingHands && value) 
			{
				controllerObject.SetActive(false);
				hand.SetActive(true);
			}

			usingHands = value;
		}
	}

    private void Start()
    {
		controllerObject = OVRPlugin.GetSystemHeadsetType() switch
		{ 
			OVRPlugin.SystemHeadset.Oculus_Quest_2 => Oculus_Quest2_Controller,
			OVRPlugin.SystemHeadset.Oculus_Quest or OVRPlugin.SystemHeadset.Rift_S => Oculus_Quest_Rift_S_Controller,
			_ => default_Controller,
		};
    }

	private void Update()
	{
		if (usingHands)
		{
			float grip;
			float trigger;

			if (isLeftHand)
			{
				ControllerManager.leftInput.GetControllerPressed(VRButton.grip, out grip);
				ControllerManager.leftInput.GetControllerPressed(VRButton.trigger, out trigger);
			}
			else
			{
				ControllerManager.rightInput.GetControllerPressed(VRButton.grip, out grip);
				ControllerManager.rightInput.GetControllerPressed(VRButton.trigger, out trigger);
			}

			hand.GetComponent<Animator>().SetFloat("Grip", grip);
			hand.GetComponent<Animator>().SetFloat("Trigger", trigger);
		}
	}
}
