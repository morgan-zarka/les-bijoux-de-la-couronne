using System.Collections.Generic;
using UnityEngine;

public class CheminCorrect : MonoBehaviour
{
    // Liste des coordonnées (x, z) qui sont SÛRES
    // Le reste du damier (8x8) est piégé
    public List<Vector2Int> casesSures = new List<Vector2Int>
    {
        new Vector2Int(0, 0),
        new Vector2Int(0, 1),
        new Vector2Int(1, 1),
        new Vector2Int(1, 2),
        new Vector2Int(1, 3),
        new Vector2Int(2, 3),
        new Vector2Int(3, 3),
        new Vector2Int(3, 4),
        new Vector2Int(3, 5),
        new Vector2Int(4, 5),
        new Vector2Int(5, 5),
        new Vector2Int(5, 6),
        new Vector2Int(5, 7),
        new Vector2Int(6, 7),
        new Vector2Int(7, 7),
    };

    // Vérifie si une case donnée fait partie du chemin sûr
    public bool EstSure(int x, int z)
    {
        return casesSures.Contains(new Vector2Int(x, z));
    }
}