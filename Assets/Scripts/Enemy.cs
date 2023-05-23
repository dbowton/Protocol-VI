using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
	public GameObject target;

	[SerializeField] float distanceBuffer = 0.25f;
	[SerializeField] Animator animator;
	NavMeshAgent agent;

	[SerializeField] float attackDelay = 1.25f;
	[SerializeField] int attackIndexCount = 2;
	float timeAccumulator = 0;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		if(animator)
			animator.SetFloat("speed", agent.velocity.magnitude);

		if(target)
			agent.SetDestination(target.transform.position);

		if(timeAccumulator < attackDelay) timeAccumulator += Time.deltaTime;

		if (timeAccumulator >= attackDelay && target && Vector3.Distance(transform.position, target.transform.position) < agent.stoppingDistance + distanceBuffer)
		{
			timeAccumulator -= attackDelay;
			animator.SetInteger("attackIndex", Random.Range(0, attackIndexCount));
			animator.SetTrigger("attack");
		}
	}

	public void Die()
	{
		agent.isStopped = true;

		foreach (var c in gameObject.GetComponentsInChildren<Collider>())
			Destroy(c);
	}
}
