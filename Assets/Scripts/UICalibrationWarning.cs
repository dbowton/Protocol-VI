using UnityEngine;

public class UICalibrationWarning : MonoBehaviour
{
    [SerializeField] GameObject uiWarning;

    void Start()
    {
        uiWarning.SetActive((!PlayerPrefs.HasKey("CalibrationComplete") || PlayerPrefs.GetInt("CalibrationComplete") == 0));
    }
}
