using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRButton = UnityEngine.XR.CommonUsages;

[RequireComponent(typeof(Rigidbody))]
public class Reciever : MonoBehaviour
{
	[SerializeField] int baseAmmo = 1;
	private int ammoCount = 0;
	[SerializeField] float baseFireRate = 0.25f;
	float fireRate = 0;
	[SerializeField] float baseRange = 20f;
	float range = 0;
	[SerializeField] float baseDamage = 2f;
	float damage = 0;

	public void ModifyAmmo(float val)
	{
		ammoCount += (int)(baseAmmo * val);
	}
	public void ModifyFireRate(float val)
	{
		fireRate += (int)(baseFireRate * val);
	}
	public void ModifyRange(float val)
	{
		range += (int)(baseFireRate * val);
	}
	public void ModifyDamage(float val)
	{
		damage += (int)(baseDamage * val);
	}

	[SerializeField] Transform barrel;
	public Transform magTransform;
	public Transform sightTransform;

	public Transform Barrel { get { return barrel; } }

	[SerializeField] float hapticIntensity = 0.35f;
	[SerializeField] float hapticDuration = 0.5f;

	[SerializeField] List<AudioClip> fireSounds;
	[SerializeField] List<AudioClip> missfireSounds;

	[SerializeField] Animator animator;
	[SerializeField] List<ParticleSystem> muzzleFlashes;

	[SerializeField] GameObject leftUI;
	[SerializeField] GameObject rightUI;

	[SerializeField] float projectileSpeed = 1000f;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] GameObject casingPrefab;
	[SerializeField] Transform casingSpawn;

	Timer fireTimer = null;
	Timer destroyTimer = null;
	bool fireable = true;

	Nullable<UnityEngine.XR.InputDevice> input;

	private void Awake()
	{
		ammoCount = 0;
		fireRate = 0;
		range = 0;
		damage = 0;
	}
	private void Start()
	{
		ammoCount += baseAmmo;
		fireRate += baseFireRate;
		range += baseRange;
		damage += baseDamage;

		leftUI.SetActive(false);
		rightUI.SetActive(false);
	}

	[HideInInspector] public GunData.FireType fireType = GunData.FireType.Semi_Auto;

	public void Enable()
	{
		if (ControllerManager.leftController.heldPoint && ControllerManager.leftController.heldPoint.gameObject == gameObject)
		{
			rightUI.SetActive(true);
			input = ControllerManager.leftInput;
		}
		else
		{
			leftUI.SetActive(true);
			input = ControllerManager.rightInput;
		}

		destroyTimer?.Remove();
		destroyTimer = null;
	}
	public void UpdateGun()
	{
		leftUI.GetComponent<WeaponUIManager>().UpdateBulletCount(ammoCount);
		rightUI.GetComponent<WeaponUIManager>().UpdateBulletCount(ammoCount);

		switch (fireType)
		{
			case GunData.FireType.Semi_Auto:
				SemiAutoUpdate();
				break;
			case GunData.FireType.Full_Auto:
				FullAutoUpdate();
				break;
			case GunData.FireType.Burst:
				BurstFireUpdate();
				break;
		}
	}
	public void Disable()
	{
		fireTimer?.Remove();

		leftUI.SetActive(false);
		rightUI.SetActive(false);

		if (ammoCount == 0)
		{
			Destroy(transform.GetComponent<GrabPoint>());
			Destroy(gameObject, 1.75f);
		}
		else
			destroyTimer = new Timer(5f, () => Destroy(gameObject), true);
		
		input = null;
	}

	public void Fire()
	{
		if (ammoCount <= 0)
			AudioSource.PlayClipAtPoint(missfireSounds[UnityEngine.Random.Range(0, missfireSounds.Count)], transform.position);
		else
		{
			ammoCount--;
			Shoot();
			input.Value.SendHaptic(hapticIntensity, hapticDuration);
		}
	}

	public void Shoot()
	{
		if(animator)
			animator.SetTrigger("Fire");
		foreach (var flash in muzzleFlashes)
			flash.Play();

		AudioSource.PlayClipAtPoint(fireSounds[UnityEngine.Random.Range(0, fireSounds.Count)], transform.position);
		if (casingSpawn && casingPrefab)
		{
			GameObject casing = Instantiate(casingPrefab, casingSpawn);
			casing.transform.parent = null;
			casing.GetComponent<Rigidbody>().AddForce(-casing.transform.forward * UnityEngine.Random.Range(1f, 3.5f), ForceMode.Impulse);
			casing.GetComponent<Rigidbody>().AddForce(casing.transform.right * UnityEngine.Random.Range(1.5f, 4.5f), ForceMode.Impulse);
			casing.GetComponent<Rigidbody>().AddForce(casing.transform.up * UnityEngine.Random.Range(2f, 4f), ForceMode.Impulse);

			casing.GetComponent<Rigidbody>().AddTorque(casing.transform.up * UnityEngine.Random.Range(3f,15f), ForceMode.Impulse);

			Destroy(casing, 2.5f);
		}

		if (bulletPrefab)
		{
			GameObject bullet = Instantiate(bulletPrefab, barrel);
			bullet.transform.forward = barrel.transform.forward;

			for (int i = 0; i < bullet.transform.childCount;)
			{
				Transform pellet = bullet.transform.GetChild(0);
				pellet.transform.parent = null;
				pellet.GetComponent<Bullet>().Init(damage, new List<string>() { "Target", "Enemy" }, projectileSpeed);

				Destroy(pellet.gameObject, 2f);
			}

			Destroy(bullet);
		}
		else
		{
			if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out RaycastHit hitInfo, range))
				if (hitInfo.collider.TryGetComponent<Health>(out Health health))
					health.Damage(damage);
		}
	}

	private void SemiAutoUpdate()
	{
		if (input.Value.GetControllerPressed(VRButton.triggerButton, out bool pressed) && pressed)
		{
			if (fireable && (fireTimer == null || fireTimer.IsOver))
			{
				fireTimer?.Remove();
				fireTimer = new Timer(fireRate);
				fireable = false;

				Fire();
			}
		}
		else
		{
			fireable = true;
		}
	}

	private void FullAutoUpdate()
	{
		if (input.Value.GetControllerPressed(VRButton.triggerButton, out bool pressed) && pressed)
		{
			if (fireTimer == null || fireTimer.IsOver)
			{
				Fire();
				fireTimer = new Timer(fireRate);
			}
		}
		else
		{
			fireTimer?.Remove();
			fireTimer = null;
		}
	}

	int fireCounter = 0;
	private void BurstFireUpdate()
	{
		if (input.Value.GetControllerPressed(VRButton.triggerButton, out bool pressed) && pressed)
		{
			if (fireTimer == null)
			{
				Fire();
				fireCounter++;
				fireTimer = new Timer(fireRate, () => 
				{
					Fire();
					fireCounter++;
					if (fireCounter > 2)
					{
						fireCounter = 0;
						fireTimer.Remove();
						fireTimer = null;
					}
				}, false, true);
			}
		}
	}
}
