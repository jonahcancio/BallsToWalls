using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayersTracker : MonoBehaviour
{
    [System.Serializable]
    public class PlayerSetting
    {
        public Color color;
        public RuntimeAnimatorController animController;
    }

    public List<PlayerSetting> playerSettings;
    public List<GameObject> playerObjects;
    private bool isGameRunning;

    public static PlayersTracker Instance;
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
        isGameRunning = false;
        playerObjects = new List<GameObject>();
    }
    public void RegisterPlayer(PlayerInput pInput)
    {
        PlayerData pHealth = pInput.GetComponent<PlayerData>();
        
        pHealth.PlayerDie.AddListener(KillPlayer);        
        pInput.transform.SetParent(transform);

        int index = pInput.playerIndex;
        pHealth.playerIndex = index;
        Debug.Log("Player " + index + " has spawned");
        playerObjects.Insert(index, pInput.gameObject);

        if (!isGameRunning && playerObjects.Count >= 2)
        {
            Debug.Log("GAME HAS STARTED");
            isGameRunning = true;
        }
    }
    public void ColorizePlayer(PlayerInput pInput)
    {
        int index = pInput.playerIndex;

        SpriteRenderer bodySprite = pInput.transform.Find("PlayerBody").GetComponent<SpriteRenderer>();
        SpriteRenderer pointerSprite = bodySprite.transform.Find("Pointer").GetComponent<SpriteRenderer>();
        Image healthImage = pInput.transform.Find("PlayerUI").Find("HealthBar").Find("Health").GetComponent<Image>();

        pointerSprite.color = playerSettings[index].color;
        healthImage.color = playerSettings[index].color;

        Animator bodyAnimator = pInput.transform.Find("PlayerBody").GetComponent<Animator>();
        bodyAnimator.runtimeAnimatorController = playerSettings[index].animController;
    }

    public void KillPlayer(int index)
    {
        Debug.Log("Killing Player " + index);
        Destroy(playerObjects[index]);
        playerObjects.RemoveAt(index);

        if (isGameRunning && playerObjects.Count <= 1)
        {
            isGameRunning = false;
            int winnerIndex = playerObjects[0].GetComponent<PlayerData>().playerIndex;
            Debug.Log("Game Set! Player " + winnerIndex + " has won.");

            playerObjects[0].GetComponent<PlayerInput>().enabled = false;

            GameController.Instance.EndGame(winnerIndex, playerSettings[winnerIndex].color);
        }
    }

    public void OnPlayerLeave(PlayerInput pInput)
    {
        Debug.Log("Player " + pInput.playerIndex + " is gone");
    }
}
