using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private TileManager tileManager;
    private ItemManager itemManager;
    private DialogueManager dialogueManager;

    [SerializeField] private Camera cam;

    private Vector3Int previousMousePos = new Vector3Int();

    public Vector3 mousePos;
    public Vector3Int currentTile;
    bool mouseInRange;
    [SerializeField] GameObject dialogueBox;

    private BoxCollider2D boxCollider;

    public float boxcastOffset = 1f;
    public bool isDialogueBoxActive;
    public LayerMask layerMask;
    private Vector2 lastDirection;

    private void Start()
    {
        tileManager = GameManager.instance.tileManager;
        itemManager = GameManager.instance.itemManager;
        dialogueManager = GameManager.instance.dialogueManager;

        boxCollider = GetComponent<BoxCollider2D>();
        isDialogueBoxActive = false;
    }

    private void Update()
    {
        Vector3Int mousePos = GetMousePosition();

        Vector2 movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movementDirection != Vector2.zero)
        {
            lastDirection = movementDirection;
        }

        ChangeCursorTile();
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && mouseInRange)
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

                    else if (tileName == "Sten" && inventoryManager.toolbar.selectedSlot.itemName == "Pickaxe" ||
                        tileName == "Sten_1" && inventoryManager.toolbar.selectedSlot.itemName == "Pickaxe")
                    {
                        tileManager.objectMap.SetTile(mousePos, null);
                        tileManager.interactableMap.SetTile(mousePos, tileManager.hiddenInteractableTile);
                        Instantiate(itemManager.items[2], mousePos, Quaternion.identity);
                    }

                    else if (tileName == "TræSkov" && inventoryManager.toolbar.selectedSlot.itemName == "Axe")
                    {
                        tileManager.objectMap.SetTile(mousePos, null);
                        tileManager.interactableMap.SetTile(mousePos, tileManager.hiddenInteractableTile);
                        Instantiate(itemManager.items[3], mousePos, Quaternion.identity);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isDialogueBoxActive == false)
        {
            Boxcast();
            dialogueManager.StartDialogue();
        }
    }

    private void Boxcast()
    { 
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, lastDirection, boxcastOffset, layerMask);

        if (hit.transform.gameObject.CompareTag("NPC"))
        {
            dialogueManager.lines = hit.transform.gameObject.GetComponent<DialogueText>().characterLines;
            dialogueManager.CharacterIcon.GetComponent<Image>().sprite = hit.transform.gameObject.GetComponent<DialogueText>().characterIcon;
            dialogueManager.name = hit.transform.gameObject.GetComponent<DialogueText>().CharacterName;
            dialogueBox.SetActive(true);
            isDialogueBoxActive = true;
        }
        else
        {
            return;
        }

        if (hit.transform.gameObject.name == "Wombat Wellington" && GameManager.instance.questManager.activeQuestIndex == 0)
        {
            GameManager.instance.questManager.hasActiveQuest = true;
            GameManager.instance.questManager.activeQuestIndex = 1;
            GameManager.instance.questManager.questSetup();
        }
      /*  else if (hit.transform.gameObject.name == "Wombat Wellington" && GameManager.instance.questManager.activeQuestIndex == 1)
        {
            GameManager.instance.questManager.hasActiveQuest = false;
            GameManager.instance.questManager.BorgmesterQuestComplete();
        } */
     }

    private void OnDrawGizmosSelected()
    {
        if (boxCollider == null)
            return;

        Vector2 movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        movementDirection.Normalize();

        if (movementDirection == Vector2.zero)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)movementDirection * boxcastOffset, new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y, 1f));
    }

    Vector3Int GetMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        return tileManager.hoverMap.WorldToCell(mousePos);
    }

    private void ChangeCursorTile()
    {
        Vector3Int mousePos = GetMousePosition();

        isHoverActive();

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