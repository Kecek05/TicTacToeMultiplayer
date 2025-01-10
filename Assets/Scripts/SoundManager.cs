using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject placeSfxPrefab;
    [SerializeField] private GameObject winSfxPrefab;
    [SerializeField] private GameObject loseSfxPrefab;

    private void Start()
    {
        GameManager.Instance.OnPlacedObject += GameManager_OnPlacedObject;
        GameManager.Instance.OnGameWin += GameManager_OnGameWin;

    }

    private void GameManager_OnGameWin(object sender, GameManager.OnGameWinEventArgs e)
    {
        if(GameManager.Instance.GetLocalPlayerType() == e.winPlayerType)
        {
            GameObject sfxObject = Instantiate(winSfxPrefab);
            Destroy(sfxObject, 5f);
        }
        else
        {
            GameObject sfxObject = Instantiate(loseSfxPrefab);
            Destroy(sfxObject, 5f);
        }
    }

    private void GameManager_OnPlacedObject(object sender, System.EventArgs e)
    {
        GameObject sfxObject = Instantiate(placeSfxPrefab);
        Destroy(sfxObject, 5f);
    }
}
