using UnityEngine;

[CreateAssetMenu(fileName = "Maps", menuName = "ScriptableObjects/Maps")]
public class MapsSO : ScriptableObject
{
    private int[,] _mattrixPlayer;
    private int[,] _mattrixEnemy;

    public void TakeMattrixPlayer(int[,] mattrixPlayer)
    {
        _mattrixPlayer = mattrixPlayer;
    }

    public void TakeMattrixEnemy(int[,] mattrixEnemy)
    {
        _mattrixEnemy = mattrixEnemy;
    }

    public void ShowConsole()
    {
        string st = "Player:\n";

        for(int j = 0; j < _mattrixPlayer.GetLength(1); j++)
        {
            for(int i = 0; i < _mattrixPlayer.GetLength(0); i++)
            {
                st += $"{_mattrixPlayer[i, j]} ";
            }
            st += $"\n";
        }
        Debug.Log(st);

        st = "Enemy:\n";

        for (int j = 0; j < _mattrixEnemy.GetLength(1); j++) 
        {
            for (int i = 0; i < _mattrixEnemy.GetLength(0); i++)
            {
                st += $"{_mattrixEnemy[i, j]} ";
            }
            st += $"\n";
        }
        Debug.Log(st);
    }

    public int GetCellPlayer(int i, int j)
    {
        return _mattrixPlayer[i, j];
    }

    public int GetEnemyCell(int i, int j)
    {
        return _mattrixEnemy[i, j];
    }
}
