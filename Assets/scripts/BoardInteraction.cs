using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInteraction : MonoBehaviour
{
    public Color highlightColor;
    private Color originalColor;
    private Renderer boardRenderer;
    private bool isTaken = false;
    private bool isHighlighted = false;

    private void Start()
    {
        boardRenderer = GetComponent<Renderer>();
        if (boardRenderer == null)
        {
            Debug.LogError("Renderer component not found on the board object.");
        }
        originalColor = boardRenderer.material.color;
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject && !isTaken)
            {
                if (!isHighlighted) HighlightBoard();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!isTaken)
                    {
                        InventoryManager.AddToInventory(gameObject);
                        isTaken = true;
                        gameObject.SetActive(false);
                    }
                }
            }
            else if (isHighlighted)
            {
                UnhighlightBoard();
            }
        }
        else if (isHighlighted)
        {
            UnhighlightBoard();
        }
    }

    private void HighlightBoard()
    {
        boardRenderer.material.color = highlightColor;
        isHighlighted = true;
    }

    private void UnhighlightBoard()
    {
        boardRenderer.material.color = originalColor;
        isHighlighted = false;
    }
}
