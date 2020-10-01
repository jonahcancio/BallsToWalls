using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerColors : MonoBehaviour
{
    [SerializeField]
    public List<Color> playerColors;

    public void ColorizePlayer(PlayerInput pInput)
    {
        int index = pInput.playerIndex;
        Transform pTransform = pInput.transform;
        SpriteRenderer bodySprite = pTransform.Find("PlayerBody").GetComponent<SpriteRenderer>();
        //SpriteRenderer pointerSprite = pTransform.Find("PlayerBody").Find("Pointer").GetComponent<SpriteRenderer>();
        SpriteRenderer pointerSprite = bodySprite.transform.Find("Pointer").GetComponent<SpriteRenderer>();
        Image healthImage = pTransform.Find("PlayerUI").Find("HealthBar").Find("Health").GetComponent<Image>();

        bodySprite.color = playerColors[index];
        pointerSprite.color = playerColors[index];
        healthImage.color = playerColors[index];
    }
}
