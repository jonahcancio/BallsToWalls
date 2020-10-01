using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Dictionary<string, Color> stateColors;
    public Color lifeColor;
    public Color deathColor;
    public float baseDamage = 10f;

    [SerializeField]
    private string currentState;

    public void OnSpawn()
    {
        stateColors = new Dictionary<string, Color>();
        stateColors["life"] = lifeColor;
        stateColors["death"] = deathColor;

        ChangeState("death");
    }

    public void ChangeState(string state)
    {
        currentState = state;
        GetComponent<SpriteRenderer>().color = stateColors[state];
        baseDamage = state == "death" ? 15 : -5;
    }

    public void FlipState()
    {
        if (currentState == "life")
        {
            ChangeState("death");
        }
        else
        {
            ChangeState("life");
        }
    }

}
