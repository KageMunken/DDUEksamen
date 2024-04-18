using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Item item = GetComponent<Item>();

            if (item != null)
            {
                player.inventoryManager.Add("Backpack", item);
                Destroy(this.gameObject);
            }

            if (GameManager.instance.questManager.activeQuestIndex == 0)
            {
                GameManager.instance.questManager.checkForQuestItems();
            }
            else if (GameManager.instance.questManager.activeQuestIndex == 1)
            {
                GameManager.instance.questManager.checkForQuestItems();
            }
        }
    }
}
