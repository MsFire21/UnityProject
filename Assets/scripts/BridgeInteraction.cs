using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeInteraction : MonoBehaviour
{
    public GameObject hiddenBoard;
    public Color highlightColor;
    private Color originalColor;
    private Renderer boardRenderer;
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
            if (hit.collider.gameObject == gameObject)
            {
                if (!isHighlighted) HighlightBoard();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameObject boardToRemove = null;
                    List<GameObject> inventory = InventoryManager.GetInventory();
                    foreach (GameObject item in inventory)
                    {
                        if (item.CompareTag("Board"))
                        {
                            boardToRemove = item;
                            break;
                        }
                    }

                    if (boardToRemove != null)
                    {
                        ActivateHiddenBoard();
                        InventoryManager.RemoveFromInventory(boardToRemove);
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

    private void ActivateHiddenBoard()
    {
        hiddenBoard.SetActive(true);
        gameObject.SetActive(false);
    }
}
