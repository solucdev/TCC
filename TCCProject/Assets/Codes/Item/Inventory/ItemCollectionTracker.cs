using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemCollectionTracker : MonoBehaviour
{
    public static ItemCollectionTracker Instance;

    private HashSet<string> collectedItemNames = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public bool HasCollected(string itemID)
    {
        return collectedItemNames.Contains(itemID);
    }

    public void RegisterItem(string itemID)
    {
        collectedItemNames.Add(itemID);
    }

    private HashSet<string> amuletoPieces = new HashSet<string>();

    public void RegisterAmuletoPiece(string pieceID)
    {
        amuletoPieces.Add(pieceID);
        UpdateAmuletoObjective();
    }

    private void UpdateAmuletoObjective()
    {
        int count = amuletoPieces.Count;

        if (count < 3)
        {
            Objetivos.Instance.SetObjective($"Coletar as 3 peças do amuleto da seita ({count}/3)");
        }
        else
        {
            Objetivos.Instance.SetObjective("Colocar as peças no cofre do closet");
        }
    }
}