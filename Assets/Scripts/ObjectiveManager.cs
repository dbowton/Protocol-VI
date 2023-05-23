using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
	[SerializeField] Spawner spawner;

	[SerializeField] int killEnemies = 0;
	[SerializeField] float surviveTime = 0;
	int completedObjectives = 0;

	bool complete = false;

	private void Start()
	{
		if (killEnemies <= 0) completedObjectives++;
		if(surviveTime <= 0) completedObjectives++;
	}

	private void Update()
	{
		if (surviveTime > 0)
		{
			surviveTime -= Time.deltaTime;

			if(surviveTime <= 0) 
				completedObjectives++;
		}

		if(!complete && completedObjectives >= 2)
		{
			complete = true;
			spawner.StopSpawning();
		}

		if (complete && spawner.enemyCount == 0)
		{
			//	end level
		}
	}

	public void EnemyKilled()
	{
		if (killEnemies > 0)
		{
			killEnemies--;

			if (killEnemies <= 0)
				completedObjectives++;
		}
	}
}
