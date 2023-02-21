using UnityEngine;

[CreateAssetMenu(fileName = "Maps", menuName = "ScriptableObjects/Maps")]
public class MapsSO : ScriptableObject
{
    private int[,] _mattrixUser;
    private int[,] _mattrixEnemy;

    public void TakeMattrixUser(int[,] mattrixPlayer)
    {
        _mattrixUser = mattrixPlayer;
    }

    public void TakeMattrixEnemy(int[,] mattrixEnemy)
    {
        _mattrixEnemy = mattrixEnemy;
    }

    public void ShowConsole()
    {
        string st = "Player:\n";

        for(int j = 0; j < _mattrixUser.GetLength(1); j++)
        {
            for(int i = 0; i < _mattrixUser.GetLength(0); i++)
            {
                st += $"{_mattrixUser[i, j]} ";
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

    public int GetLengthMaps()
    {
        if (_mattrixEnemy.GetLength(1) == _mattrixEnemy.GetLength(0) && 
            _mattrixEnemy.GetLength(0) == _mattrixUser.GetLength(1) &&
            _mattrixUser.GetLength(1) == _mattrixUser.GetLength(0))
        {
            return _mattrixEnemy.GetLength(1);
        }
        else
        {
            throw new System.Exception("Матрицы карт врага и игрока должны быть квадратными и одинаковыми");
        }
    }

    public int GetCellUser(int i, int j)
    {
        return _mattrixUser[i, j];
    }

    public int GetEnemyCell(int i, int j)
    {
        return _mattrixEnemy[i, j];
    }
}
