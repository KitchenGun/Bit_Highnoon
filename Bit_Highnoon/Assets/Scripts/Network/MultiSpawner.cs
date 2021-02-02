using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

public class MultiSpawner : MonoBehaviourPunCallbacks
{
	Spawnpoint[] spawnpoints;

	// Start is called before the first frame update
	public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
		spawnpoints = GetComponentsInChildren<Spawnpoint>();
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
			if (PhotonNetwork.IsMasterClient)
				PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), spawnpoints[0].transform.position, spawnpoints[0].transform.localRotation);
			else
				PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), spawnpoints[1].transform.position, spawnpoints[1].transform.localRotation);
		}
	}
}
