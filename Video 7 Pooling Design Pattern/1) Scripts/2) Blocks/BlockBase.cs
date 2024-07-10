using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour
{
    private ItemSO blockProperities;

    public ItemSO BlockProperities { get { return blockProperities; } set { blockProperities = value; } }

    [SerializeField] private eTag identifier;
    [SerializeField] private Renderer renderer;

    private void Start()
    {
        var tempData = BlockManager.instance.GetBlockData(identifier);

        if(tempData != null)
        {
            BlockProperities = tempData;
            renderer.material.mainTexture = blockProperities.texture;
        }
    }
}
