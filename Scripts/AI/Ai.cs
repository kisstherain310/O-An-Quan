using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    public static BanCo banCo = new BanCo();

    // player 1: Người chơi
    // player 2: Máy chơi

    public static bool CheckEmpty(int[] board, int player)
    {
        if (player == 1)
        {
            for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
            {
                if (board[i] > 0)  // nếu ô còn dân
                {
                    return false;
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
            {
                if (board[i] > 0)  // nếu ô còn dân
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static void UpdateEmptyBoard(BanCo boardTemp)
    {

        if (CheckEmpty(boardTemp.GetBoard(), 2))
        {
            for (int k = 0; k < 5; k++) // Xét các ô của người từ 6 đến 10
            {
                boardTemp.SetBoardByIdx(k, 1);
            }
        }
        if (CheckEmpty(boardTemp.GetBoard(), 1))
        {
            for (int k = 6; k < 11; k++) // Xét các ô của người từ 6 đến 10
            {
                boardTemp.SetBoardByIdx(k, 1);
            }
        }
    }


    public static int[] EasyAi(int[] arr, int player)
    {
        BanCo Board = new BanCo(arr, 14);
        Board.goal_1 = arr[0];
        Board.goal_2 = arr[1];

        List<int> lsBox = new List<int>(); // list box còn dân

        // Xét các ô còn dân
        if (player == 1)
        {
            for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
            {
                if (Board.GetBoard()[i] > 0)  // neu o con dan
                {
                    lsBox.Add(i);
                }
            }
        }
        else if (player == 2)
        {
            for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
            {
                if (Board.GetBoard()[i] > 0)  // neu o con dan
                {
                    lsBox.Add(i);
                }
            }
        }

        // Tạo một đối tượng Random
        System.Random rd = new System.Random();

        // Sử dụng đối tượng Random để sinh số ngẫu nhiên
        int randomIndex = rd.Next(0, lsBox.Count);
        int randomClockwise = rd.Next(0, 2);

        return new int[] { lsBox[randomIndex], randomClockwise };
    }




    public static int[] MediumAi(int[] arr, int player)
    {
        BanCo Board = new BanCo(arr, 14);
        Board.goal_1 = arr[0];
        Board.goal_2 = arr[1];

        int[] board = (int[])Board.GetBoard().Clone();
        List<int> lsBox = new List<int>(); // list box còn dân

        // Xét các ô còn dân
        if (player == 1)
        {
            for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
            {
                if (Board.GetBoard()[i] > 0)  // nếu ô còn dân
                {
                    lsBox.Add(i);
                }
            }
        }
        else if (player == 2)
        {
            for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
            {
                if (Board.GetBoard()[i] > 0)  // nếu ô còn dân
                {
                    lsBox.Add(i);
                }
            }
        }

        // khởi tạo SortedDictionary với key là điểm số sau mỗi lượt và được sắp xếp giảm dần
        SortedDictionary<int, List<Choice>> map = new SortedDictionary<int, List<Choice>>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        foreach (int i in lsBox)  // Duyệt các ô còn dân
        {
            for (int j = 0; j < 2; j++)  // Duyệt 2 chiều quay
            {
                BanCo boardTemp = new BanCo(board);   // Khởi tạo bàn cờ tạm để thử
                int reward = boardTemp.Move(i, j);  // Di chuyển quân ở ô i theo chiều j, điểm số sau lượt đi lưu vào biến reward
                if (!map.ContainsKey(reward))  // Nếu chưa có key reward trong map
                {
                    map[reward] = new List<Choice>(); // Khởi tạo list mới
                }
                map[reward].Add(new Choice(i, j)); // Thêm vào list
            }
        }

        List<Choice> firstList = map.GetEnumerator().Current.Value; // Lấy list dự đoán có điểm số cao nhất
        Choice choice = firstList[Random.Range(0, firstList.Count)];

        return new int[] { choice.getIndex(), choice.getClockwise() }; // Random 1 trong các dự đoán
    }

    public static int[] BanCoToArr(BanCo banCo)
    {
        int[] arr = new int[14];
        arr[0] = banCo.goal_1;
        arr[1] = banCo.goal_2;
        for (int i = 2; i < 14; i++)
        {
            arr[i] = banCo.GetBoard()[i - 2];
        }
        return arr;
    }

    public static int[] HardAi(int[] arr, int player)
    {

        BanCo Board = new BanCo(arr, 14);
        Board.goal_1 = arr[0];
        Board.goal_2 = arr[1];

        int[] board = (int[])Board.GetBoard().Clone();
        List<int> lsBox = new List<int>(); // list box còn dân


        if (player == 1)
        {
            for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
            {
                if (Board.GetBoard()[i] > 0)  // nếu ô còn dân
                {
                    lsBox.Add(i);
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
            {
                if (Board.GetBoard()[i] > 0)  // nếu ô còn dân
                {
                    lsBox.Add(i);
                }
            }
        }

        SortedDictionary<int, List<Choice>> map = new SortedDictionary<int, List<Choice>>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        Choice choice = new Choice(0, 0);
        foreach (int i in lsBox)
        {
            for (int j = 0; j < 2; j++)
            {
                BanCo boardTemp = new BanCo(board); // Khởi tạo bàn cờ tạm
                int reward = boardTemp.Move(i, j); // Di chuyển quân ở ô i theo chiều j, điểm số sau lượt đi lưu vào biến reward
                UpdateEmptyBoard(boardTemp);

                int[] choiceArr = MediumAi(BanCoToArr(boardTemp), 1);
                choice = new Choice(choiceArr[0], choiceArr[1]);//  Dùng hàm MediumAi để dự đoán nước đi của người chơi
                reward -= boardTemp.Move(choice.getIndex(), choice.getClockwise()); // Điểm số sau khi người chơi đi, ta muốn tối thiểu hóa điểm số này nên coi giá trị là âm
                UpdateEmptyBoard(boardTemp);

                choiceArr = MediumAi(BanCoToArr(boardTemp), 1);
                choice = new Choice(choiceArr[0], choiceArr[1]); // Dự đoán nước đi của máy sau khi người chơi đi
                reward += boardTemp.Move(choice.getIndex(), choice.getClockwise());   // Điểm số sau khi máy đi
                UpdateEmptyBoard(boardTemp);

                if (!map.ContainsKey(reward))
                {
                    map[reward] = new List<Choice>();
                }
                map[reward].Add(new Choice(i, j));
            }
        }

        List<Choice> firstList = map.GetEnumerator().Current.Value; // Lấy list dự đoán có điểm số cao nhất
        choice = firstList[Random.Range(0, firstList.Count)];
        return new int[] { choice.getIndex(), choice.getClockwise() }; // Random 1 trong các dự đoán
    }
}
