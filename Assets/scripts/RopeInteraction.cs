using UnityEngine;
using System.Collections;

public class RopeInteraction : MonoBehaviour
{
    public Color highlightColor;
    private Color originalColor;
    private Renderer ropeRenderer;
    public Transform targetPosition; 
    public float moveSpeed = 5f; 

    private void Start()
    {
        ropeRenderer = GetComponent<Renderer>();
        if (ropeRenderer == null)
        {
            Debug.LogError("Renderer component not found on the rope object.");
        }
        originalColor = ropeRenderer.material.color;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                HighlightRope();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MovePlayerToPosition();
                }
            }
            else
            {
                UnhighlightRope();
            }
        }
        else
        {
            UnhighlightRope();
        }
    }

    private void HighlightRope()
    {
        ropeRenderer.material.color = highlightColor;
    }

    private void UnhighlightRope()
    {
        ropeRenderer.material.color = originalColor;
    }

    private void MovePlayerToPosition()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            StartCoroutine(MovePlayerCoroutine(playerController));
        }
    }

    private IEnumerator MovePlayerCoroutine(PlayerController playerController)
    {
        Vector3 startPosition = playerController.transform.position;
        Vector3 targetPosition = this.targetPosition.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;
        float journeyLength = distance / moveSpeed;

        while (playerController.transform.position != targetPosition)
        {
            float coveredDistance = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = coveredDistance / journeyLength;
            playerController.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }

            yield return null;
        }
        playerController.DetachFromRope(); 
    }
}
