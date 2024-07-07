using UnityEngine;

public class KeyInteraction : MonoBehaviour
{
    public Color highlightColor;
    private Color originalColor;
    private Renderer keyRenderer;
    public float maxPickupDistance = 7f;

    private void Start()
    {
        keyRenderer = GetComponent<Renderer>();
        if (keyRenderer == null)
        {
            Debug.LogError("Renderer component not found on the key object.");
        }
        originalColor = keyRenderer.material.color;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                HighlightKey();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
                    if (distanceToPlayer <= maxPickupDistance)
                    {
                        AddKeyToInventory();
                    }
                    else
                    {
                        Debug.Log("The key is too far away to pick up.");
                    }
                }
            }
            else
            {
                UnhighlightKey();
            }
        }
        else
        {
            UnhighlightKey();
        }
    }

    private void HighlightKey()
    {
        keyRenderer.material.color = highlightColor;
    }

    private void UnhighlightKey()
    {
        keyRenderer.material.color = originalColor;
    }

    private void AddKeyToInventory()
    {
        InventoryManager.AddToInventory(gameObject);
        gameObject.SetActive(false);
    }
}
