using System.Collections.Generic;
using UnityEngine;

public class CheminCorrect : MonoBehaviour
{
    // Liste des coordonnées (x, z) qui sont SÛRES
    // Le damier fait maintenant 8 (largeur) x 16 (profondeur)
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
        new Vector2Int(6, 8),
        new Vector2Int(6, 9),
        new Vector2Int(5, 9),
        new Vector2Int(5, 10),
        new Vector2Int(4, 10),
        new Vector2Int(4, 11),
        new Vector2Int(3, 11),
        new Vector2Int(3, 12),
        new Vector2Int(2, 12),
        new Vector2Int(2, 13),
        new Vector2Int(1, 13),
        new Vector2Int(1, 14),
        new Vector2Int(0, 14),
        new Vector2Int(0, 15),
    };

    // Vérifie si une case donnée fait partie du chemin sûr
    public bool EstSure(int x, int z)
    {
        return casesSures.Contains(new Vector2Int(x, z));
    }
}