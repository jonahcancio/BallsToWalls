using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool joinCompleted = false;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform playerPointer;
    public Animator playerBodyAnimator;

    private Vector2 moveVector;
    private Vector2 aimVector;


    public float playerSpeed = 15f;
    public float projectileSpeed = 20f;
    public float lobCooldown = 1.0f;

    private Rigidbody2D rb;
    private Transform playerBody;

    private float timeLastLobbed;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        playerBody = transform.Find("PlayerBody");
        aimVector = playerBody.up;
        playerPointer = playerBody.Find("Pointer");
        playerBodyAnimator = playerBody.GetComponent<Animator>();

        timeLastLobbed = -lobCooldown;
        joinCompleted = true;
    }

    void Update()
    {
        rb.velocity = moveVector * playerSpeed;
        playerBody.up = aimVector;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!joinCompleted)
        {
            return;
        }

        moveVector = context.ReadValue<Vector2>();
            
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (!joinCompleted)
        {
            return;
        }
        if (context.phase == InputActionPhase.Performed)
        {
            string deviceUsed = context.control.device.displayName;
            Vector2 mouseVector, stickVector;
            if (deviceUsed == "Mouse")
            {
                mouseVector = mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
                aimVector = Vector3.Normalize(new Vector3(mouseVector.x, mouseVector.y) - transform.position);
            }
            else
            {
                stickVector = context.ReadValue<Vector2>();
                if (!stickVector.Equals(Vector2.zero))
                {
                    aimVector = Vector3.Normalize(stickVector);
                }
            }
        }
        
    }

    public void OnLob(InputAction.CallbackContext context)
    {
        if (!joinCompleted)
        {
            return;
        }
        if (context.phase == InputActionPhase.Performed)
        {
            if (Time.time - timeLastLobbed > lobCooldown)
            {
                //BallPooler.Instance.SpawnFromPool(playerPointer.position, aimVector * projectileSpeed);
                StartCoroutine(LobBallAfterTime(0.125f));
                timeLastLobbed = Time.time;
                playerBodyAnimator.SetTrigger("IsLobbing");
                AudioController.Instance.PlaySound("lob");
            }
        }
    }

    private IEnumerator LobBallAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        BallPooler.Instance.SpawnFromPool(playerPointer.position, aimVector * projectileSpeed);
    }
}
