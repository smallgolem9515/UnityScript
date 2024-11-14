/*
 * ����� ���� �ڷ����� �⺻������ Serialize �� �ȵȴ�.
 * C# ������ System.Serializable Attribute �� �ο��ؼ� Serialize �� �����ϵ��� ����ؾ��Ѵ�.
 */
using System;
using System.Collections.Generic;

namespace Practices.MVC_Example.DataBace
{
    [Serializable]
    public struct InventorySlotData
    {
        public int itemId;
        public int itemNum;
    }

    public class InventoryData
    {
        public InventoryData(int capacity)
        {
            slotDataList = new List<InventorySlotData>(capacity);
        }
        public List<InventorySlotData> slotDataList;
    }
}

