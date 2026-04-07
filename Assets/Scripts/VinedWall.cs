using UnityEngine;
using UnityEngine.Tilemaps;

public class VinedWall : MonoBehaviour
{
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Vector3Int[] tilesToRemove; //cells to clear


    public void RemoveWall()
    {
        foreach (Vector3Int tile in tilesToRemove)
        {
            wallTilemap.SetTile(tile,null); //set tile to null at each given coord
        }
        
        gameObject.SetActive(false); //disables the VinedWall GameObject after the wall is removed
    }
    
}
