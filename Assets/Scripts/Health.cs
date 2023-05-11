using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	[SerializeField] float maxHealth;
	public float MaxHealth { get { return maxHealth; } }

	private float currentHealth;
	public float CurrentHealth { get { return currentHealth; } }

	[System.Serializable]
	public class HealthThreshold
	{
		[SerializeField][Tooltip("Inclusive")][Range(0,1)] public float thresholdMin;
		[SerializeField][Tooltip("Exclusive")][Range(0, 1)] public float thresholdMax;

		public UnityEvent OnDamage;
		public UnityEvent OnHeal;
	}

	[SerializeField][Tooltip("Not Used On Lethal Damage")] List<HealthThreshold> healthThresholds;
	public UnityEvent OnDeath;

	private void Start()
	{
		currentHealth = maxHealth;
	}

	public void Heal(float val)
	{
		if (val < 0)
		{
			Damage(val); 
			return;
		}

		currentHealth = Mathf.Min(currentHealth + val, maxHealth);
		foreach (var threshold in healthThresholds)
			if (currentHealth / maxHealth <= threshold.thresholdMax && threshold.thresholdMin <= currentHealth / maxHealth)
				threshold.OnHeal.Invoke();
	}

	public void Damage(float val)
	{
		if (val < 0)
		{
			Heal(val);
			return;
		}

		if (currentHealth > 0)
		{
			currentHealth -= val;

			if (currentHealth <= 0)
				OnDeath.Invoke();
			else
				foreach (var threshold in healthThresholds)
					if (currentHealth / maxHealth <= threshold.thresholdMax && threshold.thresholdMin <= currentHealth / maxHealth)
						threshold.OnDamage.Invoke();
		}
	}

	public void FullHeal()
	{
		Heal(Mathf.Abs(currentHealth) + maxHealth);
	}
}
