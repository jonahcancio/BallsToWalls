using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public List<BallState> stateList;


    [System.Serializable]
    public class BallState
    {
        public Color color;
        public Sprite sprite;
    }

    public float baseDamage = 10f;

    [SerializeField]
    private string currentState;

    public void OnSpawn()
    {
        ChangeState("death");
    }

    public void ChangeState(string state)
    {
        currentState = state;
        baseDamage = state == "death" ? 15 : -5;
        int stateIndex = state == "death" ? 1 : 0;
        GetComponent<SpriteRenderer>().color = stateList[stateIndex].color;
        GetComponent<SpriteRenderer>().sprite = stateList[stateIndex].sprite;

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
