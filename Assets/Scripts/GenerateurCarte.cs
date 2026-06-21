using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateurCarte : MonoBehaviour
{
    [Header("Taille")]
    public int tailleCase = 40;
    public int grilleX = 8;
    public int grilleZ = 16;

    [Header("Référence à l'image (mets-la même si désactivée)")]
    public RawImage rawImage;

    private List<Vector2Int> chemin = new List<Vector2Int>
    {
        new Vector2Int(0,0), new Vector2Int(0,1),
        new Vector2Int(1,1), new Vector2Int(1,2),
        new Vector2Int(1,3), new Vector2Int(2,3),
        new Vector2Int(3,3), new Vector2Int(3,4),
        new Vector2Int(3,5), new Vector2Int(4,5),
        new Vector2Int(5,5), new Vector2Int(5,6),
        new Vector2Int(5,7), new Vector2Int(6,7),
        new Vector2Int(6,8), new Vector2Int(6,9),
        new Vector2Int(5,9), new Vector2Int(5,10),
        new Vector2Int(4,10), new Vector2Int(4,11),
        new Vector2Int(3,11), new Vector2Int(3,12),
        new Vector2Int(2,12), new Vector2Int(2,13),
        new Vector2Int(1,13), new Vector2Int(1,14),
        new Vector2Int(0,14), new Vector2Int(0,15)
    };

    void Awake()
    {
        if (rawImage != null)
            rawImage.texture = GenererTexture();
    }

    Texture2D GenererTexture()
    {
        int largeur = tailleCase * grilleX;
        int hauteur = tailleCase * grilleZ;
        Texture2D tex = new Texture2D(largeur, hauteur);

        for (int x = 0; x < grilleX; x++)
        {
            for (int z = 0; z < grilleZ; z++)
            {
                bool estClair = (x + z) % 2 == 0;
                Color couleur = estClair ? Color.white : Color.black;

                if (chemin.Contains(new Vector2Int(x, z)))
                    couleur = Color.yellow;

                for (int px = 0; px < tailleCase; px++)
                    for (int py = 0; py < tailleCase; py++)
                        tex.SetPixel(x * tailleCase + px, z * tailleCase + py, couleur);
            }
        }

        tex.Apply();
        return tex;
    }
}