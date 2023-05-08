using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DirectionalObject
{
	public static GameObject GetObjectBehind(GameObject center, List<GameObject> positions)
	{
		if(positions.Count == 0) return center;

		positions.Max(x => Vector3.SignedAngle(center.transform.forward, (x.transform.position - center.transform.position).normalized, Vector3.up));

		float maxAngle = Mathf.Abs(Vector3.SignedAngle(center.transform.forward, (positions[0].transform.position - center.transform.position).normalized, center.transform.up));
		GameObject savedMax = positions[0];

		for (int i = 1; i < positions.Count; i++)
		{
			float calcAngle = Mathf.Abs(Vector3.SignedAngle(center.transform.forward, (positions[i].transform.position - center.transform.position).normalized, center.transform.up));

			if (calcAngle > maxAngle)
			{
				maxAngle = calcAngle;
				savedMax = positions[i];
			}
		}

		return savedMax;
	}
}
