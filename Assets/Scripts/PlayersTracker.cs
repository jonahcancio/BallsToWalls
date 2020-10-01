using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayersTracker : MonoBehaviour
{
    
    public List<Color> playerColors;
    [SerializeField]
    private List<GameObject> playerObjects;
    [SerializeField]
    private bool isGameRunning;
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

        bodySprite.color = playerColors[index];
        pointerSprite.color = playerColors[index];
        healthImage.color = playerColors[index];
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
        }
    }
}
