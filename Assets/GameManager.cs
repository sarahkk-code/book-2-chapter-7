using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject letter;
    public GameObject cen;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultText;

    private string wordToGuess = "";
    private int lengthOfWordToGuess;

    private char[] lettersToGuess;
    private bool[] lettersGuessed;

    private float timeRemaining;
    private bool gameOver = false;

    private string[] wordsToGuess =
    {
        // 3 letters
        "cat", "dog", "sun",

        // 4 letters
        "book", "tree", "fish",

        // 5 letters
        "apple", "house", "chair",

        // 6 letters
        "school", "orange", "planet",

        // 7 letters
        "computer", "student", "teacher",

        // 8 letters
        "elephant", "building", "keyboard"
    };

    void Start()
    {
        cen = GameObject.Find("centerOfScreen");

        string playerName = PlayerPrefs.GetString("playerName", "Player");
        playerNameText.text = "Player: " + playerName;

        timeRemaining = PlayerPrefs.GetInt("timeLimit", 15);
        UpdateTimerText();

        resultText.gameObject.SetActive(false);

        initGame();
        initLetters();
    }

    void Update()
    {
        if (!gameOver)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                gameOver = true;

                timerText.text = "Time: 0";

                PlayerPrefs.SetString("gameResult", "TIME'S UP!");
                PlayerPrefs.SetString("finalWord", wordToGuess);

                SceneManager.LoadScene("wordGameEnd");
            }
            else
            {
                UpdateTimerText();
                checkKeyboard();
            }
        }
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
    }

    void initGame()
    {
        int selectedLength = PlayerPrefs.GetInt("wordLength", 3);

        string[] matchingWords = System.Array.FindAll(
            wordsToGuess,
            word => word.Length == selectedLength
        );

        int randomNumber = Random.Range(0, matchingWords.Length);

        wordToGuess = matchingWords[randomNumber].ToUpper();

        lengthOfWordToGuess = wordToGuess.Length;

        lettersToGuess = wordToGuess.ToCharArray();
        lettersGuessed = new bool[lengthOfWordToGuess];
    }

    void initLetters()
    {
        int nbLetters = lengthOfWordToGuess;

        for (int i = 0; i < nbLetters; i++)
        {
            GameObject l = Instantiate(
                letter,
                GameObject.Find("Canvas").transform
            );

            RectTransform rect = l.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector2(
                (i - (nbLetters - 1) / 2.0f) * 100,
                0
            );

            l.name = "letter" + (i + 1);
        }
    }

    void checkKeyboard()
    {
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            if (string.IsNullOrEmpty(Input.inputString))
                return;

            char letterPressed = Input.inputString[0];

            if (char.IsLetter(letterPressed))
            {
                letterPressed = char.ToUpper(letterPressed);

                for (int i = 0; i < lengthOfWordToGuess; i++)
                {
                    if (!lettersGuessed[i] &&
                        lettersToGuess[i] == letterPressed)
                    {
                        lettersGuessed[i] = true;

                        GameObject.Find("letter" + (i + 1))
                            .GetComponent<TextMeshProUGUI>()
                            .text = letterPressed.ToString();
                    }
                }

                CheckWin();
            }
        }
    }

    void CheckWin()
    {
        for (int i = 0; i < lengthOfWordToGuess; i++)
        {
            if (!lettersGuessed[i])
            {
                return;
            }
        }

        gameOver = true;

        PlayerPrefs.SetString("gameResult", "YOU WIN!");
        PlayerPrefs.SetString("finalWord", wordToGuess);

        SceneManager.LoadScene("wordGameEnd");
    }
}