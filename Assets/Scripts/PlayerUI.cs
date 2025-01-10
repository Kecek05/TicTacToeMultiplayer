using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject crossArrowGameObject;
    [SerializeField] private GameObject circleArrowGameObject;
    [SerializeField] private GameObject crossYouTxtGameObject;
    [SerializeField] private GameObject circleYouTxtGameObject;

    [SerializeField] private TextMeshProUGUI playerCrossScoreTxt;
    [SerializeField] private TextMeshProUGUI playerCircleScoreTxt;


    private void Awake()
    {
        crossArrowGameObject.SetActive(false);
        circleArrowGameObject.SetActive(false);
        crossYouTxtGameObject.SetActive(false);
        circleYouTxtGameObject.SetActive(false);

        playerCircleScoreTxt.text = "";
        playerCrossScoreTxt.text = "";
    }

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnCurrentPlayablePlayerTypeChanged += GameManager_OnCurrentPlayablePlayerTypeChanged;
        GameManager.Instance.OnScoresChanged += GameManager_OnScoresChanged;
    }

    private void GameManager_OnScoresChanged(object sender, System.EventArgs e)
    {
        GameManager.Instance.GetScores(out int playerCrossScore, out int playerCircleScore);

        playerCircleScoreTxt.text = playerCircleScore.ToString();
        playerCrossScoreTxt.text = playerCrossScore.ToString();
    }

    private void GameManager_OnGameStarted(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.GetLocalPlayerType() == GameManager.PlayerType.Cross)
        {
            crossYouTxtGameObject.SetActive(true);
        }
        else
        {
            circleYouTxtGameObject.SetActive(true);
        }

        playerCircleScoreTxt.text = "0";
        playerCrossScoreTxt.text = "0";

        UpdateCurrentArrow();
    }

    private void GameManager_OnCurrentPlayablePlayerTypeChanged(object sender, System.EventArgs e)
    {
        UpdateCurrentArrow();
    }

    private void UpdateCurrentArrow()
    {
        if(GameManager.Instance.GetCurrentPlayablePlayerType() == GameManager.PlayerType.Cross)
        {
            crossArrowGameObject.SetActive(true);
            circleArrowGameObject.SetActive(false);
        }
        else
        {
            crossArrowGameObject.SetActive(false);
            circleArrowGameObject.SetActive(true);
        }
    }
}
