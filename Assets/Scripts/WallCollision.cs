using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{

    public float maxVelocity = 40f;
    public float maxScale = 2f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Projectile"))
        {
            col.gameObject.GetComponent<Ball>().FlipState();
            //Biggen(col.transform);
        }

    }

    void SpeedUp(Rigidbody2D rb)
    {
        Vector2 vel = rb.velocity;
        rb.velocity = Vector2.ClampMagnitude(1.5f * vel, maxVelocity); ;
    }

    void Biggen(Transform t)
    {
        Vector3 scale = t.localScale;
        t.localScale = Vector3.ClampMagnitude(1.2f * scale, maxScale);
    }
}
