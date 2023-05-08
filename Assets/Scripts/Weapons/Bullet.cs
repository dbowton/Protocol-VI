using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
	float damage = 0;
	List<string> targetTags = new List<string>();

	[SerializeField] GameObject hitPrefab;

	public void Init(float damage, string targetTag, float projectileSpeed)
	{
		this.Init(damage, new List<string>() { targetTag }, projectileSpeed);
	}

	public void Init(float damage, List<string> targets, float projectileSpeed)
	{
		this.damage = damage;
		this.targetTags = targets;
		this.GetComponent<Rigidbody>().AddForce(this.transform.forward * projectileSpeed, ForceMode.Acceleration);
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (targetTags.Contains(collision.transform.tag) && 
			(collision.transform.TryGetComponent<Health>(out Health health) || 
			collision.transform.root.TryGetComponent<Health>(out health)))
		{
			if (hitPrefab)
				Destroy(Instantiate(hitPrefab, collision.GetContact(0).point, Quaternion.identity), 0.25f);

			health.Damage(damage);
			Destroy(this.gameObject);
		}
	}
}
