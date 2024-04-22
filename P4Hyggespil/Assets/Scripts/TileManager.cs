using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap objectMap;
    public Tilemap interactableMap;
    public Tilemap hoverMap;

    public Tile hiddenInteractableTile;
    public Tile plowedTile;
    public Tile interactableHoverTile;
    public Tile nonInteractableHoverTile;

    private void OnLevelWasLoaded(int level)
    {
        objectMap = GameObject.Find("Objects").GetComponent<Tilemap>();
        interactableMap = GameObject.Find("InteractableMap").GetComponent<Tilemap>();
        hoverMap = GameObject.Find("HoverMap").GetComponent<Tilemap>();
    }

    private void Start()
    {
        objectMap = GameObject.Find("Objects").GetComponent<Tilemap>();
        interactableMap = GameObject.Find("InteractableMap").GetComponent<Tilemap>();
        hoverMap = GameObject.Find("HoverMap").GetComponent<Tilemap>();

        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            if(tile != null && tile.name == "Interactable_Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            } 
        }
    }

    public string GetTileName(Vector3Int mousePos)
    {
        TileBase tile = interactableMap.GetTile(mousePos);
        if (interactableMap != null && tile != null)
        {
                return tile.name;
        }

        tile = objectMap.GetTile(mousePos);
        if (objectMap != null && tile != null)
        {
                return tile.name;
        }

        return "";
    }
}
