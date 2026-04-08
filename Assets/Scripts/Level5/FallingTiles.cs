using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingTiles : MonoBehaviour
{
    [SerializeField] private Tilemap playableAreaTilemap;
    [SerializeField] private Transform playerFeet;
    [SerializeField] private TileBase crackedTile;
    [SerializeField] private Level1UIManager uiManager;

    [SerializeField] private float startDelay = 1.5f;
    [SerializeField] private float crackDelay = 0.2f;
    [SerializeField] private float breakDelay = 0.4f;

    private bool fallingActive = false;
    private bool playerDead = false;
    private Vector3Int lastCell;
    private bool initialized = false;
    private HashSet<Vector3Int> triggeredCells = new HashSet<Vector3Int>();

    private void Start()
    {
        if (uiManager == null)
            uiManager = FindObjectOfType<Level1UIManager>();

        StartCoroutine(BeginFallingAfterDelay());
    }

    private IEnumerator BeginFallingAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        fallingActive = true;
    }

    private void Update()
    {
        if (!fallingActive || playerDead)
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
        
        // if player is standing on a cell that already has no tile, die
        if (!playableAreaTilemap.HasTile(currentCell))
        {
            KillPlayer();
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

        if (!fallingActive || playerDead)
            yield break;

        if (crackedTile != null && playableAreaTilemap.HasTile(cell))
            playableAreaTilemap.SetTile(cell, crackedTile);

        yield return new WaitForSeconds(breakDelay);

        if (!fallingActive || playerDead)
            yield break;

        if (playableAreaTilemap.HasTile(cell))
            playableAreaTilemap.SetTile(cell, null);

        // check if player is still on the tile that just broke
        Vector3Int playerCell = playableAreaTilemap.WorldToCell(playerFeet.position);
        if (playerCell == cell)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        if (playerDead)
            return;

        playerDead = true;
        fallingActive = false;
        StopAllCoroutines();

        IDamageable damageable = playerFeet.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageable.Die();
            return;
        }

        if (uiManager != null)
        {
            uiManager.PlayerDied();
        }
    }

    public void StopFalling()
    {
        fallingActive = false;
        StopAllCoroutines();
    }
}