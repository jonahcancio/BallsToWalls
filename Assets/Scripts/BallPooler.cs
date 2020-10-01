using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPooler : MonoBehaviour
{

    public GameObject ballPrefab;
    private Queue<GameObject> inactiveBallQueue;
    private Queue<GameObject> activeBallQueue;
    public int queueMaxSize = 40;

    #region Singleton
    public static BallPooler Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    void Start()
    {
        inactiveBallQueue = new Queue<GameObject>();
        activeBallQueue = new Queue<GameObject>();
        for (int i = 0; i < queueMaxSize; i++)
        {
            GameObject ballObj = Instantiate(ballPrefab, transform);
            ballObj.SetActive(false);
            inactiveBallQueue.Enqueue(ballObj);
        }
    }


    public void SpawnFromPool(Vector3 position, Vector2 velocity)
    {
        GameObject ballToSpawn;
        if (inactiveBallQueue.Count > 0)
        {
            ballToSpawn = inactiveBallQueue.Dequeue();
        } else if (activeBallQueue.Count > 0)
        {
            ballToSpawn = activeBallQueue.Dequeue();
        } else
        {
            return;
        }
        
        ballToSpawn.SetActive(true);

        ballToSpawn.GetComponent<Ball>().OnSpawn();
        ballToSpawn.transform.position = position;
        ballToSpawn.GetComponent<Rigidbody2D>().velocity = velocity;

        activeBallQueue.Enqueue(ballToSpawn);
    }

    public void ReturnToPool(GameObject ballToReturn)
    {
        ballToReturn.SetActive(false);
        inactiveBallQueue.Enqueue(ballToReturn);
    }
}
