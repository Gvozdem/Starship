using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryPanel;
    public Transform storagePanel;
    public List<InventorySlot> storageSlots = new List<InventorySlot>();
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public GameObject UIPanel;

    private bool isOpened = false;

    public List<ItemScriptableObject> startingItems;


    private void Start()
    {
        UIPanel.SetActive(isOpened);

        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                inventorySlots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }

        for (int i = 0; i < storagePanel.childCount; i++)
        {
            if (storagePanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                storageSlots.Add(storagePanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        AddItem();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventoryPanel();
        }
    }

    public void ShowInventoryPanel()
    {
        isOpened = !isOpened;
        UIPanel.SetActive(isOpened);
    }

    private void AddItem(ItemScriptableObject _item = null)
    {
        foreach (InventorySlot slot in storageSlots)
        {
            if (slot.isEmpty && _item != null)
            {
                slot.item = _item;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                break;
            }
            else if (slot.isEmpty == false && _item == null)
            {
                Debug.Log($"Закидывает стартовые слоты");
                slot.SetIconStart();
            }
        }
    }
}

