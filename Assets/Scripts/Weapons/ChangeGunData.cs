using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GunData;

public class ChangeGunData : MonoBehaviour
{
	[SerializeField] GunData gunData;

	[SerializeField] bool changeFireType = false;
	[SerializeField] FireType fireType = FireType.Semi_Auto;
	[SerializeField] bool changeGunPrefab = false;
	[SerializeField] int gunPrefabIndex = 0;
	[SerializeField] bool changeSightPrefab = false;
	[SerializeField] int sightPrefabIndex = -1;
	[SerializeField] bool changeMagPrefab = false;
	[SerializeField] int magPrefabIndex = -1;
	[SerializeField] bool changeBulletPrefab = false;
	[SerializeField] int bulletPrefabIndex = -1;

	[Header("Reciever Changes")]

	[SerializeField] bool changeAmmo = false;
	[SerializeField] float ammoCap = 0;
	[SerializeField] bool changeFireRate = false;
	[SerializeField] float fireRate = 0;
	[SerializeField] bool changeDamage = false;
	[SerializeField] float damage = 0;
	[SerializeField] bool changeRange = false;
	[SerializeField] float range = 0;

	public void UpdateInfo()
	{
		if (changeFireType)
			gunData.UpdateFireMode(fireType);
		if(changeGunPrefab)
			gunData.UpdateModel(gunPrefabIndex);
		if(changeSightPrefab)
			gunData.UpdateSight(sightPrefabIndex);
		if (changeMagPrefab)
			gunData.UpdateMag(magPrefabIndex);
		if(changeBulletPrefab)
			gunData.UpdateBullet(bulletPrefabIndex);

		if (changeAmmo)
			gunData.modifications.ammoCap = ammoCap;
		if (changeFireRate)
			gunData.modifications.fireRate = fireRate;
		if (changeDamage)
			gunData.modifications.damage = damage;
		if (changeRange)
			gunData.modifications.range = range;
	}
}
