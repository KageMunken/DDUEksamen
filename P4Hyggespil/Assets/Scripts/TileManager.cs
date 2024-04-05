using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] public Tilemap interactableMap;
    [SerializeField] public Tilemap hoverMap;

    [SerializeField] private Tile hiddenInteractableTile;
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

    

   /* public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, plowedTile);
    } */

    public string GetTileName(Vector3Int mousePos)
    {
        if (interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(mousePos);

            if (tile != null)
            {
                return tile.name;
            }
        }

        return "";
    }
}
