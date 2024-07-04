using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
     protected Player player;

    protected virtual void Awake()
    {
        player = GetComponent<Player>();
    }
    
}
