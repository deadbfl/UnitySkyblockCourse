using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance;
    public Dictionary<eTag, ItemSO> blokcs = new Dictionary<eTag, ItemSO>();
    [SerializeField] private Transform blockParent;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float distance;
    [SerializeField] private Vector3 pos;
    private void Awake()
    {
        instance = this;

        var blockSO = Resources.LoadAll("1) ScriptableObjects/Blocks", typeof(ItemSO)).Cast<ItemSO>().ToArray();

        for (int i = 0; i < blockSO.Length; i++)
        {
            if (blokcs.ContainsKey(blockSO[i].identifier))
                Debug.LogError(string.Format("{0} this block already defined.", blockSO[i].identifier));
            else
                blokcs.Add(blockSO[i].identifier, blockSO[i]);
        }
    }

    public ItemSO GetBlockData(eTag key)
    {
        if (!blokcs.ContainsKey(key))
            return null;

        return blokcs[key];
    }
    private void Update()
    {


        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, distance))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (hit.collider.CompareTag("Block"))
                {
                    Vector3 spawnPos = hit.transform.position;
                    spawnPos += hit.normal;
                    var obj = PoolingManager.instance.OpenObject("Block", spawnPos,blockParent);
                }

            }
        }


        Debug.DrawRay(playerCamera.position, playerCamera.forward * distance, Color.red);
    }
}
