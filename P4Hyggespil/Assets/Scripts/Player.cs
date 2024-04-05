using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private TileManager tileManager;

    [SerializeField] private Camera cam;

    private Vector3Int previousMousePos = new Vector3Int();

    public Vector3 mousePos;
    public Vector3Int currentTile;
    bool mouseInRange;

    private void Start()
    {
        tileManager = GameManager.instance.tileManager;
    }

    private void Update()
    {
        Vector3Int mousePos = GetMousePosition();

        isHoverActive();

        //if (!mousePos.Equals(previousMousePos) || !mouseInRange)
        //{
            tileManager.hoverMap.SetTile(previousMousePos, null);

            if (mouseInRange)
            {
                tileManager.hoverMap.SetTile(mousePos, tileManager.interactableHoverTile);
            }
            else if (mouseInRange == false)
            {
                tileManager.hoverMap.SetTile(mousePos, tileManager.nonInteractableHoverTile);
            }
            
            previousMousePos = mousePos;
       // }
        
        if (Input.GetKeyDown(KeyCode.Space) && mouseInRange)
        {
            if (tileManager != null)
            {

                string tileName = tileManager.GetTileName(mousePos);

                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    if (tileName == "Interactable" && inventoryManager.toolbar.selectedSlot.itemName == "Hoe")
                    {
                        tileManager.interactableMap.SetTile(mousePos,tileManager.plowedTile);
                    }
                }
            }
        }
    }

    void ChangeHoverMarker()
    {

    }

    Vector3Int GetMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        return tileManager.hoverMap.WorldToCell(mousePos);
    }

    private void isHoverActive()
    {
        Vector3 mousePlayerDistance = mousePos - transform.position;

        if(mousePlayerDistance.magnitude - 10 < 0.15)
        {
            mouseInRange = true;
        }
        else
        {
            mouseInRange = false;
        }

        Debug.Log(mousePlayerDistance.magnitude - 10);
        
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);

        droppedItem.rb2D.AddForce(spawnOffset * 2, ForceMode2D.Impulse);
    }

    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }
}