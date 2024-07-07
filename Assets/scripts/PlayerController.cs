using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float stoneAttachSpeed = 50.0f;
    private CharacterController controller;
    private bool isAttachedToStone = false;
    private Vector3 attachedStonePosition;
    private bool isAttachedToRope = false;
    private Vector3 attachedRopePosition;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!isAttachedToStone && !isAttachedToRope)
        {
            MovePlayer();
        }
        else
        {
            HandleAttachment();
        }
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection.y -= 9.81f * Time.deltaTime;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleAttachment()
    {
        Vector3 moveDirection = Vector3.zero;
        float attachSpeed = isAttachedToStone ? stoneAttachSpeed : moveSpeed;
        if (isAttachedToStone)
        {
            moveDirection = (attachedStonePosition - transform.position).normalized;
        }
        else if (isAttachedToRope)
        {
            moveDirection = (attachedRopePosition - transform.position).normalized;
        }

        controller.Move(moveDirection * attachSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isAttachedToStone)
            {
                DetachFromStone();
            }
            else if (isAttachedToRope)
            {
                DetachFromRope();
            }
        }
    }

    public void AttachToStone(Vector3 stonePosition)
    {
        float distanceToStone = Vector3.Distance(transform.position, stonePosition);
        if (distanceToStone <= stoneAttachSpeed)
        {
            isAttachedToStone = true;
            attachedStonePosition = stonePosition;
        }
        else
        {
            Debug.Log("The stone is too far away to attach.");
        }
    }

    public void DetachFromStone()
    {
        isAttachedToStone = false;
    }

    public void AttachToRope(Vector3 ropePosition)
    {
        isAttachedToRope = true;
        attachedRopePosition = ropePosition;
    }

    public void DetachFromRope()
    {
        isAttachedToRope = false;
    }
}
