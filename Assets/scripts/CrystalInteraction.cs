using UnityEngine;

public class CrystalInteraction : MonoBehaviour
{
    public Color highlightColor;
    private Color originalColor;
    private Renderer crystalRenderer;
    private bool isHighlighted = false;
    public GameObject crystal;

    private void Start()
    {
        crystalRenderer = GetComponent<Renderer>();
        if (crystalRenderer == null)
        {
            Debug.LogError("Renderer component not found on the crystal object.");
        }
        originalColor = crystalRenderer.material.color;
        crystal.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (!isHighlighted) HighlightCrystal();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AddCrystalToInventory();
                }
            }
            else if (isHighlighted)
            {
                UnhighlightCrystal();
            }
        }
        else if (isHighlighted)
        {
            UnhighlightCrystal();
        }
    }

    private void HighlightCrystal()
    {
        crystalRenderer.material.color = highlightColor;
        isHighlighted = true;
    }

    private void UnhighlightCrystal()
    {
        crystalRenderer.material.color = originalColor;
        isHighlighted = false;
    }

    private void AddCrystalToInventory()
    {
        InventoryManager.AddToInventory(gameObject);
        Debug.Log("Crystal added to inventory");
        gameObject.SetActive(false);
        crystal.SetActive(true);
    }

}
