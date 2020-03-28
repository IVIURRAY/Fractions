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
	private List<GameObject> resources = new List<GameObject>();
	private float timer;
	private bool walkingHome;
	private Transform holdingArea;
	private GameObject holdingResource;
	public VillagerCamp villagerCamp;
	private Animator animator;


	public void SetCamp(VillagerCamp camp) {
		villagerCamp = camp;
	}

	// Use this for initialization
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		timer = wanderTimer;
		walkingHome = false;
		holdingArea = gameObject.transform.Find("HoldingArea");
		resources = FindAllResources();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (walkingHome)
			WalkHome();

		if (holdingResource)
			HoldResource(holdingResource);

		ForageForResources();
	}

	private void ForageForResources()
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
	}

	private List<GameObject> FindAllResources() 
	{
		GameObject[] wood = GameObject.FindGameObjectsWithTag("WoodResource");
		GameObject[] stone = GameObject.FindGameObjectsWithTag("StoneResource");
		GameObject[] food = GameObject.FindGameObjectsWithTag("FoodResource");

		resources.AddRange(wood);
		resources.AddRange(stone);
		resources.AddRange(food);

		return resources;

	}

	private void WalkHome()
	{
		float distanceToHome = Vector3.Distance(transform.position, villagerCamp.GetHome().transform.position);
		if (distanceToHome < 1)
		{
			villagerCamp.DepositResouce(holdingResource);
			resources.Remove(holdingResource);
			walkingHome = false;
			holdingResource = null;
			animator.SetBool("IsCarrying", false);
		}

	}

	private void MoveToResource(GameObject resource)
	{
		float distance = Vector3.Distance(transform.position, resource.transform.position);
		agent.SetDestination(resource.transform.position);
		if (distance < 1)
		{
			TakeResourceToCamp(resource);
		}
	}

	private void TakeResourceToCamp(GameObject resource)
	{
		Resource res = resource.GetComponent<Resource>();
		res.IsAqurired = true;

		HoldResource(resource);
		walkingHome = true;
		if (resource.CompareTag("WoodResource"))
		{
			agent.SetDestination(villagerCamp.GetWoodStore().transform.position);
		}
		else if (resource.CompareTag("StoneResource"))
		{
			agent.SetDestination(villagerCamp.GetStoneStore().transform.position);
		}
		else if (resource.CompareTag("FoodResource"))
		{
			agent.SetDestination(villagerCamp.GetFoodStore().transform.position);
		}
		else
		{
			agent.SetDestination(villagerCamp.GetHome().transform.position);
		}

	}

	private void HoldResource(GameObject resource)
	{
		animator.SetBool("IsCarrying", true);
		holdingResource = resource;
		resource.transform.position = holdingArea.position;
	}

	private bool IsCloseToResource()
	{
		GameObject closest = FindClosestResource();
		if (closest.GetComponent<Resource>().IsAqurired)
			return false; // Dont move to resouce if someone has it

		if (Vector3.Distance(FindClosestResource().transform.position, transform.position) < 4)
			return true;

		return false;
	}

	private GameObject FindClosestResource() {
		float distance = Mathf.Infinity;
		GameObject closest = null;

		foreach (GameObject resource in resources)
		{
			if (resource != null)
			{
				float distanceToResource = Vector3.Distance(resource.gameObject.transform.position, transform.position);
				if (distanceToResource < distance)
				{
					distance = distanceToResource;
					closest = resource;
				}
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
