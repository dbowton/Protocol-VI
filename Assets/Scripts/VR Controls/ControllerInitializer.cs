using UnityEngine;

public class ControllerInitializer : MonoBehaviour
{
    [SerializeField] GameObject Oculus_Quest_Rift_S_Controller;
    [SerializeField] GameObject Oculus_Quest2_Controller;
    [SerializeField] GameObject default_Controller;
    
    [HideInInspector] public GameObject controllerObject;
    [HideInInspector] public GrabPoint heldPoint;

    private void Start()
    {
		controllerObject = OVRPlugin.GetSystemHeadsetType() switch
		{
			OVRPlugin.SystemHeadset.Oculus_Quest_2 => Oculus_Quest2_Controller,
			OVRPlugin.SystemHeadset.Oculus_Quest or OVRPlugin.SystemHeadset.Rift_S => Oculus_Quest_Rift_S_Controller,
			_ => default_Controller,
		};
		
        controllerObject.SetActive(true);
    }
}
