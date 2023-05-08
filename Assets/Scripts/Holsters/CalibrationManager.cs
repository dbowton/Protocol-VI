using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CalibrationManager : MonoBehaviour
{
	[SerializeField] List<Calibration> calibrations = new List<Calibration>();
	[SerializeField] UnityEvent OnCalibrated;
	[SerializeField] TMPro.TMP_Text text;
	int index = 0;

	private void Update()
	{
		if (index < calibrations.Count)
		{
			text.text = calibrations[index].desc + (calibrations[index].IsLeftHand ? "\nLeft Trigger to Continue" : "\nRight Trigger to Continue");

			if (calibrations[index].CheckCalibration())
			{
				index++;

				if(index == calibrations.Count) 
				{
					text.text = "Calibration Complete";
					PlayerPrefs.SetInt("CalibrationComplete", 1);
					OnCalibrated.Invoke();
				}
			}
		}
	}
}
