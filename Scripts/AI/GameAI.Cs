namespace HayDay
{
  class Choice
  {
    private int index;
    private int clockwise;

    public Choice(int index, int clockwise)
    {
      this.index = index;
      this.clockwise = clockwise;
    }

    public Choice()
    {
      this.index = 0;
      this.clockwise = 0;
    }

    public void setChoice(int index, int clockwise)
    {
      this.index = index;
      this.clockwise = clockwise;
    }

    public int getIndex()
    {
      return this.index;
    }

    public int getClockwise()
    {
      return this.clockwise;
    }

    public void setIndex(int index)
    {
      this.index = index;
    }

    public void setClockwise(int clockwise)
    {
      this.clockwise = clockwise;
    }

    public void toString()
    {
      Console.WriteLine("Index: " + this.index + " Clockwise: " + (this.clockwise == 1 ? "clockwise" : "anticlockwise"));
    }
  }

  class BanCo
  {
    private int[] board = new int[12];
    public int goal_1 = 0;
    public int goal_2 = 0;

    // ham get board
    public int[] getBoard()
    {
      return board;
    }

    // set board
    public void setBoard(int[] arr)
    {
      for (int i = 0; i < 12; i++)
      {
        board[i] = arr[i];
      }
    }

    public void setBoardByIdx(int idx, int value)
    {
      board[idx] = value;
    }

    // ham khoi tao BanCo
    public BanCo()
    {
      for (int i = 0; i < 12; i++)
      {
        if (i == 5 || i == 11)
        {
          board[i] = 10; // Quan la 10
        }
        else
        {
          board[i] = 5; // Dan la 5
        }
      }
    }

    //  ham khoi tao board voi dau vao la mang 14 phan tu
    public BanCo(int[] arr, int num)
    {
      for (int i = 2; i < 14; i++)
      {
        board[i - 2] = arr[i];
      }
    }


    //  ham khoi tao board voi dau vao la mang 12 phan tu
    public BanCo(int[] arr)
    {
      for (int i = 0; i < 12; i++)
      {
        board[i] = arr[i];
      }
    }

    // In ban co
    public void PrintBoard()
    {
      Console.Write("     ");

      for (int i = 0; i < 6; i++)
      {
        Console.Write(board[i] + "   ");
      }
      Console.WriteLine();
      for (int i = 11; i >= 6; i--)
      {
        Console.Write(board[i] + "   ");
      }
      Console.WriteLine();
      Console.WriteLine("Điểm người 1: " + goal_1 + "   Điểm người 2: " + goal_2);
      Console.WriteLine();
    }

    public void PrintBoardTestMove()
    {
      Console.Write("     ");
      for (int i = 0; i < 6; i++)
      {
        Console.Write(board[i] + "   ");
      }
      Console.WriteLine();
      for (int i = 11; i >= 6; i--)
      {
        Console.Write(board[i] + "   ");
      }
      Console.WriteLine();
      Console.WriteLine();

    }


    // choi co
    public int Move(int index, int clockwise)
    {
      if (index < 0 || index > 11)
      {
        Console.WriteLine("Ô " + index + " không có quân!");
        return 0;
      }
      if (index == 5 || index == 11)
      {
        Console.WriteLine("Ô " + index + "không thể chọn!");
        return 0;
      }
      if (board[index] == 0)
      {
        Console.WriteLine("Ô " + index + " không có quân!");
        return 0;
      }


      int temp = 0;
      int i = index;
      int step;
      int reward = 0;

      if (clockwise == 1)
      {
        step = 1;  // Theo chieu kim dong ho
      }
      else
      {
        step = -1; // nguoc chieu kim dong ho
      }

      // Rải quân
      while (board[i] > 0 && i != 5 && i != 11)
      {
        temp = board[i];
        board[i] = 0;
        while (temp > 0)
        {
          // Console.WriteLine(i);
          i = (i + step) % 12;
          if (i < 0)
          {
            i += 12;
          }
          board[i]++;
          temp--;
          // PrintBoardTestMove();
        }
        i = (i + step) % 12;
        if (i < 0)
        {
          i += 12;
        }

      }
      // rải hết quân  => đến ô trống đầu tiên
      int j = (i + step) % 12;
      if (j < 0)
      {
        j += 12;
      }

      // Console.WriteLine("An quan thoi");
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
        // PrintBoardTestMove();
      }


      return reward;
    }


  }




  class Ai
  {
    public static BanCo banCo = new BanCo();

    // player 1: Người chơi
    // player 2: Máy chơi

    public static bool checkEmpty(int[] board, int player)
    {
      if (player == 1)
      {
        for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
        {
          if (board[i] > 0)  // neu o con dan
          {
            return false;
          }
        }
      }
      else
      {
        for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
        {
          if (board[i] > 0)  // neu o con dan
          {
            return false;
          }
        }
      }
      return true;
    }

    public static void UpdateEmptyBoard(BanCo boardTemp)
    {

      if (checkEmpty(boardTemp.getBoard(), 2))
      {
        for (int k = 0; k < 5; k++) // Xét các ô của người từ 6 đến 10
        {
          boardTemp.setBoardByIdx(k, 1);
        }
      }
      if (checkEmpty(boardTemp.getBoard(), 1))
      {
        for (int k = 6; k < 11; k++) // Xét các ô của người từ 6 đến 10
        {
          boardTemp.setBoardByIdx(k, 1);
        }
      }
    }


    public static int[] EasyAi(int[] arr, int player)
    {
      BanCo Board = new BanCo(arr, 14);
      Board.goal_1 = arr[0];
      Board.goal_2 = arr[1];

      List<int> lsBox = new List<int>(); // list box còn dân
      if (player == 1)
      {
        for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
        {
          if (Board.getBoard()[i] > 0)  // neu o con dan
          {
            lsBox.Add(i);
          }
        }
      }
      else
      {
        for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
        {
          if (Board.getBoard()[i] > 0)  // neu o con dan
          {
            lsBox.Add(i);
          }
        }
      }

      Random rd = new Random();

      return new int[] { lsBox[rd.Next(0, lsBox.Count)], rd.Next(0, 2) }; // random 1 ô và chiều bất kỳ
    }


    public static int[] MediumAi(int[] arr, int player)
    {
      BanCo Board = new BanCo(arr, 14);
      Board.goal_1 = arr[0];
      Board.goal_2 = arr[1];

      int[] board = (int[])Board.getBoard().Clone();
      List<int> lsBox = new List<int>(); // list box còn dân

      // Xét các ô còn dân
      if (player == 1)
      {
        for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
        {
          if (Board.getBoard()[i] > 0)  // neu o con dan
          {
            lsBox.Add(i);
          }
        }
      }
      else if (player == 2)
      {
        for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
        {
          if (Board.getBoard()[i] > 0)  // neu o con dan
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
          BanCo boardTemp = new BanCo(board);   // Khởi tạo bàn cờ tạm đế đi thử
          int reward = boardTemp.Move(i, j);  // Di chuyển quân ở ô i theo chiều j, điểm số sau lượt đi lưu vào biến reward
          if (!map.ContainsKey(reward))  // Nếu chưa có key reward trong map
          {
            map[reward] = new List<Choice>(); // Khởi tạo list mới
          }
          map[reward].Add(new Choice(i, j)); // Thêm vào list
        }
      }



      List<Choice> firstList = map.FirstOrDefault().Value; // Lấy list dự đoán có điêm số cao nhất
      Random rd = new Random();
      Choice choice = firstList[rd.Next(0, firstList.Count)];

      return new int[] { choice.getIndex(), choice.getClockwise() }; // Random 1 trong các dự đoán
    }

    public static int[] BanCoToArr(BanCo banCo)
    {
      int[] arr = new int[14];
      arr[0] = banCo.goal_1;
      arr[1] = banCo.goal_2;
      for (int i = 2; i < 14; i++)
      {
        arr[i] = banCo.getBoard()[i - 2];
      }
      return arr;
    }

    public static int[] HardAi(int[] arr, int player)
    {

      BanCo Board = new BanCo(arr, 14);
      Board.goal_1 = arr[0];
      Board.goal_2 = arr[1];

      int[] board = (int[])Board.getBoard().Clone();
      List<int> lsBox = new List<int>(); // list box còn dân


      if (player == 1)
      {
        for (int i = 6; i < 11; i++) // Xét các ô của người từ 6 đến 10
        {
          if (Board.getBoard()[i] > 0)  // neu o con dan
          {
            lsBox.Add(i);
          }
        }
      }
      else
      {
        for (int i = 0; i < 5; i++) // Xét các ô của máy từ 0 đến 4
        {
          if (Board.getBoard()[i] > 0)  // neu o con dan
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

      List<Choice> firstList = map.FirstOrDefault().Value; // Lấy list dự đoán có điêm số cao nhất
      Random rd = new Random();
      choice = firstList[rd.Next(0, firstList.Count)];
      return new int[] { choice.getIndex(), choice.getClockwise() }; // Random 1 trong các dự đoán
    }
  }
}