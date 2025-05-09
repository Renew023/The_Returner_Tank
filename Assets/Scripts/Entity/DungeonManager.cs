using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;
    public Dungeon pools;
    public Player player;

    private void Awake()
    {
        instance = this;
    }
}
