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

    public GameObject[] buildingChoiceResourceIcon;
    public GameObject[] buildingChoiceResourceText;
    public Sprite[] buildingChoiceImages;
    public GameObject buildinChoiceShownImage;
    public GameObject buildingChoiceName;

    public int[] questGoals;
    public int activeQuestIndex;
    public int numberOfObjectivesInQuest;
    int[] aquiredItemsInInventory = new int[4];
    int[] totalAquiredItems = new int[4];
    int[] aquiredItemsInToolbar = new int[4];

    public bool hasActiveQuest;
    public bool isQuestComplete;
    public bool rådhus1Selected;
    public bool rådhus2Selected;
    public bool køkken1Selected;
    public bool køkken2Selected;

    public string[] itemToCollect;


    // Start is called before the first frame update
    void Start()
    {
        rådhus1Selected = false;
        rådhus2Selected = false;
        køkken1Selected = false;
        køkken2Selected = false;

        isQuestComplete = false;
        activeQuestIndex = 0;

        
        QuestCollectibleIcon[0].SetActive(false);
        QuestCollectibleIcon[1].SetActive(false);
        QuestCollectibleIcon[2].SetActive(false);
        QuestCollectibleIcon[3].SetActive(false);

        buildingChoiceResourceIcon[0].SetActive(false);
        buildingChoiceResourceIcon[1].SetActive(false);
        buildingChoiceResourceIcon[2].SetActive(false);
        buildingChoiceResourceIcon[3].SetActive(false);
    }

    public void questSetup()
    {
        switch (activeQuestIndex)
        {
            case 1:
                hasActiveQuest = true;
                    numberOfObjectivesInQuest = 2;
                    questGoals = new int[] {1, 2};
                    QuestCollectibleIcon[0].SetActive(true);
                    QuestCollectibleIcon[1].SetActive(true);

                    buildingChoiceResourceIcon[0].SetActive(true);
                    buildingChoiceResourceIcon[1].SetActive(true);


                    QuestCollectibleIcon[0].GetComponent<Image>().sprite = collectableImage[2];
                    QuestCollectibleIcon[1].GetComponent<Image>().sprite = collectableImage[1];
                    QuestCollectibleIcon[2].GetComponent<Image>().sprite = collectableImage[0];
                    QuestCollectibleIcon[3].GetComponent<Image>().sprite = collectableImage[0];

                    buildingChoiceResourceIcon[0].GetComponent<Image>().sprite = collectableImage[2];
                    buildingChoiceResourceIcon[1].GetComponent<Image>().sprite = collectableImage[1];
                    buildingChoiceResourceIcon[2].GetComponent<Image>().sprite = collectableImage[0];
                    buildingChoiceResourceIcon[3].GetComponent<Image>().sprite = collectableImage[0];

                    buildingChoiceResourceText[0].GetComponent<TMP_Text>().text = questGoals[0].ToString();
                    buildingChoiceResourceText[1].GetComponent<TMP_Text>().text = questGoals[1].ToString();

                buildinChoiceShownImage.GetComponent<Image>().sprite = buildingChoiceImages[0];
                buildingChoiceName.GetComponent<TMP_Text>().text = "Townhall";

                itemToCollect = new string[] {"Wood","Stone"};
                    QuestCollectibleText[0].GetComponent<TMP_Text>().text = $"{totalAquiredItems[0]}/{questGoals[0]}";
                    QuestCollectibleText[1].GetComponent<TMP_Text>().text = $"{totalAquiredItems[1]}/{questGoals[1]}";
                    QuestCollectibleText[2].GetComponent<TMP_Text>().text = string.Empty;
                    QuestCollectibleText[3].GetComponent<TMP_Text>().text = string.Empty;

                checkForQuestItems();
                break;
            case 3:
                hasActiveQuest = true;
                numberOfObjectivesInQuest = 2;
                questGoals = new int[] { 2, 1 };
                QuestCollectibleIcon[0].SetActive(true);
                QuestCollectibleIcon[1].SetActive(true);

                buildingChoiceResourceIcon[0].SetActive(true);
                buildingChoiceResourceIcon[1].SetActive(true);


                QuestCollectibleIcon[0].GetComponent<Image>().sprite = collectableImage[2];
                QuestCollectibleIcon[1].GetComponent<Image>().sprite = collectableImage[1];
                QuestCollectibleIcon[2].GetComponent<Image>().sprite = collectableImage[0];
                QuestCollectibleIcon[3].GetComponent<Image>().sprite = collectableImage[0];

                buildingChoiceResourceIcon[0].GetComponent<Image>().sprite = collectableImage[2];
                buildingChoiceResourceIcon[1].GetComponent<Image>().sprite = collectableImage[1];
                buildingChoiceResourceIcon[2].GetComponent<Image>().sprite = collectableImage[0];
                buildingChoiceResourceIcon[3].GetComponent<Image>().sprite = collectableImage[0];

                buildingChoiceResourceText[0].GetComponent<TMP_Text>().text = questGoals[0].ToString();
                buildingChoiceResourceText[1].GetComponent<TMP_Text>().text = questGoals[1].ToString();

                buildinChoiceShownImage.GetComponent<Image>().sprite = buildingChoiceImages[2];
                buildingChoiceName.GetComponent<TMP_Text>().text = "Kitchen";

                itemToCollect = new string[] { "Wood", "Stone" };
                QuestCollectibleText[0].GetComponent<TMP_Text>().text = $"{totalAquiredItems[0]}/{questGoals[0]}";
                QuestCollectibleText[1].GetComponent<TMP_Text>().text = $"{totalAquiredItems[1]}/{questGoals[1]}";
                QuestCollectibleText[2].GetComponent<TMP_Text>().text = string.Empty;
                QuestCollectibleText[3].GetComponent<TMP_Text>().text = string.Empty;

                checkForQuestItems();
                break;
        }
    }

    public void ClearQuestBoard()
    {
        hasActiveQuest = false;

        numberOfObjectivesInQuest = 0;

        QuestCollectibleIcon[0].SetActive(false);
        QuestCollectibleIcon[1].SetActive(false);
        QuestCollectibleIcon[2].SetActive(false);
        QuestCollectibleIcon[3].SetActive(false);

        QuestCollectibleText[0].GetComponent<TMP_Text>().text = string.Empty;
        QuestCollectibleText[1].GetComponent<TMP_Text>().text = string.Empty;
        QuestCollectibleText[2].GetComponent<TMP_Text>().text = string.Empty;
        QuestCollectibleText[3].GetComponent<TMP_Text>().text = string.Empty;

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

            QuestCollectibleText[y].GetComponent<TMP_Text>().text = $"{totalAquiredItems[y]}/{questGoals[y]}";

            if (totalAquiredItems[0] >= questGoals[0] && totalAquiredItems[1] >= questGoals[1])
            {
                isQuestComplete = true;
                Debug.Log("Quest Complete");
            }
        }
    }

    public void removeQuestItems()
    {
        for (int y = 0; y < numberOfObjectivesInQuest; y++)
        {
            for (int i = 0; i < GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots.Count; i++)
            {
                if (GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots[i].itemName == itemToCollect[y])
                {
                    GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots[i].count = GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots[i].count - questGoals[y];
                }
            }

            for (int i = 0; i < GameManager.instance.player.inventoryManager.GetInventoryByName("Toolbar").slots.Count; i++)
            {
                if (GameManager.instance.player.inventoryManager.GetInventoryByName("Toolbar").slots[i].itemName == itemToCollect[y])
                {
                    GameManager.instance.player.inventoryManager.GetInventoryByName("Toolbar").slots[i].count = GameManager.instance.player.inventoryManager.GetInventoryByName("Backpack").slots[i].count - questGoals[y];
                }
            }
        }
    }



    public void BuildingChoiceImageButton()
    {
        if (buildinChoiceShownImage.GetComponent<Image>().sprite == buildingChoiceImages[0])
        {
            buildinChoiceShownImage.GetComponent<Image>().sprite = buildingChoiceImages[1];
        }
        else if (buildinChoiceShownImage.GetComponent<Image>().sprite == buildingChoiceImages[1])
        {
            buildinChoiceShownImage.GetComponent<Image>().sprite = buildingChoiceImages[0];
        }

        else if (buildinChoiceShownImage.GetComponent<Image>().sprite == buildingChoiceImages[2])
        {
            buildinChoiceShownImage.GetComponent<Image>().sprite = buildingChoiceImages[3];
        }
        else if (buildinChoiceShownImage.GetComponent<Image>().sprite == buildingChoiceImages[3])
        {
            buildinChoiceShownImage.GetComponent<Image>().sprite = buildingChoiceImages[2];
        }
    }

    public void SelectBuilding()
    {
        if (buildinChoiceShownImage.GetComponent<Image>().sprite == buildingChoiceImages[0])
        {
            GameObject.Find("Buildings").transform.GetChild(0).gameObject.SetActive(true);
            rådhus1Selected = true;
        }
        else if (buildinChoiceShownImage.GetComponent<Image>().sprite == buildingChoiceImages[1])
        {
            GameObject.Find("Buildings").transform.GetChild(1).gameObject.SetActive(true);
            rådhus2Selected = true;
        }

        else if (buildinChoiceShownImage.GetComponent<Image>().sprite == buildingChoiceImages[2])
        {
            GameObject.Find("Buildings").transform.GetChild(2).gameObject.SetActive(true);
            køkken1Selected = true;
        }
        else if (buildinChoiceShownImage.GetComponent<Image>().sprite == buildingChoiceImages[3])
        {
            GameObject.Find("Buildings").transform.GetChild(3).gameObject.SetActive(true);
            køkken2Selected = true;
        }


        removeQuestItems();
        GameManager.instance.player.buildingChoice.gameObject.SetActive(false);

        ClearQuestBoard();

    }

    private void OnLevelWasLoaded(int level)
    {
        if(rådhus1Selected == true)
        {
            GameObject.Find("Buildings").transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (rådhus2Selected == true)
        {
            GameObject.Find("Buildings").transform.GetChild(1).gameObject.SetActive(true);
        }

        if (køkken1Selected == true)
        {
            GameObject.Find("Buildings").transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (køkken2Selected == true)
        {
            GameObject.Find("Buildings").transform.GetChild(3).gameObject.SetActive(true);
        }
    }
}

   
