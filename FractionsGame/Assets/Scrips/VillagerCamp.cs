using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerCamp : MonoBehaviour
{
	[SerializeField]
	GameObject home;
	[SerializeField]
	GameObject foodStore;
	[SerializeField]
	GameObject woodStore;
	[SerializeField]
	GameObject stoneStore;

	private void Start()
	{
		home = gameObject;
		foodStore = gameObject;
		woodStore = gameObject;
		stoneStore = gameObject;
	}

	public GameObject GetHome() {
		return home;
	}

	public GameObject GetFoodStore()
	{
		return foodStore;
	}

	public GameObject GetStoneStore()
	{
		return stoneStore;
	}

	public GameObject GetWoodStore()
	{
		return woodStore;
	}
}
