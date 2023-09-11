using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ConnectNetwork : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームへ参加しました!!");
    }

    // ランダムなルームへの参加が失敗した時に呼ばれるコールバック
    public override void OnJoinRandomFailed(short returnCode, string message){
        var initialProps = new ExitGames.Client.Photon.Hashtable();
        initialProps["NextPlayerID"] = 1;

        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.CustomRoomProperties = initialProps;

        // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
        PhotonNetwork.CreateRoom(null, roomOptions);
    }
}
