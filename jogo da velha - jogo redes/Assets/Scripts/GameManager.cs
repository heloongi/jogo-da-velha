using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] cells; // Os 9 botões do tabuleiro
    public TextMeshProUGUI statusText; // Texto que mostra o turno ou resultado
    public Button restartButton; // Botão de reiniciar

    private string currentPlayer = "X";
    private string[] board = new string[9];
    private bool gameOver = false;

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        for (int i = 0; i < cells.Length; i++)
        {
            int index = i; // necessário para capturar o valor correto
            cells[i].onClick.AddListener(() => ClickCell(index));
        }
        RestartGame();
    }

    void RestartGame()
    {
        for (int i = 0; i < 9; i++)
        {
            board[i] = "";
            var txt = cells[i].GetComponentInChildren<TextMeshProUGUI>();
            if (txt != null) txt.text = "";
            cells[i].interactable = true;
        }
        currentPlayer = "X";
        gameOver = false;
        statusText.text = "Turno: X";
    }

    public void ClickCell(int index)
    {
        if (board[index] != "" || gameOver)
            return;

        var txt = cells[index].GetComponentInChildren<TextMeshProUGUI>();
        if (txt != null)
        {
            board[index] = currentPlayer;
            txt.text = currentPlayer;
            cells[index].interactable = false;
        }

        if (CheckWin())
        {
            statusText.text = currentPlayer + " venceu!";
            gameOver = true;
        }
        else if (CheckDraw())
        {
            statusText.text = "Deu velha!";
            gameOver = true;
        }
        else
        {
            currentPlayer = (currentPlayer == "X") ? "O" : "X";
            statusText.text = "Turno: " + currentPlayer;
        }
    }

    bool CheckWin()
    {
        int[,] win = new int[,] {
            {0,1,2}, {3,4,5}, {6,7,8}, // linhas
            {0,3,6}, {1,4,7}, {2,5,8}, // colunas
            {0,4,8}, {2,4,6}           // diagonais
        };

        for (int i = 0; i < win.GetLength(0); i++)
        {
            int a = win[i, 0], b = win[i, 1], c = win[i, 2];
            if (board[a] != "" && board[a] == board[b] && board[b] == board[c])
                return true;
        }
        return false;
    }

    bool CheckDraw()
    {
        foreach (string s in board)
            if (s == "") return false;
        return true;
        
    }
}
