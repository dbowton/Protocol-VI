using UnityEngine;

[CreateAssetMenu(fileName = "SavedPosition", menuName = "SaveData/HolsterData")]
public class GunData : ScriptableObject
{
	[SerializeField] GunPrefabsAndAttachments gunPrefabs;
	[SerializeField] int holsterLevel = 0;

	public Vector3 holsterPosition;

	[SerializeField] FireType defaultFireType;
	public FireType fireType;

	int gunPrefabIndex = 0;
	int sightPrefabIndex = -1;
	int magPrefabIndex = -1;
	int bulletPrefabIndex = -1;

	[SerializeField] int defaultGunPrefabIndex = 0;
	[SerializeField] int defaultSightPrefabIndex = -1;
	[SerializeField] int defaultMagPrefabIndex = -1;
	[SerializeField] int defaultBulletPrefabIndex = -1;

	public GameObject GunPrefab 
	{ 
		get 
		{
			if (gunPrefabIndex == -1) return null;
			if (holsterLevel == 0 && gunPrefabIndex >= gunPrefabs.primaryGunPrefabs.Count) return null;
			if (holsterLevel == 1 && gunPrefabIndex >= gunPrefabs.secondaryGunPrefabs.Count) return null;
			if (holsterLevel > 1) return null;

			if (holsterLevel == 0) return gunPrefabs.primaryGunPrefabs[gunPrefabIndex];
			if (holsterLevel == 1) return gunPrefabs.secondaryGunPrefabs[gunPrefabIndex];
			return null;
		}
	}

	public GameObject SightPrefab
	{
		get
		{
			if (sightPrefabIndex == -1) return null;
			if (sightPrefabIndex >= gunPrefabs.SightPrefabs.Count) return null;

			return gunPrefabs.SightPrefabs[sightPrefabIndex];
		}
	}
	public GameObject MagPrefab
	{
		get
		{
			if (magPrefabIndex == -1) return null;
			if (magPrefabIndex >= gunPrefabs.MagPrefabs.Count) return null;

			return gunPrefabs.MagPrefabs[magPrefabIndex];
		}
	}
	public GameObject BulletPrefab
	{
		get
		{
			if (bulletPrefabIndex == -1) return null;
			if (bulletPrefabIndex >= gunPrefabs.BulletPrefabs.Count) return null;

			return gunPrefabs.BulletPrefabs[bulletPrefabIndex];
		}
	}

	public GunMod modifications;

	public enum FireType
	{ 
		Semi_Auto,
		Full_Auto,
		Burst
	}

	public void Enable()
	{
		float posX = PlayerPrefs.HasKey(name + ".xPos") ? PlayerPrefs.GetFloat(name + ".xPos") : 0;
		float posY = PlayerPrefs.HasKey(name + ".yPos") ? PlayerPrefs.GetFloat(name + ".yPos") : 0;
		float posZ = PlayerPrefs.HasKey(name + ".zPos") ? PlayerPrefs.GetFloat(name + ".zPos") : 0;

		holsterPosition = new Vector3(posX, posY, posZ);

		gunPrefabIndex	  = PlayerPrefs.HasKey(name + ".gunPrefabIndex") ? PlayerPrefs.GetInt(name + ".gunPrefabIndex") : defaultGunPrefabIndex;
		sightPrefabIndex  = PlayerPrefs.HasKey(name + ".sightPrefabIndex") ? PlayerPrefs.GetInt(name + ".sightPrefabIndex") : defaultSightPrefabIndex;
		magPrefabIndex	  = PlayerPrefs.HasKey(name + ".magPrefabIndex") ? PlayerPrefs.GetInt(name + ".sightPrefabIndex") : defaultMagPrefabIndex;
		bulletPrefabIndex = PlayerPrefs.HasKey(name + ".bulletPrefabIndex") ? PlayerPrefs.GetInt(name + ".bulletPrefabIndex") : defaultBulletPrefabIndex;

		fireType = PlayerPrefs.HasKey(name + ".fireType") ? (FireType)PlayerPrefs.GetInt(name + ".fireType") : defaultFireType;
	}

	public void SetupReciever(Reciever reciever)
	{
		reciever.fireType = fireType;
		modifications.Enable(reciever);

		if (reciever.sightTransform && SightPrefab) Instantiate(SightPrefab, reciever.sightTransform);
		if (reciever.magTransform && MagPrefab) Instantiate(MagPrefab, reciever.magTransform);
	}

	public void UpdatePos(Vector3 pos)
	{
		holsterPosition = pos;

		PlayerPrefs.SetFloat(name + ".xPos", pos.x);
		PlayerPrefs.SetFloat(name + ".yPos", pos.y);
		PlayerPrefs.SetFloat(name + ".zPos", pos.z);
	}

	public void UpdateModel(int modelIndex)
	{
		if (((holsterLevel == 0 && modelIndex >= gunPrefabs.primaryGunPrefabs.Count) || 
				(holsterLevel == 1 && modelIndex >= gunPrefabs.secondaryGunPrefabs.Count)) || modelIndex < 0) modelIndex = 0;

		gunPrefabIndex = modelIndex;
		PlayerPrefs.SetInt(name + ".gunPrefabIndex", modelIndex);
	}
	public void UpdateSight(int sightIndex)
	{
		if (sightIndex >= gunPrefabs.SightPrefabs.Count || sightIndex < 0) sightIndex = -1;

		sightPrefabIndex = sightIndex;
		PlayerPrefs.SetInt(name + ".sightPrefabIndex", sightIndex);
	}
	public void UpdateMag(int magIndex)
	{
		if (magIndex >= gunPrefabs.MagPrefabs.Count || magIndex < 0) magIndex = -1;

		magPrefabIndex = magIndex;
		PlayerPrefs.SetInt(name + ".magPrefabIndex", magIndex);
	}
	public void UpdateBullet(int bulletIndex)
	{
		if(bulletIndex >= gunPrefabs.BulletPrefabs.Count || bulletIndex < 0) bulletIndex = -1;

		bulletPrefabIndex = bulletIndex;
		PlayerPrefs.SetInt(name + ".bulletPrefabIndex", bulletIndex);
	}
	public void UpdateFireMode(FireType fireType)
	{
		this.fireType = fireType;
		PlayerPrefs.SetInt(name + ".fireType", (int)fireType);
	}
}
