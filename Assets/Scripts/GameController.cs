using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<GameObject> mapPrefabList;
    private List<GameObject> mapInGameList;

    public GameObject endGameUI;

    private PlayerInputManager playerInputManager;
    public int maxMaps = 3;

    public static GameController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

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
        playerInputManager.EnableJoining();
    }

    public void HideEverything()
    {
        for (int i = 0; i < maxMaps; i++)
        {
            mapInGameList[i].SetActive(false);
        }
        foreach (GameObject player in PlayersTracker.Instance.playerObjects)
        {
            Destroy(player);
        }
        PlayersTracker.Instance.playerObjects.RemoveAll(x => true);
    }

    public void EndGame(float index, Color color)
    {
        playerInputManager.DisableJoining();

        endGameUI.SetActive(true);
        Text winText = endGameUI.transform.Find("Win Text").GetComponent<Text>();
        winText.text = "Player " + (index+1) + " wins!";
        winText.color = color;
    }
}
