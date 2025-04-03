class MyMatrix
{
    private Int32[,] matrix;
    private int rows, cols;
    public MyMatrix(int rows, int cols, int minVal, int maxVal)
    {
        (this.rows, this.cols) = (rows, cols);
        matrix = new Int32[rows, cols];
        Random random = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
             matrix[i, j] = random.Next(minVal, maxVal+1);
            }
        }
    }
   public void Fill()
    {
        Int32[,] filledMatrix = new Int32[rows, cols];
        Random random = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                filledMatrix[i, j] = random.Next()%100;
            }
        }
        matrix = filledMatrix;
    }
    public int this[int row, int col]
    {
        get
        {
            return matrix[row, col];
        }
        set
        {
            matrix[row, col] = value;
        }
    }
    public MyMatrix ChangeSize(int newRows, int newCols, int minVal, int maxVal)
    {
        MyMatrix newMatrix = new(newRows, newCols,0,0);
        Random random = new();
        for (int i = 0; i < newRows; i++)
        {
            for (int j = 0; j < newCols; j++)
            {
                if (i >= rows ||  j >= cols)
                {
                    newMatrix[i,j] = random.Next(minVal,maxVal+1);
                }
                else newMatrix[i, j] = this[i, j];
            }
        }
        return newMatrix;
    }
    public void ShowPartialy(int startRow,int endRow, int startCol, int endCol)
    {
        for (int i = startRow; i <= endRow && i < rows; i++)
        {
            for (int j = startCol; j <= endCol && j < cols; j++)
            {
                Console.Write(this[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    public void Show()
    {
        ShowPartialy(0,rows-1,0,cols-1);
    }

}
class Program
{
    static void Main(string[] args)
    {

        Console.Write("Введите размеры матрицы, а также диапазон значений через пробел: ");
        var inputMatrix = Console.ReadLine().Split(' ');
        var minVal = Convert.ToInt32(inputMatrix[2]);
        var maxVal = Convert.ToInt32(inputMatrix[3]);
        MyMatrix matrix = new MyMatrix(
            Convert.ToInt32(inputMatrix[0]),
            Convert.ToInt32(inputMatrix[1]),
            Convert.ToInt32(inputMatrix[2]),
            Convert.ToInt32(inputMatrix[3])
            );
        Console.WriteLine("Матрица:");
        matrix.Show();
        Console.Write("Введите новые размеры матрицы через пробел: ");
        inputMatrix = Console.ReadLine().Split(' ');
        MyMatrix newMatrix = matrix.ChangeSize(
            Convert.ToInt32(inputMatrix[0]),
            Convert.ToInt32(inputMatrix[1]),
            minVal, maxVal);
        Console.WriteLine("Новая матрица:");
        newMatrix.Show();
        Console.WriteLine("Вывести часть матрицы 3х3:");
        matrix.ShowPartialy(0, 2, 0, 2);
        Console.WriteLine("Новая заполненная матрица:");
        matrix.Fill();
        matrix.Show();
    }
}
