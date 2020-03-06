using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
public class BaseMapGenerator : MonoBehaviour
{
    public int SavedTileNumber;
    public int width;
    public int height;
    public float scale = 20f;
    public Tilemap baseMap;
    public Tile water;
    public Tile sand;
    public Tile sandGrassBorder;
    public Tile sandGrassCorner;
    public Tile grass;
    public Tile tallGrass;
    private int[,] mapNumArray;
    // Start is called before the first frame update
    void Start()
    {
        mapNumArray = new int[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int value = CalculateHeight(x, y);
                Tile tile = water;
                mapNumArray[x, y] = value;
                if (value == 0)
                    tile = tallGrass;
                if (value == 1)
                    tile = grass;
                if (value == 2)
                    tile = sand;
                if (value == 3)
                    tile = water;
                baseMap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        //SaveAssetMap(SavedTileNumber);
    }

    private int CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        float f = Mathf.PerlinNoise(xCoord, yCoord);

        if (f > 0.75)
            return 0;
        if (f <= 0.75 && f > 0.6)
            return 1;
        if (f <= 0.6 && f > 0.5)
            return 2;
        return 3;
    }

    public void SaveAssetMap(int SavedTileNumber)
    {
        string saveName = "TileMap_" + SavedTileNumber;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/" + saveName + ".prefab";
            if (PrefabUtility.SaveAsPrefabAsset(mf, savePath))
            {
                EditorUtility.DisplayDialog("Tilemap saved", "Your Tilemap was saved under" + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT saved", "An ERROR occured while trying to saveTilemap under" + savePath, "Continue");
            }


        }


    }
}
