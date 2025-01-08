using System;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{

    public static GameManager Instance { get; private set; }

    public event EventHandler<ClickedOnGridPositionEventArgs> OnClickedOnGridPosition;

    public class ClickedOnGridPositionEventArgs : EventArgs
    {
        public int x;
        public int y;
        public PlayerType playerType;
    }

    public enum PlayerType
    {
        None,
        Cross,
        Circle,
    }

    private PlayerType localPlayerType;
    private PlayerType currentPlayablePlayerType;


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

    public override void OnNetworkSpawn()
    {
        if(NetworkManager.Singleton.LocalClientId == 0)
        {
            localPlayerType = PlayerType.Cross;
        } else
        {
            localPlayerType = PlayerType.Circle;
        }

        if(IsServer)
        {
            currentPlayablePlayerType = PlayerType.Cross;
        }
    }

    [Rpc(SendTo.Server)]
    public void ClickedOnGridPositionRpc(int x, int y, PlayerType playerType)
    {
        Debug.Log($"Clicked in X: {x} Y: {y} Player: {playerType}");

        if (playerType != currentPlayablePlayerType) return; // Not the turn of the player

        OnClickedOnGridPosition?.Invoke(this, new ClickedOnGridPositionEventArgs { x = x, y = y, playerType = playerType });

        switch (currentPlayablePlayerType)
        {
            default:
            case PlayerType.Cross:
                currentPlayablePlayerType = PlayerType.Circle;
                break;
            case PlayerType.Circle:
                currentPlayablePlayerType = PlayerType.Cross;
                break;
        }

    }

    public PlayerType GetLocalPlayerType()
    {
        return localPlayerType;
    }

}
