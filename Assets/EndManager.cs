using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndManager : MonoBehaviour
{
    public TMP_Text endMessageText;
    public TMP_Text finalInfoText;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("playerName", "Player");
        string word = PlayerPrefs.GetString("wordToGuess", "");

        endMessageText.text = "Game Over!";

        if (string.IsNullOrEmpty(word))
        {
            finalInfoText.text = "Player: " + playerName;
        }
        else
        {
            finalInfoText.text =
                "Player: " + playerName +
                "\nThe word was: " + word;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}