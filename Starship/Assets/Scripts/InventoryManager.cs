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

    private void Update()
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

    private void AddItem()
    {
        foreach (InventorySlot slot in storageSlots)
        {
 
            if (slot.isEmpty == false)
            {
                slot.SetIconStart();
            }
        }
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.isEmpty == false)
            {
                slot.SetIconStart();
                slot.ApplySpeedBoostFromItem();
            }
        }
    }
}

