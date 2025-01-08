using Unity.Netcode;
using UnityEngine;

public class GameVisualManager : NetworkBehaviour
{

    private const float GRID_SIZE = 3.1f;

    [SerializeField] private GameObject crossPrefab;
    [SerializeField] private GameObject circlePrefab;



    private void Start()
    {
        GameManager.Instance.OnClickedOnGridPosition += GameManager_OnClickedOnGridPosition; ;
    }

    private void GameManager_OnClickedOnGridPosition(object sender, GameManager.ClickedOnGridPositionEventArgs e)
    {
        Debug.Log("GameManager_OnClickedOnGridPosition");
        SpawnObjectRpc(e.x, e.y, e.playerType);
    }

    [Rpc(SendTo.Server)]
    private void SpawnObjectRpc(int x, int y, GameManager.PlayerType playerType)
    {
        Debug.Log($"SpawnObject {playerType}");

        GameObject prefab;
        switch(playerType)
        {
            case GameManager.PlayerType.Cross:
                prefab = crossPrefab;
                break;
            case GameManager.PlayerType.Circle:
                prefab = circlePrefab;
                break;
            default:
                return;
        }


        GameObject spawnedCrossObject = Instantiate(prefab, GetGridWorldPosition(x, y), Quaternion.identity);
        spawnedCrossObject.transform.GetComponent<NetworkObject>().Spawn(true);
    }

    private Vector2 GetGridWorldPosition(int x, int y)
    {
        return new Vector2(-GRID_SIZE + x * GRID_SIZE, -GRID_SIZE + y * GRID_SIZE);
    }
}
