using System;
using System.Collections.Generic;
using UnityEngine;

using VRButton = UnityEngine.XR.CommonUsages;

[RequireComponent(typeof(Rigidbody))]
public class Gun : MonoBehaviour
{
    [SerializeField] GameObject barrel;
    [SerializeField] LaserSight sight;

	[SerializeField] int baseAmmo = 1;
	private int ammoCount = 0;
	[SerializeField] float baseFireRate = 0.25f;
    float fireRate = 0;
    [SerializeField] float baseRange = 20f;
    float range = 0;
    [SerializeField] float baseDamage = 2f;
    float damage = 0;

    [SerializeField] float hapticIntensity = 0.35f;
    [SerializeField] float hapticDuration = 0.5f;

	[SerializeField] List<AudioClip> fireSound;
    [SerializeField] List<AudioClip> missfireSound;

    [SerializeField] Vector3 heldPos;
    [SerializeField] Vector3 heldAngle;

    public GameObject Barrel { get { return barrel; } }

	Nullable<UnityEngine.XR.InputDevice> input;
    Timer fireTimer;

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
	}

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

	public void UpdateGun()
    {
        if (input.Value.GetControllerPressed(VRButton.triggerButton, out bool fire) && fire) Fire();
    }

    private void Fire()
    {
        if (fireTimer.IsOver)
        {
            if (ammoCount <= 0)
                AudioSource.PlayClipAtPoint(missfireSound[UnityEngine.Random.Range(0, missfireSound.Count)], transform.position);
            else
            {
                ammoCount--;
                DryFire();
                input.Value.SendHaptic(hapticIntensity, hapticDuration);
            }

            fireTimer.Reset();
        }
    }

    public void DryFire()
    {
		AudioSource.PlayClipAtPoint(fireSound[UnityEngine.Random.Range(0, fireSound.Count)], transform.position);

		if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out RaycastHit hitInfo, range))
			if (hitInfo.collider.TryGetComponent<Health>(out Health health))
				health.Damage(damage);
	}

	public void EnableGun()
    {
        if (ControllerManager.leftController.heldPoint == gameObject)
        {
            input = ControllerManager.leftInput;
            transform.parent = ControllerManager.leftController.transform;
            ControllerManager.leftController.controllerObject.SetActive(false);
        }
        else
        {
			transform.parent = ControllerManager.rightController.transform;
			input = ControllerManager.rightInput;
			ControllerManager.rightController.controllerObject.SetActive(false);
		}

        GetComponent<Rigidbody>().useGravity = false;

        transform.localPosition = heldPos;
        transform.localEulerAngles = heldAngle;

        fireTimer = new Timer(fireRate);
        fireTimer.End();
	}
	public void DisableGun()
    {
		if (ControllerManager.leftController.heldPoint == gameObject)
            ControllerManager.leftController.controllerObject.SetActive(true);
		else
			ControllerManager.rightController.controllerObject.SetActive(true);

		fireTimer.Remove();
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        if(ammoCount == 0) Destroy(GetComponent<GrabPoint>(), 3.5f);
        input = null;
    }
}
