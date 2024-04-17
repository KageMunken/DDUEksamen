using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public GameObject QuestCollectibleIcon;
    public GameObject QuestCollectibleText;
    public Sprite[] collectableImage;
    int questGoal;
    int aquiredItemsInInventory;
    int aquiredItemsInToolbar;
    int totalAquiredItems;

    int activeQuestIndex;

    bool borgmesterQuestSetupHasRun;
    bool køkkenQuestSetupHasRun;

    // Start is called before the first frame update
    void Start()
    {
        activeQuestIndex = 0;
        borgmesterQuestSetupHasRun = false;
        køkkenQuestSetupHasRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeQuestIndex == 0)
        {
            if (borgmesterQuestSetupHasRun == false)
            {
                questGoal = 5;
                QuestCollectibleIcon.GetComponent<Image>().sprite = collectableImage[1];
                borgmesterQuestSetupHasRun = true;
            }

            BorgmesterQuest();
        }
        else if (activeQuestIndex == 1)
        {
            if (køkkenQuestSetupHasRun == false)
            {
                questGoal = 9;
                QuestCollectibleIcon.GetComponent<Image>().sprite = collectableImage[0];
                QuestCollectibleText.GetComponent<TMP_Text>().text = ($"{totalAquiredItems} / {questGoal}");
                borgmesterQuestSetupHasRun = true;
            }
            KøkkenQuest();
        }
    }

    void BorgmesterQuest()
    {
        for (int i = 0; i < GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots.Count; i++)
        {
            if (GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots[i].itemName == "Wood")
            {
                aquiredItemsInInventory = GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots[i].count;
            }
        }

        for (int i = 0; i < GameManager.instance.player.inventoryManager.GetInventoryByName("Toolbar").slots.Count; i++)
        {
            if (GameManager.instance.player.inventoryManager.GetInventoryByName("Toolbar").slots[i].itemName == "Wood")
            {
                aquiredItemsInToolbar = GameManager.instance.player.inventoryManager.GetInventoryByName("Toolbar").slots[i].count;
            }
        }


        totalAquiredItems = aquiredItemsInInventory + aquiredItemsInToolbar;

        QuestCollectibleText.GetComponent<TMP_Text>().text = ($"{totalAquiredItems} / {questGoal}");
    
    }

    void BorgmesterQuestComplete()
    {
        activeQuestIndex++;
    }

    void KøkkenQuest()
    {

    }

    void KøkkenQuestComplete()
    {
        activeQuestIndex++;
    }
}

   
