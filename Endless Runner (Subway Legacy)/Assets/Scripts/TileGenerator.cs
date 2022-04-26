/*
*	TickLuck
*	All rights reserved
*/
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject[] Tile_Prefabs;
    [SerializeField] private int startTiles = 6;
    [SerializeField] private Transform player;

    private List<GameObject> ActiveTiles = new List<GameObject>();
    private float spawn_position = 0;
    private float tile_length;
    #endregion

    #region Unity Methods
    void Start()
    {
        if (Tile_Prefabs.Length > 0)
        {
            tile_length = Tile_Prefabs[0].transform.localScale.z;
        }
        else
        {
            tile_length = 40;
        }

        for (int i = 0; i < startTiles; i++)
        {
            // Spawning Default Tile
            if (i == 0)
                SpawnTile(Tile_Prefabs.Length - 1);

            SpawnTile(Random.Range(0, Tile_Prefabs.Length));
        }
    }

    void Update()
    {
        if (player.position.z - 40 > spawn_position - (startTiles * tile_length))
        {
            SpawnTile(Random.Range(0, Tile_Prefabs.Length));
            DeleteTile();
        }
    }
    #endregion

    private void SpawnTile(int tile_index)
    {
        if (tile_index < 0 || tile_index > Tile_Prefabs.Length)
            return;

        GameObject new_tile = Instantiate(Tile_Prefabs[tile_index], transform.forward * spawn_position, transform.rotation);

        ActiveTiles.Add(new_tile);
        spawn_position += tile_length;
    }

    private void DeleteTile()
    {
        ActiveTiles[0].SetActive(false);
        ActiveTiles.RemoveAt(0);
    }
}
