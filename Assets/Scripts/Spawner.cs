using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();
	[SerializeField] List<GameObject> spawnPoints = new List<GameObject>();

	[SerializeField] GameObject player;
	[SerializeField] int maxEnemies = -1;
	[SerializeField] float spawnTime = 0.5f;

	[HideInInspector] public int enemyCount = 0;

	[SerializeField] ObjectiveManager objectiveManager;

	Camera mainCam;
	Timer spawnTimer = null;

	private void Start()
	{
		mainCam = Camera.main;

		spawnTimer = new Timer(spawnTime, () => SpawnEnemy(), false, true);
	}

	public void SpawnEnemy()
	{
		if (maxEnemies != -1 && enemyCount >= maxEnemies) return;

		GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], 
			DirectionalObject.GetObjectFrom(mainCam.gameObject, spawnPoints, 120f).transform.position, Quaternion.identity);

		enemy.GetComponent<Enemy>().target = player;
		enemy.GetComponent<Health>().OnDeath.AddListener(() => enemyCount--);
		enemy.GetComponent<Health>().OnDeath.AddListener(() => objectiveManager.EnemyKilled());

		enemyCount++;
	}

	public void StopSpawning()
	{
		spawnTimer?.Remove();
	}
}
