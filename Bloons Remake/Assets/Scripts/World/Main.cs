using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    public float money = 0f;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;

    public static bool MenuOpen { get; private set; }

    public float globalSpeedMultiplier = 1f;

    public SceneGrabber SceneGrabber { get; private set; }
    public BloonHirachy Hirachy { get; private set; }

    public static Main Current => FindObjectOfType<Main>();

    public Player Player { get; private set; }

    private void Awake()
    {
        SceneGrabber = GetComponent<SceneGrabber>();
        Hirachy = GetComponent<BloonHirachy>();
        Player = SceneGrabber.Player;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            MenuOpen = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            MenuOpen = false;
        }
        moneyText.text = $"${money}";

        if (!Player) { return; }
        livesText.text = $"Lives: {Player.lives}";
    }
}
