using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BaseItem
{
    public int id; // ID ��������
    public VisualItemInInventory basicParameters; // ������� ���������
    public VisualItemInWorld worldParameters; // ��������� �������� � ������� ����
    public ToolActionsItems toolActions; // ����� �������� ����������� ����� ����������� ���� ���������

}
