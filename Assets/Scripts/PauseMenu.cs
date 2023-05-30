using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VRButton = UnityEngine.XR.CommonUsages;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject UIPrefab;
    GameObject UIObject;

    void Update()
    {
        ControllerManager.leftInput.GetControllerPressed(VRButton.menuButton, out bool pressed);

		if (pressed && UIObject == null)
        {
			Time.timeScale = 0f;
			UIObject = Instantiate(UIPrefab, transform.position, Quaternion.identity);
			UIObject.transform.FindChildRecursive("ResumeBacking").GetComponent<EventHolder>().action.AddListener(Disable);
		}
    }

    public void Disable()
    {
		Time.timeScale = 1.0f;
		Destroy(UIObject);
		UIObject = null;
	}

	private void OnDestroy()
	{
        Time.timeScale = 1f;
	}
}
