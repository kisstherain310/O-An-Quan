using UnityEngine;

public class BanCo
{
    private int[] board = new int[12];
    public int goal_1 = 0;
    public int goal_2 = 0;

    // Hàm lấy board
    public int[] GetBoard()
    {
        return board;
    }

    // Hàm set board
    public void SetBoard(int[] arr)
    {
        for (int i = 0; i < 12; i++)
        {
            board[i] = arr[i];
        }
    }

    public void SetBoardByIdx(int idx, int value)
    {
        board[idx] = value;
    }

    // Hàm khởi tạo BanCo
    public BanCo()
    {
        for (int i = 0; i < 12; i++)
        {
            if (i == 5 || i == 11)
            {
                board[i] = 10; // Quân là 10
            }
            else
            {
                board[i] = 5; // Dân là 5
            }
        }
    }

    // Hàm khởi tạo board với đầu vào là mảng 14 phần tử
    public BanCo(int[] arr, int num)
    {
        for (int i = 2; i < 14; i++)
        {
            board[i - 2] = arr[i];
        }
    }

    // Hàm khởi tạo board với đầu vào là mảng 12 phần tử
    public BanCo(int[] arr)
    {
        for (int i = 0; i < 12; i++)
        {
            board[i] = arr[i];
        }
    }

    // In bàn cờ

    // Chơi cờ
    public int Move(int index, int clockwise)
    {
        if (index < 0 || index > 11)
        {
            return 0;
        }
        if (index == 5 || index == 11)
        {
            return 0;
        }
        if (board[index] == 0)
        {
            return 0;
        }

        int temp = 0;
        int i = index;
        int step;
        int reward = 0;

        if (clockwise == 1)
        {
            step = 1;  // Theo chiều kim đồng hồ
        }
        else
        {
            step = -1; // Ngược chiều kim đồng hồ
        }

        // Rải quân
        while (board[i] > 0 && i != 5 && i != 11)
        {
            temp = board[i];
            board[i] = 0;
            while (temp > 0)
            {
                i = (i + step) % 12;
                if (i < 0)
                {
                    i += 12;
                }
                board[i]++;
                temp--;
            }
            i = (i + step) % 12;
            if (i < 0)
            {
                i += 12;
            }
        }

        // Rải hết quân  => đến ô trống đầu tiên
        int j = (i + step) % 12;
        if (j < 0)
        {
            j += 12;
        }

        while (board[i] == 0 && board[j] > 0)
        {
            reward += board[j];
            board[j] = 0;
            i = (j + step) % 12;
            if (i < 0)
            {
                i += 12;
            }
            j = (i + step) % 12;
            if (j < 0)
            {
                j += 12;
            }
        }

        return reward;
    }
}
