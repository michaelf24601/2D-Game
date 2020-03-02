using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int width;
    public int height;
    public float scale = 20f;
    public GameObject water;
    public GameObject sand;
    public GameObject grass;
    public GameObject rock;

    private GameObject map;

    private int[,] tileMap;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("TileMap");
       tileMap = new int[width,height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int value = CalculateHeight(x, y);
                GameObject obj = water;
                tileMap[x, y] = value;
                if(value == 0)
                    obj = grass;
                if (value == 1)
                    obj = sand;
                if (value == 2)
                    obj = water;
                if (value == 3)
                    obj = rock;
                Instantiate(obj, new Vector2(x, y), Quaternion.identity);
            }
        }
    }

    private int CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        float f = Mathf.PerlinNoise(xCoord, yCoord);

        if (f > 0.75)
            return 0;
        if (f <= 0.75 && f > 0.5)
            return 1;
        if (f <= 0.5 && f > 0.25)
            return 2;
        return 3;
    }
}
