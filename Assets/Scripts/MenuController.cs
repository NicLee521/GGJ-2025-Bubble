using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit() {
        Application.Quit();
    }
}
