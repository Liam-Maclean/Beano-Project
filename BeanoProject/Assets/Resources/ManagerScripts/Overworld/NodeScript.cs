using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public enum Biome { Residential, School, Park, Forest, Downtown, Beanoland };

    public int nodeID;
    public Biome biomeID;
    public bool isGame;

    void Start()
    {
        Vector3 newPos = transform.position;
        newPos.z = newPos.y;
        transform.position = newPos;
    }

    public int GetID()
    {
        return nodeID;
    }

    public int GetBiomeType()
    {
        return (int)biomeID;
    }

    public bool IsGame()
    {
        return isGame;
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
