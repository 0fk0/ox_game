using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellController : MonoBehaviourPunCallbacks
{
    private int PLAYER1 = 0;
    private int PLAYER2 = 1;
    private GameObject gameManager;
    private GameObject oObject, xObject;
    private int index;
    PhotonView gManagerPhotonView;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gManagerPhotonView = gameManager.GetComponent<PhotonView>();

        oObject = transform.Find("o").gameObject;
        xObject = transform.Find("x").gameObject; 

        oObject.SetActive(false);
        xObject.SetActive(false);

        int x = (int)transform.position.x / 2 + 1;
        int y = -(int)transform.position.y / 2 + 1;
        index = x + y * 3;
    }

    // セルに印を付ける
    public bool MarkCell()
    {
        int playerId = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerID"];
        GManager gManager = gameManager.GetComponent<GManager>();
        int turnNum = gManager.getTurnNum();

        if (gManager.getBoard(index) == -1){
            gManagerPhotonView.RPC("RpcSetBoard", RpcTarget.All, index, playerId);
            if ((playerId == PLAYER1) && ((turnNum % 2) == PLAYER1))
            {
                // oの印を表示する処理
                if (oObject != null)
                {
                    photonView.RPC(nameof(RpcDisplayO), RpcTarget.All);
                    return true;
                }
            }
            else if((playerId == PLAYER2) && ((turnNum % 2) == PLAYER2))
            {
                // xの印を表示する処理表示にする
                if (xObject != null)
                {
                    photonView.RPC(nameof(RpcDisplayX), RpcTarget.All);
                    return true;
                }
            }
        }
        
        return false;
    }

    public int getIndex()
    {
        return index;
    }

    [PunRPC]
    private void RpcDisplayO(){
        oObject.SetActive(true);
    }

    [PunRPC]
    private void RpcDisplayX(){
        xObject.SetActive(true);
    }
}
