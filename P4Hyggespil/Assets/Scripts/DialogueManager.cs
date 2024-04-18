using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameTextComponent;
    public string[] lines;
    public string characterNameHolder;
    public float textSpeed;
    public GameObject DialogueBox;
    public GameObject CharacterIcon;
    public GameObject CharacterName;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.player.isDialogueBoxActive == true)
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

   public void StartDialogue()
    {
        textComponent.text = string.Empty;
        nameTextComponent.text = string.Empty;
        nameTextComponent.text = name;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            DialogueBox.SetActive(false);
            GameManager.instance.player.isDialogueBoxActive = false;
        }
    }
}
