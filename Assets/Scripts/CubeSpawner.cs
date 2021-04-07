using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private Sprite sprite;
    private Texture2D texture2D;
    [SerializeField] private GameObject cube;
    [SerializeField] private CollectibleCube collectibleCubePrefab;

    private List<CollectibleCube> collactableCubeList = new List<CollectibleCube>();

    int totalCubeCount;
    int coloredCubeCount;

    private void Start()
    {
        int levelCount = LevelManager.instance.levelCount;
        sprite = LevelManager.instance.levelInfoAsset.levelSprites[levelCount];

        texture2D = sprite.texture;
        for (int x = 0; x < texture2D.width; x++)
        {
            for (int y = 0; y < texture2D.height; y++)
            {
                Color color = texture2D.GetPixel(x, y);
                if (color.a == 0) 
                {
                    continue;
                }
                GameObject instantiatedCube = Instantiate(cube, transform);
                instantiatedCube.transform.localPosition = new Vector3(x - texture2D.width / 2, 0, y - texture2D.height / 2);
                totalCubeCount++;
                instantiatedCube.GetComponent<PlaceholderCube>().color = color;
            }
        }
        OrderCollectibleCubes();
    }

    private void OnCubeCollected()
    {
        coloredCubeCount++;
        Debug.Log(coloredCubeCount + " Cubes Colored of total " + totalCubeCount);
        if (coloredCubeCount == totalCubeCount)
        {
            for (int i = 0; i < coloredCubeCount; i++)
            {
                collactableCubeList[i].OnCubeCollected -= OnCubeCollected;
            }
        }
        
    }

    private void OrderCollectibleCubes()
    {
        int i = 0;
        
        for(int z = -3; z < 0; z++)
        {
            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    if (i >= totalCubeCount) return;
                    i++;
                    CollectibleCube instantiatedCube = Instantiate(collectibleCubePrefab,new Vector3(x,y,z), Quaternion.identity,transform);
                    instantiatedCube.OnCubeCollected += OnCubeCollected;
                    collactableCubeList.Add(instantiatedCube);
                    
                }
            }
        }
        
        
    }
}
