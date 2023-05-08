using UnityEngine;

[System.Serializable]
public class GunMod
{
	public float ammoCap = 0;
	public float fireRate = 0;
	public float damage = 0;
	public float range = 0;

	public void Enable(Reciever reciever)
	{
		reciever.ModifyAmmo(ammoCap);
		reciever.ModifyFireRate(fireRate);
		reciever.ModifyRange(range);
		reciever.ModifyDamage(damage);
	}
}
