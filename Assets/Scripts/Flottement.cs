using UnityEngine;

public class Flottement : MonoBehaviour
{
    public float amplitude = 0.05f;
    public float vitesse = 2f;

    private Vector3 positionDepart;

    void Start()
    {
        positionDepart = transform.localPosition;
    }

    void Update()
    {
        float decalage = Mathf.Sin(Time.time * vitesse) * amplitude;
        transform.localPosition = positionDepart + new Vector3(0, decalage, 0);
    }
}