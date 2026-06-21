using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Taille de la grille")]
    public int largeur = 8;
    public int profondeur = 8;
    public float tailleCase = 1f;

    [Header("Le prefab de la case")]
    public GameObject casePrefab;

    [Header("Point de départ de la grille (coin)")]
    public Vector3 pointDepart = new Vector3(0, 0.05f, 2);

    [Header("Couleurs du damier")]
    public Material materialClair;
    public Material materialFonce;

    void Start()
    {
        GenererGrille();
    }

    void GenererGrille()
    {
        for (int x = 0; x < largeur; x++)
        {
            for (int z = 0; z < profondeur; z++)
            {
                Vector3 position = pointDepart + new Vector3(x * tailleCase, 0, z * tailleCase);
                GameObject nouvelleCase = Instantiate(casePrefab, this.transform);
                nouvelleCase.transform.localPosition = position;
                nouvelleCase.transform.localRotation = Quaternion.identity;
                nouvelleCase.name = "Case_" + x + "_" + z;

                Case scriptCase = nouvelleCase.GetComponent<Case>();
                if (scriptCase != null)
                {
                    scriptCase.coordX = x;
                    scriptCase.coordZ = z;
                }

                // Alternance des couleurs façon damier
                bool estClair = (x + z) % 2 == 0;
                Renderer renderer = nouvelleCase.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = estClair ? materialClair : materialFonce;
                }
            }
        }
    }
}