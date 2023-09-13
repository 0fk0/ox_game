using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviourPunCallbacks
{
    private int PLAYER1 = 0;
    private int PLAYER2 = 1;
    private int NO_PLAAYER = -1;
    private int turnNum = 0;
    public int[] board;
    private string resultText = "";

    public GameObject[] lines;

    private void Start()
    {
        board = new int[9];
        for (int i = 0; i < 9; i++){
            board[i] = -1;
        }

        lines = new GameObject[8];
        lines[0] = GameObject.Find("Line");
        lines[1] = GameObject.Find("Line (1)");
        lines[2] = GameObject.Find("Line (2)");
        lines[3] = GameObject.Find("Line (3)");
        lines[4] = GameObject.Find("Line (4)");
        lines[5] = GameObject.Find("Line (5)");
        lines[6] = GameObject.Find("Line (6)");
        lines[7] = GameObject.Find("Line (7)");

        foreach(GameObject line in lines){
            line.SetActive(false);
        }
    }
    private void Update()
    {
        // マウスの左ボタンがクリックされたら、マウス位置に応じて○または×を置く
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            int playerId = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerID"];
            if (hit2D && (turnNum % 2) == playerId)
            {
                GameObject cell = hit2D.collider.gameObject;
                if (board[cell.GetComponent<CellController>().getIndex()] == NO_PLAAYER)
                {
                    PlaceSymbol(cell);
                    photonView.RPC(nameof(countUpTurnNum), RpcTarget.All);
                }
            }
        }

        if(Input.GetKey(KeyCode.Space) && turnNum == 9)
        {
            SceneManager.LoadScene(1);
        }
    }

    [PunRPC]
    private void countUpTurnNum(){
        turnNum++;
    }

    [PunRPC]
    public void RpcSetBoard(int index, int ox){
        if (0 <= index && index <= 8){
            board[index] = ox;
        } else {
            Debug.Log("invalid index...");
        }
    }

    public int getBoard(int index){
        if (0 <= index && index <= 8){
            return board[index];
        } else {
            Debug.Log("invalid index...");
        }

        return -2;
    }


    private void OnGUI()
    {
        GUIStyle customStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 30 // フォントサイズを設定
        };
        customStyle.normal.textColor = Color.red; // 文字の色を設定
        // 結果を表示
        Rect resultRect = new Rect(Screen.width/4, 20, 400, 40);
        GUI.Label(resultRect, resultText,customStyle);
    }

    private void PlaceSymbol(GameObject cell)
    {
        int playerId = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerID"];
        if(cell.GetComponent<CellController>().MarkCell())
        {
            int index = cell.GetComponent<CellController>().getIndex();
            board[index] = playerId;
            isFinish();
        } 
    }

    private bool isFinish()
    {
        if(isLine(board[0], board[1], board[2]))
        {
            photonView.RPC(nameof(displayResult), RpcTarget.All, 0);
            return true;
        }else if(isLine(board[3], board[4], board[5]))
        {
            photonView.RPC(nameof(displayResult), RpcTarget.All, 1);
            return true;
        }else if(isLine(board[6], board[7], board[8]))
        {
            photonView.RPC(nameof(displayResult), RpcTarget.All, 2);
            return true;
        }else if(isLine(board[0], board[3], board[6]))
        {
            photonView.RPC(nameof(displayResult), RpcTarget.All, 3);
            return true;
        }else if(isLine(board[1], board[4], board[7]))
        {
            photonView.RPC(nameof(displayResult), RpcTarget.All, 4);
            return true;
        }else if(isLine(board[2], board[5], board[8]))
        {
            photonView.RPC(nameof(displayResult), RpcTarget.All, 5);
            return true;
        }else if(isLine(board[0], board[4], board[8]))
        {
            photonView.RPC(nameof(displayResult), RpcTarget.All, 6);
            return true;
        }else if(isLine(board[2], board[4], board[6]))
        {
            photonView.RPC(nameof(displayResult), RpcTarget.All, 7);
            return true;
        }else if (turnNum == 9)
        {
            resultText = "引き分けです";
            return true;
        }
        return false;
    }

    private bool isLine(int elem1, int elem2, int elem3)
    {
        return (elem1 != -1) && (elem1 == elem2) && (elem1 == elem3);
    }

    private void displayText(){
        if ((turnNum % 2) == PLAYER1)
        {
            resultText = "Oの勝ちです";
        }
        else if ((turnNum % 2) == PLAYER2)
        {
            resultText = "Xの勝ちです";
        }

        turnNum = 8;
    }

    [PunRPC]
    private void displayResult(int index){
        lines[index].SetActive(true);
        displayText();
    }

    public int getTurnNum(){
        return turnNum;
    }
}
