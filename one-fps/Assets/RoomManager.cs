using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;

    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }


   public override void OnEnable()
    {
       base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public override void OnDisable()
    {
       base .OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
       if(scene.buildIndex == 1) 
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerManager"),Vector3.zero, Quaternion.identity);
        }
    }

  
}
