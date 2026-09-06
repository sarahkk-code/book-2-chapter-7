using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PreferencesManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_Dropdown wordLengthDropdown;
    public TMP_Dropdown timeDropdown;

    public void StartGame()
    {
        PlayerPrefs.SetString("playerName", playerNameInput.text);

        int wordLength = wordLengthDropdown.value + 3;
        PlayerPrefs.SetInt("wordLength", wordLength);

        int time = 15;

        switch (timeDropdown.value)
        {
            case 0:
                time = 15;
                break;
            case 1:
                time = 30;
                break;
            case 2:
                time = 45;
                break;
            case 3:
                time = 60;
                break;
            case 4:
                time = 90;
                break;
            case 5:
                time = 120;
                break;
        }

        PlayerPrefs.SetInt("timeLimit", time);

        SceneManager.LoadScene("wordGame");
    }
}