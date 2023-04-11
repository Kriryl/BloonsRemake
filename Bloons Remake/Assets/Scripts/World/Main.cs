using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    public float money = 0f;
    public TextMeshProUGUI moneyText;

    public SceneGrabber SceneGrabber { get; private set; }
    public BloonHirachy Hirachy { get; private set; }

    public static Main Current => FindObjectOfType<Main>();

    private void Awake()
    {
        SceneGrabber = GetComponent<SceneGrabber>();
        Hirachy = GetComponent<BloonHirachy>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        moneyText.text = $"${money}";
    }
}
