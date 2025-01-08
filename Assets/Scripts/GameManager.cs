using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is already a GameManager in the scene");
        }
    }

    public void OnGridPositionClicked(int x, int y)
    {
        Debug.Log($"Clicked in X: {x} Y: {y} ");
    }

}
