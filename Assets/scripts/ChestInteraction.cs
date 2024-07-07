using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class ChestInteraction : MonoBehaviour
{
    public Color highlightColor;
    private Color originalColor;
    private Renderer chestRenderer;
    private bool isHighlighted = false;
    public TMP_Text notificationText;
    public Material newSkybox; // Новый скайбокс
    public PlayableDirector timelineDirector; // Ссылка на ваш Timeline

    private bool sceneFinished = false;

    private void Start()
    {
        timelineDirector.stopped += OnTimelineFinished;
        chestRenderer = GetComponent<Renderer>();
        if (chestRenderer == null)
        {
            Debug.LogError("Renderer component not found on the chest object.");
        }
        originalColor = chestRenderer.material.color;

        if (notificationText != null)
        {
            notificationText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!sceneFinished)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (!isHighlighted) HighlightChest();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        TryOpenChest();
                    }
                }
                else if (isHighlighted)
                {
                    UnhighlightChest();
                }
            }
            else if (isHighlighted)
            {
                UnhighlightChest();
            }
        }
    }

    private void HighlightChest()
    {
        chestRenderer.material.color = highlightColor;
        isHighlighted = true;
    }

    private void UnhighlightChest()
    {
        chestRenderer.material.color = originalColor;
        isHighlighted = false;
    }

    private void TryOpenChest()
    {
        List<GameObject> inventory = InventoryManager.GetInventory();
        GameObject crystal = null;

        foreach (GameObject item in inventory)
        {
            Debug.Log("Inventory item: " + item.name);
            if (item.CompareTag("Crystal"))
            {
                crystal = item;
                break;
            }
        }

        if (crystal != null)
        {
            Debug.Log("Crystal found in inventory");
            timelineDirector.Play();

            if (newSkybox != null)
            {
                RenderSettings.skybox = newSkybox;
                DynamicGI.UpdateEnvironment(); 
            }
        }
        else
        {
            Debug.Log("No crystal in inventory to place in the chest.");
        }
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        sceneFinished = true;
        if (notificationText != null)
        {
            notificationText.gameObject.SetActive(true);
            Time.timeScale = 1f; 
        }
    }
}
