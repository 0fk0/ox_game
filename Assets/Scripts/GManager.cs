using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    private int turn = 1; // ×プレイヤーのターンかどうか
    public int[] board;

    private void Start()
    {
        board = new int[9];
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
                    PlaceSymbol(cell);
                }
            }
        }
    }

    private void PlaceSymbol(GameObject cell)
    {
        if(cell.GetComponent<CellController>().MarkCell(turn))
        {
            int index = cell.GetComponent<CellController>().getIndex();
            board[index] = turn;
            isWin();
            turn = -turn;
        } 
    }

    private void isWin()
    {
        if((board[0] != 0 && board[0] == board[1] && board[0] == board[2])
        || (board[3] != 0 && board[3] == board[4] && board[3] == board[5])
        || (board[6] != 0 && board[6] == board[7] && board[6] == board[8])
        || (board[0] != 0 && board[0] == board[3] && board[0] == board[6])
        || (board[1] != 0 && board[1] == board[4] && board[1] == board[7])
        || (board[2] != 0 && board[2] == board[5] && board[2] == board[8])
        || (board[0] != 0 && board[0] == board[4] && board[0] == board[8])
        || (board[2] != 0 && board[2] == board[4] && board[2] == board[6]))
        {
            if(turn == 1)
            {
                Debug.Log("Oの勝ちです");
            }else if(turn == -1)
            {
                Debug.Log("Xの勝ちです");
            }
            turn = 0;
        }
    }

}
