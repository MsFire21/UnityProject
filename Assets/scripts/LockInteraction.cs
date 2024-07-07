using System.Collections.Generic;
using UnityEngine;

public class LockInteraction : MonoBehaviour
{
    public GameObject lockObject;
    public GameObject chainObject;
    public Color highlightColor;
    private Color originalColor;
    private Renderer lockRenderer;
    private bool isHighlighted = false;
    private static int unlockedLocks = 0;

    private void Start()
    {
        lockRenderer = GetComponent<Renderer>();
        if (lockRenderer == null)
        {
            Debug.LogError("Renderer component not found on the lock object.");
        }
        originalColor = lockRenderer.material.color;
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (!isHighlighted) HighlightLock();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    List<GameObject> inventory = InventoryManager.GetInventory();
                    foreach (GameObject item in inventory)
                    {
                        if (item.CompareTag("Key"))
                        {
                            Unlock();
                            InventoryManager.RemoveFromInventory(item);
                            break;
                        }
                    }
                }
            }
            else if (isHighlighted)
            {
                UnhighlightLock();
            }
        }
        else if (isHighlighted)
        {
            UnhighlightLock();
        }
    }



    private void HighlightLock()
    {
        lockRenderer.material.color = highlightColor;
        isHighlighted = true;
    }

    private void UnhighlightLock()
    {
        lockRenderer.material.color = originalColor;
        isHighlighted = false;
    }

    private void Unlock()
    {
        unlockedLocks++;
        Destroy(lockObject);

        if (unlockedLocks == 5) 
        {
            Destroy(chainObject);
        }
    }
}
