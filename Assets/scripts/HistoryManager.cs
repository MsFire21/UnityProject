using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HistoryManager : MonoBehaviour
{
    public Color highlightColor;
    private Color originalColor;
    private Renderer historyRenderer;
    public float maxPickupDistance = 7f;
    public GameObject scrollUIPanel; // Панель UI для свитка
    public TextMeshProUGUI scrollText; // UI текст для отображения истории
    public string historyText; // Текст истории для этого конкретного свитка
    private bool isScrollUIActive = false;

    void Start()
    {
        historyRenderer = GetComponent<Renderer>();
        if (historyRenderer == null)
        {
            Debug.LogWarning("No Renderer component found on the gameObject.");
            return;
        }
        originalColor = historyRenderer.material.color;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                HighlightHistory();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
                    if (distanceToPlayer <= maxPickupDistance)
                    {
                        AddHistoryToInventory();
                    }
                    else
                    {
                        Debug.Log("The history is too far away to pick up.");
                    }
                }
            }
            else
            {
                UnhighlightHistory();
            }
        }
        else
        {
            UnhighlightHistory();
        }
    }

    private void HighlightHistory()
    {
        historyRenderer.material.color = highlightColor;
    }

    private void UnhighlightHistory()
    {
        historyRenderer.material.color = originalColor;
    }

    private void AddHistoryToInventory()
    {
        InventoryManager.AddToInventory(gameObject);
        gameObject.SetActive(false);
        ShowScrollUI(); 
    }

    public void ShowScrollUI()
    {
        scrollUIPanel.SetActive(true);
        scrollText.text = historyText; 
        isScrollUIActive = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideScrollUI()
    {
        scrollUIPanel.SetActive(false);
        isScrollUIActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
