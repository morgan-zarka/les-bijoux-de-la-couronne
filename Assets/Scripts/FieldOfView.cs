using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour
{
    [Header("Paramètres de vision")]
    public float fov = 45f;
    public float viewDistance = 10f;
    public int rayCount = 50; // Plus ce nombre est élevé, plus le bord du mesh sera lisse
    public LayerMask obstacleMask; // Assurez-vous que vos murs sont sur ce Layer

    private Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate()
    {
        float halfFov = (fov / 2f) * Mathf.Deg2Rad;

        // +1 pour l'origine, +1 pour refermer le cercle de rayons, +1 pour le point central (bouchon avant)
        int vertexCount = rayCount + 3;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[rayCount * 6]; // 3 indices pour la face latérale, 3 pour la face avant

        // L'origine (Ampoule de la lampe)
        vertices[0] = Vector3.zero;

        // Position du point central au bout du cône pour fermer le volume (bouchon avant)
        RaycastHit hit;
        Vector3 globalCenterDir = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, globalCenterDir, out hit, viewDistance, obstacleMask))
            vertices[vertexCount - 1] = transform.InverseTransformPoint(hit.point);
        else
            vertices[vertexCount - 1] = Vector3.forward * viewDistance;

        for (int i = 0; i <= rayCount; i++)
        {
            // Répartition de 0 à 2 PI (Cercle complet)
            float angleAroundZ = ((float)i / rayCount) * Mathf.PI * 2f;

            // Formule mathématique d'un cône orienté vers l'avant (Axe Z)
            Vector3 direction = new Vector3(
                Mathf.Sin(halfFov) * Mathf.Cos(angleAroundZ),
                Mathf.Sin(halfFov) * Mathf.Sin(angleAroundZ),
                Mathf.Cos(halfFov)
            );

            Vector3 globalDirection = transform.TransformDirection(direction);
            Vector3 vertexPosition;

            // Détection du mur sur la circonférence
            if (Physics.Raycast(transform.position, globalDirection, out hit, viewDistance, obstacleMask))
            {
                vertexPosition = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                vertexPosition = direction * viewDistance;
            }

            vertices[i + 1] = vertexPosition;

            // Construction des triangles (parois latérales + bouchon avant)
            if (i > 0)
            {
                int baseIndex = (i - 1) * 6;

                // Face latérale (du centre vers les bords)
                triangles[baseIndex] = 0;
                triangles[baseIndex + 1] = i;
                triangles[baseIndex + 2] = i + 1;

                // Face avant (pour fermer le bout du cône comme un solide)
                triangles[baseIndex + 3] = vertexCount - 1;
                triangles[baseIndex + 4] = i + 1;
                triangles[baseIndex + 5] = i;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}