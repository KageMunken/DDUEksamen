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
    int aquiredItems;

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
                QuestCollectibleText.GetComponent<TMP_Text>().text = ($"{aquiredItems} / {questGoal}");
                borgmesterQuestSetupHasRun = true;
            }

            BorgmesterQuest();
            Debug.Log(aquiredItems);

        }
        else if (activeQuestIndex == 1)
        {
            if (køkkenQuestSetupHasRun == false)
            {
                questGoal = 9;
                QuestCollectibleIcon.GetComponent<Image>().sprite = collectableImage[0];
                QuestCollectibleText.GetComponent<TMP_Text>().text = ($"{aquiredItems} / {questGoal}");
                borgmesterQuestSetupHasRun = true;
            }
            KøkkenQuest();
        }
    }

    void BorgmesterQuest()
    {
    
       // aquiredItems = GameManager.instance.player.itemManager.
    
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

   
