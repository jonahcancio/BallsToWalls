using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public List<GameObject> mapPrefabList;
    private List<GameObject> mapInGameList;

    private PlayerInputManager playerInputManager;
    public int maxMaps = 3;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        mapInGameList = new List<GameObject>();
        for (int i = 0; i < maxMaps; i++)
        {
            GameObject map = Instantiate(mapPrefabList[i], transform);
            map.SetActive(false);
            mapInGameList.Add(map);
        }
    }
    public void SpawnMap(int index)
    {
        mapInGameList[index].SetActive(true);
        playerInputManager.enabled = true;
    }

    public void hideMaps()
    {
        for (int i = 0; i < maxMaps; i++)
        {
            mapInGameList[i].SetActive(false);
        }
    }
}
