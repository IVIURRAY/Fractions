using System;
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

	[SerializeField]
	private int stoneCount = 0;
	[SerializeField]
	private int woodCount = 0;
	[SerializeField]
	private int foodCount = 0;

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

	private void AddStone() => stoneCount += 1;
	private void AddWood() => woodCount += 1;
	private void AddFood() => foodCount += 1;

	public int GetStoneCount() => stoneCount;
	public int GetWoodCount() => woodCount;
	public int GetFoodCount() => foodCount;

	internal void DepositResouce(GameObject resource)
	{
		if (resource.CompareTag("WoodResource"))
			AddWood();
		else if (resource.CompareTag("StoneResource"))
			AddStone();
		else if (resource.CompareTag("FoodResource"))
			AddFood();

		Destroy(resource);
	}
}
