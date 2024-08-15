using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button joinButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button quitButton;
    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        joinButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
