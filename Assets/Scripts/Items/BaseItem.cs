using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BaseItem
{
    public int id; // ID предмета
    public VisualItemInInventory basicParameters; // Базовые переметры
    public VisualItemInWorld worldParameters; // параметры предмета в игромов мире
    public ToolActionsItems toolActions; // какие действие инструменра можно производить этим предметом

}
