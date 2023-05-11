using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
	[HideInInspector] public GameObject target;

	[SerializeField] Animator animator;
	NavMeshAgent agent;


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
	}

	public void Die()
	{
		agent.isStopped = true;

		foreach (var c in gameObject.GetComponentsInChildren<Collider>())
			Destroy(c);
	}
}
