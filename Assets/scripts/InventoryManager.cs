using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static List<GameObject> inventory = new List<GameObject>();

    public static void AddToInventory(GameObject item)
    {
        inventory.Add(item);
        Debug.Log($"{item.name} added to inventory");
    }

    public static List<GameObject> GetInventory()
    {
        return new List<GameObject>(inventory);
    }

    public static bool HasItemWithTag(string tag)
    {
        foreach (GameObject item in inventory)
        {
            if (item.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    public static GameObject GetItemWithTag(string tag)
    {
        foreach (GameObject item in inventory)
        {
            if (item.CompareTag(tag))
            {
                return item;
            }
        }
        return null;
    }

    public static void RemoveFromInventory(GameObject item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            Debug.Log($"{item.name} removed from inventory");
        }
        else
        {
            Debug.LogError($"Attempt to remove {item.name}, which is not in inventory");
        }
    }

    public static int GetItemCount(string tag)
    {
        int count = 0;
        foreach (GameObject item in inventory)
        {
            if (item.CompareTag(tag))
            {
                count++;
            }
        }
        return count;
    }

    public static void ClearInventory()
    {
        inventory.Clear();
    }
}
