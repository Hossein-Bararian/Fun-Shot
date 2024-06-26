using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownMenu;
    [SerializeField] private Button muteButton;
    [SerializeField] private TextMeshProUGUI[] menuTexts;
    [SerializeField] private Image[] menuImage;
    [Space(35)]
    [SerializeField] private RTLTextMeshPro txtScore;
    [SerializeField] private RTLTextMeshPro txtCoin;
    
    public static int Coin;
    public static int Score;
    private Animator _scoreAnimator;

    private void Start()
    {
        Application.targetFrameRate = 80;
        _scoreAnimator = txtScore.GetComponent<Animator>();
        TutorialCheck();
    }

    private void Update()
    {
        txtCoin.text=PlayerPrefs.GetInt("Coin").ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            print("Reset");
            PlayerPrefs.SetInt("_selectedCharactersIndex", 0);
            PlayerPrefs.SetString("OwnCharacters", "0");
            GameManager.Coin = 0;
            PlayerPrefs.SetInt("Coin",0);
            print( "Coin number : "+GameManager.Coin);
            print("Coin prefs : "+PlayerPrefs.GetInt("Coin"));
            print("Coin prefs : "+PlayerPrefs.GetInt("_selectedCharactersIndex"));
            print("Coin prefs : "+PlayerPrefs.GetString("OwnCharacters"));
        }
    }

    public void UpdateScore(int value)
    {
        Score += value;
        txtScore.text = Score.ToString();
        _scoreAnimator.Play("GetScore");
        PlayerPrefs.SetInt("Score",Score);
    }
    private void TutorialCheck()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            foreach (var text in menuTexts)
            {
              text.color =new Color(1,1,1,0.3f);
            }
            foreach (var image in menuImage)
            {
               image.color=new Color(1,1,1,0.3f);
            }
            muteButton.interactable = false;
            muteButton.GetComponent<Animator>().Play("MusicLowOpacity");
            dropdownMenu.gameObject.GetComponent<Image>().color =new Color(1,1,1,0.3f);
            dropdownMenu.interactable = false;
        }
    }
}