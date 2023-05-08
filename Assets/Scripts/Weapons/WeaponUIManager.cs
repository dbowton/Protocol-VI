using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUIManager : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Text bulletCount;

	public void UpdateBulletCount(int count)
	{
		if (count <= 0)
			bulletCount.text = "00";
		else if (count >= 1000)
			bulletCount.text = "∞";
		else
			bulletCount.text = count.ToString();
	}
}
