using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

public class Sudoku {
    const String inputFile = "puzzles.txt";

    public static void Main(String[] args) {
        bool done = false;
        while (!done) {
            int max = getMaxPuzzle();
            String? str = "";
            int puzzleNum = 0;
            do
            {
                Console.WriteLine($"Enter the puzzle number, up to {max - 1}: ");
                str = Console.ReadLine();
                puzzleNum = Int32.Parse(str);
            } while (puzzleNum > max - 1);
            Console.WriteLine($"You have chosen puzzle {puzzleNum}");
            List<int> puzzle = loadPuzzle(puzzleNum);
            puzzle=solvePuzzle(puzzle);
            display(puzzle);

            Console.WriteLine("Do you want to go again? Yes or No.");
            str = Console.ReadLine();
            str=str.ToUpper();
            switch (str) {
                case "YES": {
                        break;
                    }
                case "NO": {
                        done = true;
                        break;
                    }
                  
            }
        }
    }

    //display the puzzle
    public static void display(List<int> puzzle) {
        int count = 0;
        Console.WriteLine("solved puzzle: ");
        foreach (var v in puzzle) {
            Console.Write($"{v},");
            count++;
            if (count % 9 == 0) Console.WriteLine();
        }
    }

    //get the maximum number of puzzles
    public static int getMaxPuzzle() {
        int count=0;
        try
        {
            String[] lines = File.ReadAllLines(inputFile);
            count = Int32.Parse(lines[0]);
        }
        catch (Exception E) {
            Console.WriteLine("could not open input file.");
        }
        return count;         
    }

    //load a puzzle from the file
    public static List<int> loadPuzzle(int puzzleNum) {
        String[] lines ={""};
        try
        {
            lines = File.ReadAllLines(inputFile);
        }
        catch (Exception e) {
            Console.WriteLine("could not open input file.");
        }
        List<int> puzList = new List<int>();
        int puzzleCount = 0;
        int index = 1;
        int puzzleLineCount = 0;
        int cellLineCount = 0;
        do
        {

            index++;
            puzzleLineCount = 0;
            while (puzzleLineCount < 9) {
                String puzzleLine = lines[index];
                if (puzzleCount == puzzleNum)
                {
                    for (cellLineCount = 0; cellLineCount < 9; cellLineCount++) puzList.Add(puzzleLine[cellLineCount] - 48);
                }
                puzzleLineCount++;
                index++;
            }

            puzzleCount++;
        } while (puzzleCount <= puzzleNum);
        return puzList;
    }

    //solve the puzzle
    public static List<int> solvePuzzle(List<int> puzzle) {
        int[,] grid = new int[9, 9];
        int listIndex = 0;
        List<int> puzzleOut = new List<int>();
        for (int index = 0; index < 9; index++) {
            for (int subIndex = 0; subIndex < 9; subIndex++)
            {
                grid[index, subIndex] = puzzle[listIndex];
                listIndex++;
            }
        }
        solveSudoku(grid, 0, 0);
        for (int index = 0; index < 9; index++)
        {
            for (int subIndex = 0; subIndex < 9; subIndex++)
            {
                puzzleOut.Add(grid[index, subIndex]);
            }
        }

        return puzzleOut;
    }

    //recursive solver
    static bool solveSudoku(int[,] grid, int row, int col) {
        if (row == 8 && col == 9) return true;

        if (col == 9) {
            row++;
            col = 0;
        }

        if (grid[row, col] != 0) return solveSudoku(grid, row, col + 1);

        for (int num = 1; num < 10; num++) {
            if (isSafe(grid, row, col, num)) {
                grid[row, col] = num;
                if (solveSudoku(grid, row, col + 1)) return true;
            }

            grid[row, col] = 0;
        }

        return false;
    }

    //check if the matrix is safe
    static bool isSafe(int[,] grid, int row, int col, int num) {
        for (int x = 0; x <= 8; x++)
            if (grid[row, x] == num)
                return false;

        // Check if we find the same num
        // in the similar column ,
        // we return false
        for (int x = 0; x <= 8; x++)
            if (grid[x, col] == num)
                return false;

        // Check if we find the same num
        // in the particular 3*3
        // matrix, we return false
        int startRow = row - row % 3, startCol
          = col - col % 3;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (grid[i + startRow, j + startCol] == num)
                    return false;

        return true;
    }
}
