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
                    ItemSpec itemSpec = _itemSpecRepository.Get(ChangeData.itemId); // 현재 슬롯 데이터에 해당하는 아이템 사양서 가져오기
                    _inventorySlots[slotIndex].itemIcon = itemSpec.Icon;
                    _inventorySlots[slotIndex].itemNum = ChangeData.itemNum;
                }
                // 빈 슬롯
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
            
            //선택된 슬롯 있는지?
            if (_selectedSlot)
            {
                // UI가 클릭됨
                if (_raycastResults.Count > 0) 
                {
                    // 슬롯이 캐스팅 되었는지?
                    if (_raycastResults[0].gameObject.TryGetComponent(out InventorySlot slot))
                    {
                        // 이미 선택된 슬롯 선택시 선택 취소
                        if(slot == _selectedSlot)
                        {
                            _selectedSlot = null;
                            _previewSelectedSlot.enabled = false;
                        }
                        // 다른 슬롯 선택시 데이터 스왑
                        else
                        {
                            SwapCommand(_selectedSlot.index, slot.index);
                            _selectedSlot = null;
                            _previewSelectedSlot.enabled = false;
                            //RefreshAllInventorySlots()
                        }
                    }
                }
                else // field가 클릭됨
                {
                    //nothing to do
                }
            }
            else
            {
                // UI가 클릭됨
                if (_raycastResults.Count > 0) 
                {
                    // 슬롯이 캐스팅 되었는지?
                    if (_raycastResults[0].gameObject.TryGetComponent(out InventorySlot slot))
                    {
                        _selectedSlot = slot;
                        _previewSelectedSlot.sprite = _selectedSlot.itemIcon;
                        _previewSelectedSlot.enabled = true;
                    }
                }
                else // field가 클릭됨
                {
                    //nothing to do
                }

            }
        }

        /// <summary>
        /// 인벤토리 데이터를 전체 순회하면서 슬롯View 를 모두 갱신하는 함수
        /// </summary>
        /// <param name="inventoryData"></param>
        public void RefreshAllInventorySlots(InventoryData inventoryData)
        {
            for (int i = 0; i < inventoryData.slotDataList.Count; i++)
            {
                InventorySlotData slotData = inventoryData.slotDataList[i];

                // 아이템이 있는 슬롯
                if(slotData.itemId >= 0)
                {
                    ItemSpec itemSpec = _itemSpecRepository.Get(slotData.itemId); // 현재 슬롯 데이터에 해당하는 아이템 사양서 가져오기
                    _inventorySlots[i].itemIcon = itemSpec.Icon;
                    _inventorySlots[i].itemNum = slotData.itemNum;
                }
                // 빈 슬롯
                else
                {
                    _inventorySlots[i].itemIcon = null;
                    _inventorySlots[i].itemNum = 0;
                }
                
            }
        }


        /// <summary>
        /// 인벤토리 View 를 갱신하기위해서 저장되어있던 인벤토리 데이터를 불러옴
        /// </summary>
        private InventoryData LoadInventoryData()
        {
            if (File.Exists(_inventoryDataPath) == false)
            {
                CreateDefaultInventoryData();
            }

            string jsonData = File.ReadAllText(_inventoryDataPath); // 경로에서 텍스트 읽어옴
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(jsonData); // Deserialize (읽은 텍스트를 InventoryData로 변환)
            return inventoryData;
        }

        /// <summary>
        /// 인벤토리 데이터가 한번도 만들어진적이 없을때
        /// 최초로 한번 기본값으로 생성해주기위함
        /// </summary>
        private void CreateDefaultInventoryData()
        {
            InventoryData inventoryData = new InventoryData(_inventorySlotNumber); // 아이템 추가할 용량 2 설정
            inventoryData.slotDataList.Add(new InventorySlotData { itemId = 10001, itemNum = 52 });
            inventoryData.slotDataList.Add(new InventorySlotData { itemId = 10002, itemNum = 2 });
            for (int i = inventoryData.slotDataList.Count; i < _inventorySlotNumber; i++)
            {
                inventoryData.slotDataList.Add(new InventorySlotData { itemId = -1, itemNum = 0 });
            }
            string jsonData = JsonUtility.ToJson(inventoryData); // Serialize (InventoryData 를 json 포맷의 문자열로)
            File.WriteAllText(_inventoryDataPath, jsonData); // 바뀐 json 문자열 파일로 저장
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