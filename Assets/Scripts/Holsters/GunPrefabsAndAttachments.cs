using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedPosition", menuName = "SaveData/GunAttatchments")]
public class GunPrefabsAndAttachments : ScriptableObject
{
	public List<GameObject> primaryGunPrefabs;
	public List<GameObject> secondaryGunPrefabs;

	public List<GameObject> SightPrefabs;
	public List<GameObject> MagPrefabs;
	public List<GameObject> BulletPrefabs;
}
