using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] public Tilemap objectMap;
    [SerializeField] public Tilemap interactableMap;
    [SerializeField] public Tilemap hoverMap;

    public Tile hiddenInteractableTile;
    public Tile plowedTile;
    public Tile interactableHoverTile;
    public Tile nonInteractableHoverTile;

    

    private void Start()
    {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin)
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
