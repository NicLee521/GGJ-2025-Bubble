using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void HowTo() {
        SceneManager.LoadScene("How To");
    }

    public void Quit() {
        Application.Quit();
    }
}
