using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    void AssignPlayerID()
    {
        int nextPlayerID = (int)PhotonNetwork.CurrentRoom.CustomProperties["NextPlayerID"];
        
        // プレイヤーのカスタムプロパティとしてIDを設定
        ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable
        {
            {"PlayerID", nextPlayerID}
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps);

        // 次のプレイヤーIDを更新
        ExitGames.Client.Photon.Hashtable roomUpdateProps = new ExitGames.Client.Photon.Hashtable
        {
            {"NextPlayerID", nextPlayerID + 1}
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomUpdateProps);
    }

    public override void OnJoinedRoom()
    {
        AssignPlayerID();
    }


}
