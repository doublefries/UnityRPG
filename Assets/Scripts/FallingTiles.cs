using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingTiles : MonoBehaviour
{
    [SerializeField] private Tilemap playableAreaTilemap;
    [SerializeField] private Transform playerFeet;
    [SerializeField] private TileBase crackedTile;
    [SerializeField] private float crackDelay = 0.2f;
    [SerializeField] private float breakDelay = 0.4f;
    [SerializeField] private bool fallingActive = true;

    private Vector3Int lastCell;
    private bool initialized = false;
    private HashSet<Vector3Int> triggeredCells = new HashSet<Vector3Int>();

    private void Update()
    {
        if (!fallingActive)
            return;

        Vector3Int currentCell = playableAreaTilemap.WorldToCell(playerFeet.position);

        if (!initialized)
        {
            lastCell = currentCell;
            initialized = true;
            TryStartBreaking(currentCell);
            return;
        }

        if (currentCell != lastCell)
        {
            lastCell = currentCell;
            TryStartBreaking(currentCell);
        }
    }

    private void TryStartBreaking(Vector3Int cell)
    {
        if (!playableAreaTilemap.HasTile(cell))
            return;

        if (triggeredCells.Contains(cell))
            return;

        triggeredCells.Add(cell);
        StartCoroutine(BreakTile(cell));
    }

    private IEnumerator BreakTile(Vector3Int cell)
    {
        yield return new WaitForSeconds(crackDelay);

        if (!fallingActive)
            yield break;

        if (crackedTile != null && playableAreaTilemap.HasTile(cell))
        {
            playableAreaTilemap.SetTile(cell, crackedTile);
        }

        yield return new WaitForSeconds(breakDelay);

        if (!fallingActive)
            yield break;

        if (playableAreaTilemap.HasTile(cell))
        {
            playableAreaTilemap.SetTile(cell, null);
        }

        Vector3Int playerCell = playableAreaTilemap.WorldToCell(playerFeet.position);
        if (playerCell == cell)
        {
            Debug.Log("Player fell!");
            // Respawn / lose life / reset level here
        }
    }

    public void StopFalling()
    {
        fallingActive = false;
        StopAllCoroutines();
    }

    public void StartFalling()
    {
        fallingActive = true;
        initialized = false;
    }
}