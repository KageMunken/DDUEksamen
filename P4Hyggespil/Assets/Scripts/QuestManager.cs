using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public GameObject[] QuestCollectibleIcon;
    public GameObject[] QuestCollectibleText;
    public Sprite[] collectableImage;

    public int[] questGoal;
    public int[] totalAquiredItems;
    public int activeQuestIndex;
    public int numberOfObjectivesInQuest;
    public int[] aquiredItemsInInventory;
    public int[] aquiredItemsInToolbar;


    public bool hasActiveQuest;

    public string[] itemToCollect;


    // Start is called before the first frame update
    void Start()
    {
        hasActiveQuest = false;
        activeQuestIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void questSetup()
    {
        switch (activeQuestIndex)
        {
            case 1:
                    numberOfObjectivesInQuest = 2;
                    questGoal = new int[] {10, 5};
                    QuestCollectibleIcon[0].GetComponent<Image>().sprite = collectableImage[2];
                    QuestCollectibleIcon[1].GetComponent<Image>().sprite = collectableImage[1];
                    QuestCollectibleIcon[2].GetComponent<Image>().sprite = collectableImage[0];
                    QuestCollectibleIcon[3].GetComponent<Image>().sprite = collectableImage[0];


                    itemToCollect = new string[] {"Wood","Stone"};
                    QuestCollectibleText[0].GetComponent<TMP_Text>().text = $"{totalAquiredItems[0]} / {questGoal[0]}";
                    QuestCollectibleText[1].GetComponent<TMP_Text>().text = $"{totalAquiredItems[1]} / {questGoal[1]}";
                    QuestCollectibleText[2].GetComponent<TMP_Text>().text = string.Empty;
                    QuestCollectibleText[3].GetComponent<TMP_Text>().text = string.Empty;
                break;
            case 2:
           /*         questGoal[0] = 9;
                    QuestCollectibleIcon[0].GetComponent<Image>().sprite = collectableImage[0];
                    itemToCollect[0] = "Stone";
                    QuestCollectibleText[0].GetComponent<TMP_Text>().text = ($"{totalAquiredItems} / {questGoal}");
                    borgmesterQuestSetupHasRun = true; */
                break;
        }
    }

    public void checkForQuestItems()
    {
        for (int y = 0; y < numberOfObjectivesInQuest; y++)
        {
            for (int i = 0; i < GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots.Count; i++)
            {
                if (GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots[i].itemName == itemToCollect[y])
                {
                    aquiredItemsInInventory[y] = GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots[i].count;
                }
            }

            for (int i = 0; i < GameManager.instance.player.inventoryManager.GetInventoryByName("Toolbar").slots.Count; i++)
            {
                if (GameManager.instance.player.inventoryManager.GetInventoryByName("Toolbar").slots[i].itemName == itemToCollect[y])
                {
                    aquiredItemsInToolbar[y] = GameManager.instance.player.inventoryManager.GetInventoryByName("Toolbar").slots[i].count;
                }
            }
            totalAquiredItems[y] = aquiredItemsInInventory[y] + aquiredItemsInToolbar[y];
            QuestCollectibleText[y].GetComponent<TMP_Text>().text = ($"{totalAquiredItems[y]} / {questGoal[y]}");
        }
    }

    public void BorgmesterQuestComplete()
    {
        activeQuestIndex++;
    }

    public void KøkkenQuestComplete()
    {
        activeQuestIndex++;
    }
}

   
