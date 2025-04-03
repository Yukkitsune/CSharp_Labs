class MyMatrix
{
    private Int32[,] matrix;
    private int rows;
    private int cols;
    public MyMatrix(int rows, int cols, int minVal, int maxVal)
    {
        this.rows = rows;
        this.cols = cols;
        matrix = new Int32[rows, cols];
        Random random = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {   
                if (minVal > maxVal) throw new IndexOutOfRangeException("Индекс вне диапазона");
                else matrix[i, j] = random.Next(minVal, maxVal);

            }
        }
    }
    public Int32 this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols) throw new IndexOutOfRangeException("Индекс вне диапазона");
            return matrix[row, col];
        }
        set
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols)
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            matrix[row, col] = value;
        }
    }
    public static MyMatrix operator +(MyMatrix m1, MyMatrix m2)
    {
        if (m1.rows != m2.rows || m1.cols != m2.cols)
            throw new Exception("Матрицы должны быть одинакового размера для сложения.");
        else {
            MyMatrix result = new MyMatrix(m1.rows, m1.cols, 0, 0);

            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m1.cols; j++)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return result;
        }
        
    }
    public static MyMatrix operator -(MyMatrix m1, MyMatrix m2)
    {
        if (m1.rows != m2.rows || m1.cols != m2.cols)
            throw new ArgumentException("Матрицы должны быть одинакового размера для разности.");
        MyMatrix result = new MyMatrix(m1.rows,m1.cols, 0, 0);
        for (int i = 0; i < m1.rows; i++)
        {
            for (int j = 0; j < m1.cols; j++)
            {
                result[i,j] = m1[i,j] - m2[i,j];
            }
        }
        return result;
    }
    public static MyMatrix operator *(MyMatrix m1, MyMatrix m2)
    {
        MyMatrix result = new MyMatrix(m1.rows, m2.cols, 0, 0);
        for (int i = 0;i < m1.rows; i++)
        {
            for (int j = 0; j < m2.cols; j++)
            {
                for (int k = 0; k < m2.rows; k++)
                {
                    result[i, j] += m1[i, k] * m2[k, j];
                }
            }
        }
        return result;
    }
    public static MyMatrix operator *(MyMatrix m1, int number)
    {
        MyMatrix result = new MyMatrix(m1.rows, m1.cols, 0, 0);
        for (int i = 0; i < m1.rows; i++)
        {
            for (int j = 0; j < m1.cols; j++)
            {
                result[i,j] = m1[i,j]*number;
            }
        }
        return result;
    }
    public static MyMatrix operator *(int number, MyMatrix m1)
    {
        MyMatrix result = new MyMatrix(m1.rows, m1.cols, 0, 0);
        for (int i = 0; i < m1.rows; i++)
        {
            for (int j = 0; j < m1.cols; j++)
            {
                result[i, j] = m1[i, j] * number; 
            }
        }
        return result;
    }
    public static MyMatrix operator /(MyMatrix m1, int number)
    {
        MyMatrix result = new MyMatrix(m1.rows, m1.cols, 0, 0);
        for (int i = 0; i < m1.rows; i++)
        {
            for (int j = 0; j < m1.cols; j++)
            {
                result[i, j] = m1[i,j]/number;
            }
        }
        return result;
    }
    public void print()
    {
        for (int i = 0; i < rows; i++)
        {
            for(int j=0; j< cols; j++)
            {
                Console.Write(this[i, j] +" ");
            }
            Console.WriteLine();
        }
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите размеры матрицы, а также диапазон значений через пробел: ");
        var inputMatrix = Console.ReadLine().Split(' ');
        MyMatrix matrix1 = new MyMatrix(
            Convert.ToInt32(inputMatrix[0]), 
            Convert.ToInt32(inputMatrix[1]),
            Convert.ToInt32(inputMatrix[2]),
            Convert.ToInt32(inputMatrix[3])
            );
        Console.Write("Введите размеры матрицы, а также диапазон значений через пробел: ");
        inputMatrix = Console.ReadLine().Split(' ');
        MyMatrix matrix2 = new MyMatrix(
             Convert.ToInt32(inputMatrix[0]),
             Convert.ToInt32(inputMatrix[1]),
             Convert.ToInt32(inputMatrix[2]),
             Convert.ToInt32(inputMatrix[3])
             );
        Console.WriteLine("Матрица 1:");
        matrix1.print();
        Console.WriteLine("Матрица 2:");
        matrix2.print();
        Console.Write("Введите число, на которое будете умножать матрицу: ");
        int multiply = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите число, на которое будете делить матрицу: ");
        int division = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Сумма матриц: ");
        (matrix1+matrix2).print();
        Console.WriteLine("Разность матриц: ");
        (matrix1 - matrix2).print();
        Console.WriteLine("Произведение  матриц: ");
        (matrix1 * matrix2).print();
        Console.WriteLine("Произведение матрицы на число: ");
        (matrix1 * multiply).print();
        Console.WriteLine("Деление матрицы на число: ");
        (matrix1 / division).print();

    }
}