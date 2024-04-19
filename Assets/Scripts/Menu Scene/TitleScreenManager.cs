using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TitleScreenManager : MonoBehaviour
{
    public void StartNetworkAsHost()
    {
        NetworkManager.Singleton.StartHost();

    }

    public void StartNewGame()
    {

        if (WorldSaveGameManager.instance == null)
        {
            Debug.LogError("WorldSaveGameManager.instance is null");
            return;
        }
        StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
    }
}
