using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{

	public float wanderRadius;
	public float wanderTimer;

	private Transform target;
	private NavMeshAgent agent;
	private GameObject[] resources;
	private float timer;
	private bool walkingHome;
	public Vector3 home;

	public void SetHome(Vector3 homePosition) {
		home = homePosition;
	}

	// Use this for initialization
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		timer = wanderTimer;
		walkingHome = false;
		resources = GameObject.FindGameObjectsWithTag("Resource");
	}

	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= wanderTimer && !walkingHome)
		{
			if (IsCloseToResource())
			{
				MoveToResource(FindClosestResource());
			}
			else
			{
				MoveToRandomLocation();
			}
		}

		if (walkingHome)
			walkingHome = Vector3.Distance(transform.position, home) > 1;

	}

	private void MoveToResource(GameObject resource)
	{
		float distance = Vector3.Distance(transform.position, resource.transform.position);
		agent.SetDestination(resource.transform.position);
		if (distance < 1) {
			Destroy(resource);
			walkingHome = true;
			agent.SetDestination(home);
		}
	}

	private bool IsCloseToResource()
	{
		if (Vector3.Distance(FindClosestResource().transform.position, transform.position) < 4)
			return true;

		return false;
	}

	private GameObject FindClosestResource() {
		float distance = Mathf.Infinity;
		GameObject closest = null;

		foreach (GameObject resource in resources)
		{
			if (resource == null)
				continue;

			float distanceToResource = Vector3.Distance(resource.gameObject.transform.position, transform.position);
			if (distanceToResource < distance)
			{
				distance = distanceToResource;
				closest = resource;
			}
		}

		return closest;
	}

	private void MoveToRandomLocation()
	{
		Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
		agent.SetDestination(newPos);
		timer = 0;
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
	{
		Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

		randDirection += origin;

		NavMeshHit navHit;

		NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

		return navHit.position;
	}
}
