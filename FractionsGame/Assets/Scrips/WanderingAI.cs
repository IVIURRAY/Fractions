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

	// Use this for initialization
	void OnEnable()
	{
		agent = GetComponent<NavMeshAgent>();
		timer = wanderTimer;
		resources = GameObject.FindGameObjectsWithTag("Resource");
	}

	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= wanderTimer)
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

	private void MoveToResource(GameObject resource)
	{
		print("Planning on moving toward resource");
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
