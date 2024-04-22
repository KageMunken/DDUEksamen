using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

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
    [SerializeField] GameObject tutorialBox;
    public GameObject buildingChoice;
    

    private BoxCollider2D boxCollider;

    public float boxcastOffset = 1f;
    public bool isDialogueBoxActive;
    public bool hasTalkedWithMayor;
    public LayerMask layerMask;
    private Vector2 lastDirection;

    private void Start()
    {
        tileManager = GameManager.instance.tileManager;
        itemManager = GameManager.instance.itemManager;
        dialogueManager = GameManager.instance.dialogueManager;

        boxCollider = GetComponent<BoxCollider2D>();
        isDialogueBoxActive = false;
        hasTalkedWithMayor = false;
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

        if (Input.GetKeyDown(KeyCode.T))
        {
            tutorialBox.SetActive(true);
        }
        
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

            if (hit.transform.gameObject.name == "Wombat Wellington" && GameManager.instance.questManager.activeQuestIndex == 0 && hasTalkedWithMayor == false)
            {
                hasTalkedWithMayor = true;
            }
            else if (hit.transform.gameObject.name == "Wombat Wellington" && GameManager.instance.questManager.activeQuestIndex == 0 && hasTalkedWithMayor == true)
            {
                hit.transform.gameObject.GetComponent<DialogueText>().characterLines = new string[]
                {
                "Ah, once again, my sincerest gratitude for graciously offering your assistance! One must admit, life becomes rather cumbersome without the luxury of opposable thumbs.",

                "To erect the esteemed town hall, we shall require the procurement of ten logs and five stones. It appears you are already in possession of the requisite tools.",

                "Fear not, for we are blessed with a cadre of skilled builders who shall fashion the materials into the town hall we so desperately require.",

                "Alas, whilst our builders excel in their trade, their aptitude for felling timber and quarrying stone leaves much to be desired.",

                "To acquire said materials, simply venture forth to the desired resource, equip the appropriate tool, and make use of it upon the desired resource. Thereafter, claim your spoils.",

                "Upon amassing the requisite ten logs and five stones, do return forthwith and consult our esteemed builders. They shall then commence the construction of our venerable town hall.",

                "Regrettably, our builders possess a rather indifferent stance toward architectural aesthetics, hence you must impart your preference regarding the design.",

                "For the previous iteration of our town hall, we opted for charming blue accents, a choice which I must say, resonated deeply with my sensibilities.",

                "Now, without further ado, I bid you to gather the required ten logs and five stones and may fortune favor your endeavors."
                };
                GameManager.instance.questManager.activeQuestIndex = 1;
                GameManager.instance.questManager.questSetup();
            }
            else if (hit.transform.gameObject.name == "Wombat Wellington" && GameManager.instance.questManager.activeQuestIndex == 1 && GameManager.instance.questManager.isQuestComplete == true)
            {
                GameManager.instance.questManager.activeQuestIndex++;
                buildingChoice.gameObject.SetActive(true);
                GameManager.instance.questManager.isQuestComplete = false;
                return;
            }

            if(hit.transform.gameObject.name == "Wombat Wellington" && GameManager.instance.questManager.rådhus1Selected == true)
            {
                hit.transform.gameObject.GetComponent<DialogueText>().characterLines = new string[]
                           {
                               "What an exquisite and majestic place our town hall has become, truly befitting of our esteemed community.",

                               "Alas, the construction of the town hall has drawn to a close. Your invaluable assistance is deeply appreciated.",

                               "(He looks at you and nods.)",

                               "We dare to hope that your benevolence knows no bounds, as our esteemed cook Wombat Wilfred, desires the establishment of a kitchen facility, so that we may once again savor delectable cuisine."
                           };
            }
            else if (hit.transform.gameObject.name == "Wombat Wellington" && GameManager.instance.questManager.rådhus2Selected == true)
            {
                hit.transform.gameObject.GetComponent<DialogueText>().characterLines = new string[]
                           {
                               "Oh my, what an exquisite manifestation of architecture!",

                               "At long last, our community boasts a beautiful blue town hall, a testament to your generosity. ",

                               "We dare to hope that your benevolence knows no bounds, as our esteemed cook Wombat Wilfred, desires the establishment of a kitchen facility, so that we may once again savor delectable cuisine."
                           };
            }

            if(hit.transform.gameObject.name == "Wombat Wilfred" && GameManager.instance.questManager.activeQuestIndex == 2)
            {
            hit.transform.gameObject.GetComponent<DialogueText>().characterLines = new string[]
                {
                    "Alright, we need 15 pieces of wood and 8 stones to get this kitchen going. Lots of materials for lots of space.",

                    "Hurry out! I need some good fucking food, don’t just stand there like a big fucking muffin."
                };
            GameManager.instance.questManager.activeQuestIndex = 3;
            GameManager.instance.questManager.questSetup();
            }
            else if(hit.transform.gameObject.name == "Wombat Wilfred" && GameManager.instance.questManager.activeQuestIndex == 3 && GameManager.instance.questManager.isQuestComplete == true)
            {
                GameManager.instance.questManager.activeQuestIndex++;
                buildingChoice.gameObject.SetActive(true);
                GameManager.instance.questManager.isQuestComplete = false;
                return;
            }

            if(hit.transform.gameObject.name == "Wombat Wilfred" && GameManager.instance.questManager.køkken1Selected == true)
            {
                hit.transform.gameObject.GetComponent<DialogueText>().characterLines = new string[]
                {
                    "If I relaxed, if I took my foot off the gas, I would probably die. But this made my life so much easier.",

                    "Thanks for the help chap."
                };
            }
            else if (hit.transform.gameObject.name == "Wombat Wilfred" && GameManager.instance.questManager.køkken2Selected == true)
            {
                hit.transform.gameObject.GetComponent<DialogueText>().characterLines = new string[]
                {
                    "What am I, a minimalist?",

                    "It’ll do, thanks for the help chap."
                };
            }


        if (hit.transform.gameObject.CompareTag("NPC"))
        {
            dialogueManager.lines = hit.transform.gameObject.GetComponent<DialogueText>().characterLines;
            dialogueManager.CharacterIcon.GetComponent<Image>().sprite = hit.transform.gameObject.GetComponent<DialogueText>().characterIcon;
            dialogueManager.characterNameHolder = hit.transform.gameObject.GetComponent<DialogueText>().CharacterName;
            dialogueBox.SetActive(true);
            isDialogueBoxActive = true;
        }
        else
        {
            return;
        }



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

        if(mousePlayerDistance.magnitude - 10 < 0.35)
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