using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    // Step encounter settings
    [SerializeField] private float stepDistance = 0.8f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    // Step tracking
    private Vector2 lastPosition;
    private float distanceMoved;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        lastPosition = rb.position;

        if (PlayerPrefs.HasKey("SpawnX"))
    {
        float x = PlayerPrefs.GetFloat("SpawnX");
        float y = PlayerPrefs.GetFloat("SpawnY");

        transform.position = new Vector2(x, y);
    }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        TrackSteps();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();

        // Strict 4-direction movement
        if (rawInput.x != 0)
        {
            moveInput = new Vector2(Mathf.Sign(rawInput.x), 0f);
        }
        else if (rawInput.y != 0)
        {
            moveInput = new Vector2(0f, Mathf.Sign(rawInput.y));
        }
        else
        {
            moveInput = Vector2.zero;
        }

        // Animator handling
        bool isMoving = moveInput != Vector2.zero;

        animator.SetBool("isWalking", isMoving);

        if (!isMoving)
        {
            animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
            animator.SetFloat("LastInputY", animator.GetFloat("InputY"));
        }

        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    void TrackSteps()
    {
        // Only track while moving
        if (moveInput == Vector2.zero)
            return;

        distanceMoved += Vector2.Distance(rb.position, lastPosition);

        lastPosition = rb.position;

        if (distanceMoved >= stepDistance)
        {
            distanceMoved = 0f;

            Debug.Log("STEP TAKEN");

            EncounterZone.PlayerStepped();
        }
    }
}