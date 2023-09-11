using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private int PLAYER1 = 1;
    private int PLAYER2 = 2;
    private bool isOccupied = false;
    private GameObject oObject, xObject;
    private int index;

    private void Start()
    {
        oObject = transform.Find("o").gameObject;
        xObject = transform.Find("x").gameObject; 

        oObject.SetActive(false);
        xObject.SetActive(false);

        int x = (int)transform.position.x / 2 + 1;
        int y = -(int)transform.position.y / 2 + 1;
        index = x + y * 3;
    }

    // セルが占有されているかどうかを返す
    public bool IsOccupied()
    {
        return isOccupied;
    }

    // セルに印を付ける
    public bool MarkCell()
    {
        int PlayerId = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerID"];
        if (!isOccupied){
            isOccupied = true;

            if (PlayerId == PLAYER1)
            {
                // oの印を表示する処理
                if (oObject != null)
                {
                    oObject.SetActive(true);
                    return true;
                }
            }
            else if(PlayerId == PLAYER2)
            {
                // xの印を表示する処理表示にする
                if (xObject != null)
                {
                    xObject.SetActive(true);
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
}
