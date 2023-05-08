using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSight : MonoBehaviour
{
	[SerializeField] Color color = Color.red;
	[SerializeField] float width = 0.1f;
	[SerializeField] GameObject laserContactPrefab;
	GameObject laserContact = null;
	LineRenderer lineRenderer;
	[SerializeField] GameObject laserStart;

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.material.color = color;
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;
	}

	public void UpdateLaser(Reciever gun)
	{
		lineRenderer.SetPosition(0, laserStart.transform.position);

		Nullable<RaycastHit> target = GetTarget(gun.Barrel);

		if (target == null)
		{
			lineRenderer.SetPosition(1, gun.Barrel.position + gun.Barrel.forward * 25f);
			Destroy(laserContact);
			laserContact = null;
		}
		else
		{
			lineRenderer.SetPosition(1, target.Value.point);

			if (laserContact == null)
			{
				laserContact = Instantiate(laserContactPrefab);
				laserContact.GetComponent<Renderer>().material.color = color;
			}

			laserContact.transform.position = target.Value.point;
			laserContact.transform.LookAt(gun.Barrel, Vector3.up);
		}
	}

	public void DisableLaser()
	{
		if(laserContact) Destroy(laserContact);
	}

	private Nullable<RaycastHit> GetTarget(Transform barrel)
	{
		if (Physics.Raycast(barrel.position, barrel.forward, out RaycastHit hitInfo))
			return hitInfo;

		return null;
	}
}
