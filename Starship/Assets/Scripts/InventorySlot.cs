using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public bool isEmpty = true;
    public GameObject iconGO;

    public SpaceshipController spaceship;

    private void Start()
    {
        iconGO = transform.GetChild(0).GetChild(0).gameObject;
        spaceship = FindObjectOfType<SpaceshipController>();
    }

    public void SetIcon(Sprite icon)
    {
        iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        iconGO.GetComponent<Image>().sprite = icon;
    }
    
    public void SetIconStart()
    {
        if (item != null)
        {
            iconGO.GetComponent<Image>().sprite = item.icon;
            iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            iconGO.GetComponent<Image>().enabled = true;
        }
        else
        {
            iconGO.GetComponent<Image>().sprite = null;
            iconGO.GetComponent<Image>().enabled = false;
        }
    }

    public void ApplySpeedBoostFromItem()
    {
        if (!isEmpty && item != null && transform.parent != null && transform.parent.name == "Inventary")
        {
            float speedBoost = 0f;

            if (item.itemType == ItemType.Module)
            {
                ModuleItem moduleItem = (ModuleItem)item;
                speedBoost = moduleItem.GetSpeedUpgrade();
            }
            if (spaceship != null)
            {
                spaceship.AddSpeed(speedBoost);
            }
        }
    }
    public void RemoveSpeedBoostFromItem()
    {
        if (!isEmpty && item != null && transform.parent != null && transform.parent.name == "Storage")
        {
            float speedBoost = 0f;

            if (item.itemType == ItemType.Module)
            {
                ModuleItem moduleItem = (ModuleItem)item;
                speedBoost = moduleItem.GetSpeedUpgrade();
            }

            if (spaceship != null)
            {
                spaceship.RemoveSpeed(speedBoost);
            }
        }
    }
}