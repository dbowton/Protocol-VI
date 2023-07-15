using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	[SerializeField] float damage = 10f;
	[SerializeField] float radius = 2.5f;
	[SerializeField] float delay = 1.5f;

	[SerializeField] GameObject ExplosionPrefab;
	[SerializeField] float explosionTime = 0.25f;

	bool prepped = false;

	public void Prep()
	{
		if(prepped) return;
		prepped = true;

		new Timer(delay, () => Explode(), true);
	}

	private void Explode()
	{
		List<Collider> hits = Physics.OverlapSphere(transform.position, radius).ToList();

		foreach (var hit in hits)
			if (hit.TryGetComponent<Health>(out Health health)) health.Damage(damage);

		if(ExplosionPrefab)
			Destroy(Instantiate(ExplosionPrefab, transform.position, Quaternion.identity), explosionTime);
	}
}
