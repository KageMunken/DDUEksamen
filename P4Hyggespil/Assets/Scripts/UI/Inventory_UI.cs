using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public Player player;

    public List<Slot_UI> slots = new List<Slot_UI>();

    private Slot_UI draggedSlot;
    private Image draggedIcon;
    private Canvas canvas;
    private bool dragSingle;

    private void Awake()
    {
      canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        SetupSlots();
        Refresh();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {

            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                Refresh();
            }
            else
            {
                inventoryPanel.SetActive(false);
            }

        }
    }

    void Refresh()
    {
        if (slots.Count == player.toolbar.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.toolbar.slots[i].itemName != "")
                {
                    slots[i].SetItem(player.toolbar.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
        else if (slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove()
    {
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(
            player.inventory.slots[draggedSlot.slotID].itemName);

        if(itemToDrop != null)
        { 
            if (dragSingle)
            {
                player.DropItem(itemToDrop);
                player.inventory.Remove(draggedSlot.slotID);
            }
            else
            {
                player.DropItem(itemToDrop, player.inventory.slots[draggedSlot.slotID].count);
                player.inventory.Remove(draggedSlot.slotID, player.inventory.slots[draggedSlot.slotID].count);
            }

            Refresh();
        }  

        draggedSlot = null;
    }

    public void SlotBeginDrag(Slot_UI slot)
    {
        draggedSlot = slot;
        draggedIcon = Instantiate(draggedSlot.itemIcon);
        draggedIcon.raycastTarget = false;
        draggedIcon.rectTransform.sizeDelta = new Vector2(60, 60);
        draggedIcon.transform.SetParent(canvas.transform);

        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Start Drag: " + draggedSlot.name);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);
    }

    public void SlotEndDrag()
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null;
    }

    public void SlotDrop(Slot_UI slot)
    {
        player.inventory.MoveSlot(draggedSlot.slotID, slot.slotID);
        Refresh();
    }

    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);

            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    void SetupSlots()
    {
        int counter = 0;

        foreach(Slot_UI slot in slots)
        {
            slot.slotID = counter;
            counter++;
        }
    }
}

