using UnityEngine;

public class NoticeManager : MonoBehaviour
{
    public GameObject panneauNotice;
    private static bool dejaAffichee = false;

    private void Start()
    {
        if (!dejaAffichee)
        {
            panneauNotice.SetActive(true);
            dejaAffichee = true;
        }
        else
        {
            panneauNotice.SetActive(false);
        }
    }

    void Update()
    {
        if (panneauNotice.activeSelf && UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
        {
            FermerNotice();
        }
    }

    public void FermerNotice()
    {
        panneauNotice.SetActive(false);
    }
}