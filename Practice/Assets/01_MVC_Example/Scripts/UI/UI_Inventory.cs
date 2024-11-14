using Practices.MVC_Example.DataBace;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Practices.MVC_Example.UI
{
    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField] InventorySlot _inventorySlotPrefeb;
        [SerializeField] Transform _content;
        [SerializeField] ItemSpecRepository _itemSpecRepository;
        List<InventorySlot> _inventorySlots;
        string _inventoryDataPath;
        int _inventorySlotNumber = 40;
        [SerializeField] InputActionReference _leftClick;
        [SerializeField] InputActionReference _rightClick;
        GraphicRaycaster _graphicRaycaster;
        List<RaycastResult> _raycastResults;
        PointerEventData _pointerEventData;
        InventorySlot _selectedSlot;
        [SerializeField] Image _previewSelectedSlot;
        event Action<int, InventorySlotData> onSlotDataChanged;

        private void Awake()
        {
            _pointerEventData = new PointerEventData(EventSystem.current);
            _raycastResults = new List<RaycastResult>(5);
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            _inventoryDataPath = Application.persistentDataPath + "/InventoryData.json";
            _inventorySlots = new List<InventorySlot>(_inventorySlotNumber);

            for (int i = 0; i < _inventorySlotNumber; i++)
            {
                InventorySlot slot = Instantiate(_inventorySlotPrefeb, _content);
                slot.index = i;
                slot.itemIcon = null;
                slot.itemNum = 0;
                _inventorySlots.Add(slot);
            }
            onSlotDataChanged += (slotIndex, ChangeData) =>
            {
                if (ChangeData.itemId >= 0)
                {
                    ItemSpec itemSpec = _itemSpecRepository.Get(ChangeData.itemId); // ���� ���� �����Ϳ� �ش��ϴ� ������ ��缭 ��������
                    _inventorySlots[slotIndex].itemIcon = itemSpec.Icon;
                    _inventorySlots[slotIndex].itemNum = ChangeData.itemNum;
                }
                // �� ����
                else
                {
                    _inventorySlots[slotIndex].itemIcon = null;
                    _inventorySlots[slotIndex].itemNum = 0;
                }
            };

            _leftClick.action.performed += OnLeftClick;

        }
        private void Start()
        {
            InventoryData inventoryData = LoadInventoryData();
            RefreshAllInventorySlots(inventoryData);
        }

        private void Update()
        {
            if(_selectedSlot)
            {
                _previewSelectedSlot.transform.position = Mouse.current.position.ReadValue();
            }
        }

        private void OnLeftClick(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValueAsButton());
            if(context.ReadValueAsButton())
            {
                return;
            }
            _pointerEventData.position = Mouse.current.position.ReadValue();
            _raycastResults.Clear();
            _graphicRaycaster.Raycast(_pointerEventData, _raycastResults);
            
            //���õ� ���� �ִ���?
            if (_selectedSlot)
            {
                // UI�� Ŭ����
                if (_raycastResults.Count > 0) 
                {
                    // ������ ĳ���� �Ǿ�����?
                    if (_raycastResults[0].gameObject.TryGetComponent(out InventorySlot slot))
                    {
                        // �̹� ���õ� ���� ���ý� ���� ���
                        if(slot == _selectedSlot)
                        {
                            _selectedSlot = null;
                            _previewSelectedSlot.enabled = false;
                        }
                        // �ٸ� ���� ���ý� ������ ����
                        else
                        {
                            SwapCommand(_selectedSlot.index, slot.index);
                            _selectedSlot = null;
                            _previewSelectedSlot.enabled = false;
                            //RefreshAllInventorySlots()
                        }
                    }
                }
                else // field�� Ŭ����
                {
                    //nothing to do
                }
            }
            else
            {
                // UI�� Ŭ����
                if (_raycastResults.Count > 0) 
                {
                    // ������ ĳ���� �Ǿ�����?
                    if (_raycastResults[0].gameObject.TryGetComponent(out InventorySlot slot))
                    {
                        _selectedSlot = slot;
                        _previewSelectedSlot.sprite = _selectedSlot.itemIcon;
                        _previewSelectedSlot.enabled = true;
                    }
                }
                else // field�� Ŭ����
                {
                    //nothing to do
                }

            }
        }

        /// <summary>
        /// �κ��丮 �����͸� ��ü ��ȸ�ϸ鼭 ����View �� ��� �����ϴ� �Լ�
        /// </summary>
        /// <param name="inventoryData"></param>
        public void RefreshAllInventorySlots(InventoryData inventoryData)
        {
            for (int i = 0; i < inventoryData.slotDataList.Count; i++)
            {
                InventorySlotData slotData = inventoryData.slotDataList[i];

                // �������� �ִ� ����
                if(slotData.itemId >= 0)
                {
                    ItemSpec itemSpec = _itemSpecRepository.Get(slotData.itemId); // ���� ���� �����Ϳ� �ش��ϴ� ������ ��缭 ��������
                    _inventorySlots[i].itemIcon = itemSpec.Icon;
                    _inventorySlots[i].itemNum = slotData.itemNum;
                }
                // �� ����
                else
                {
                    _inventorySlots[i].itemIcon = null;
                    _inventorySlots[i].itemNum = 0;
                }
                
            }
        }


        /// <summary>
        /// �κ��丮 View �� �����ϱ����ؼ� ����Ǿ��ִ� �κ��丮 �����͸� �ҷ���
        /// </summary>
        private InventoryData LoadInventoryData()
        {
            if (File.Exists(_inventoryDataPath) == false)
            {
                CreateDefaultInventoryData();
            }

            string jsonData = File.ReadAllText(_inventoryDataPath); // ��ο��� �ؽ�Ʈ �о��
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(jsonData); // Deserialize (���� �ؽ�Ʈ�� InventoryData�� ��ȯ)
            return inventoryData;
        }

        /// <summary>
        /// �κ��丮 �����Ͱ� �ѹ��� ����������� ������
        /// ���ʷ� �ѹ� �⺻������ �������ֱ�����
        /// </summary>
        private void CreateDefaultInventoryData()
        {
            InventoryData inventoryData = new InventoryData(_inventorySlotNumber); // ������ �߰��� �뷮 2 ����
            inventoryData.slotDataList.Add(new InventorySlotData { itemId = 10001, itemNum = 52 });
            inventoryData.slotDataList.Add(new InventorySlotData { itemId = 10002, itemNum = 2 });
            for (int i = inventoryData.slotDataList.Count; i < _inventorySlotNumber; i++)
            {
                inventoryData.slotDataList.Add(new InventorySlotData { itemId = -1, itemNum = 0 });
            }
            string jsonData = JsonUtility.ToJson(inventoryData); // Serialize (InventoryData �� json ������ ���ڿ���)
            File.WriteAllText(_inventoryDataPath, jsonData); // �ٲ� json ���ڿ� ���Ϸ� ����
        }

        private void SwapCommand(int slot1Index, int slot2Index)
        {
            InventoryData inventoryData = LoadInventoryData();
            InventorySlotData slot1Data = inventoryData.slotDataList[slot1Index];
            inventoryData.slotDataList[slot1Index] = inventoryData.slotDataList[slot2Index];
            inventoryData.slotDataList[slot2Index] = slot1Data;
            SaveInventoryData(inventoryData);
            onSlotDataChanged.Invoke(slot1Index, inventoryData.slotDataList[slot1Index]);
            onSlotDataChanged.Invoke(slot2Index, inventoryData.slotDataList[slot2Index]);
        }

        private void SaveInventoryData(InventoryData inventoryData)
        {
            string jsonData = JsonUtility.ToJson(inventoryData);
            File.WriteAllText(_inventoryDataPath, jsonData);
        }
    }
}