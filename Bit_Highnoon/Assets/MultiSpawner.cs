﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

public class MultiSpawner : MonoBehaviourPunCallbacks
{
	private Transform[] Spawners;

	// Start is called before the first frame update
	public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
		Spawners = GetComponentsInChildren<Transform>();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if (scene.buildIndex == 8) // We're in the game scene
		{
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), transform.position, transform.rotation);
		}
	}
}
