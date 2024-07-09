using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item SO", menuName = "Scriptable Objects/Item SO")]
public class ItemSO : ScriptableObject
{
    public eTag identifier;
    public eTag[] tags;
    public eTool bestTool; // En iyi kırabilen araç
    public eNeedTool minBreakMaterial; // Olmasi gereken minimum malzeme
    public float blastResistence; // patlama direnci
    public float hardness; // kırma zorluğu
    public Texture texture;
}
