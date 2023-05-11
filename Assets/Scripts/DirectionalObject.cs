using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DirectionalObject
{
	public static GameObject GetObjectBehind(GameObject center, List<GameObject> positions)
	{
		if(positions.Count == 0) return center;

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


	public static List<GameObject> GetObjectsFrom(GameObject center, List<GameObject> positions, float angle)
	{
		if (positions.Count == 0) return new List<GameObject>() { center };
		
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < positions.Count; i++)
		{
			float calcAngle = Mathf.Abs(Vector3.SignedAngle(center.transform.forward, (positions[i].transform.position - center.transform.position).normalized, center.transform.up));

			if (calcAngle >= angle)
				list.Add(positions[i]);
		}

		return list;
	}

	public static GameObject GetObjectFrom(GameObject center, List<GameObject> positions, float angle)
	{
		if (positions.Count == 0) return center;

		List<GameObject> gos = GetObjectsFrom(center, positions, angle);
		return gos[Random.Range(0, gos.Count)];
	}
}
