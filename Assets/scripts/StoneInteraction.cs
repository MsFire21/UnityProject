using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneInteraction : MonoBehaviour
{
    public Color highlightColor;
    public float maxAttachDistance = 10f;
    private Color originalColor;
    private Renderer stoneRenderer;
    private bool isAttached = false;
    private Vector3 stonePosition;
    public float moveSpeed = 50f;

    private void Start()
    {
        stoneRenderer = GetComponent<Renderer>();
        if (stoneRenderer == null)
        {
            Debug.LogError("Renderer component not found on the stone object.");
        }
        originalColor = stoneRenderer.material.color;
        stonePosition = transform.position;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                HighlightStone();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
                    if (distanceToPlayer <= maxAttachDistance)
                    {
                        if (!isAttached)
                        {
                            StartCoroutine(MovePlayerToStone());
                        }
                        else
                        {
                            DetachPlayerFromStone();
                        }
                    }
                    else
                    {
                        Debug.Log("The stone is too far away to attach.");
                    }
                }
            }
            else
            {
                UnhighlightStone();
            }
        }
        else
        {
            UnhighlightStone();
        }
    }

    private void HighlightStone()
    {
        stoneRenderer.material.color = highlightColor;
    }

    private void UnhighlightStone()
    {
        stoneRenderer.material.color = originalColor;
    }

    private IEnumerator MovePlayerToStone()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            Vector3 startPosition = playerController.transform.position;
            float startTime = Time.time;
            float journeyLength = Vector3.Distance(startPosition, stonePosition);
            float fracJourney = 0;

            while (fracJourney < 1)
            {
                fracJourney = (Time.time - startTime) * moveSpeed / journeyLength;
                playerController.transform.position = Vector3.Lerp(startPosition, stonePosition, fracJourney);
                yield return null;
            }

            AttachPlayerToStone();
        }
    }

    private void AttachPlayerToStone()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.AttachToStone(stonePosition);
            isAttached = true;
        }
    }

    private void DetachPlayerFromStone()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.DetachFromStone();
            isAttached = false;
        }
    }
}
