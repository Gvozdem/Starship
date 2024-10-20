using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Module", menuName ="Inventory/Items/New Module")]
public class ModuleItem : ItemScriptableObject
{
    public float speedUpgrade;

    private void Start()
    {
        itemType = ItemType.Module;
    }
}
