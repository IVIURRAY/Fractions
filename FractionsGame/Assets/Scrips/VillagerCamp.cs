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

	private GameObject GetHome() {
		return home;
	}

	private GameObject GetFoodStore()
	{
		return foodStore;
	}

	private GameObject GetStoneStore()
	{
		return stoneStore;
	}

	private GameObject GetWoodStore()
	{
		return woodStore;
	}

	public GameObject GetResourcesHome(GameObject resource)
	{
		GameObject store = null;
		if (resource.CompareTag("WoodResource"))
			store = GetWoodStore();
		else if (resource.CompareTag("StoneResource"))
			store = GetStoneStore();
		else if (resource.CompareTag("FoodResource"))
			store = GetFoodStore();
		else
			GetHome();

		return store;
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
