using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    private int turn = 1; // ×プレイヤーのターンかどうか
    private int turnNum = 0;
    public int[] board;
    private string resultText = "";

    public GameObject[] lines;

    private void Start()
    {
        board = new int[9];
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

            if (hit2D)
            {
                GameObject cell = hit2D.collider.gameObject;
                Debug.Log("ヒットしました！");

                if (!cell.GetComponent<CellController>().IsOccupied())
                {
                    turnNum++;
                    PlaceSymbol(cell);
                }
            }
        }

        if(Input.GetKey(KeyCode.Space) && turn == 0)
        {
            SceneManager.LoadScene(1);
        }
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
        if(cell.GetComponent<CellController>().MarkCell(turn))
        {
            int index = cell.GetComponent<CellController>().getIndex();
            board[index] = turn;
            isFinish();
            turn = -turn;
        } 
    }

    private bool isFinish()
    {
        if(board[0] != 0 && board[0] == board[1] && board[0] == board[2])
        {
            lines[0].SetActive(true);
            setText();
            return true;
        }else if(board[3] != 0 && board[3] == board[4] && board[3] == board[5])
        {
            lines[1].SetActive(true);
            setText();
            return true;
        }else if(board[6] != 0 && board[6] == board[7] && board[6] == board[8])
        {
            lines[2].SetActive(true);
            setText();
            return true;
        }else if(board[0] != 0 && board[0] == board[3] && board[0] == board[6])
        {
            lines[3].SetActive(true);
            setText();
            return true;
        }else if(board[1] != 0 && board[1] == board[4] && board[1] == board[7])
        {
            lines[4].SetActive(true);
            setText();
            return true;
        }else if(board[2] != 0 && board[2] == board[5] && board[2] == board[8])
        {
            lines[5].SetActive(true);
            setText();
            return true;
        }else if(board[0] != 0 && board[0] == board[4] && board[0] == board[8])
        {
            lines[6].SetActive(true);
            setText();
            return true;
        }else if(board[2] != 0 && board[2] == board[4] && board[2] == board[6])
        {
            lines[7].SetActive(true);
            setText();
            return true;
        }else if (turnNum == 9)
        {
            resultText = "引き分けです";
            turn = 0;
            return true;
        }
        return false;
    }

    private void setText(){
        if (turn == 1)
        {
            resultText = "Oの勝ちです";
        }
        else if (turn == -1)
        {
            resultText = "Xの勝ちです";
        }
        turn = 0;
    }

}
