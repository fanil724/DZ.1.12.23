void Factorial(object? obj)
{
    if (obj is int n)
    {
        int num = 1;
        for (int i = 2; i <= n; i++)
        {
            num *= i;
            Console.WriteLine($"Factorial: {num}");
        }
        
    }
}


void Fibonachi(object? obj)
{
    if (obj is int end)
    {
        int j = 1;
        for (int i = 1; i <= end; i += j)
        {
            Console.WriteLine($"Fibonachi {i}");
            j = i - j;
        }
    }
}

void StepenNumber(object? obj)
{
    if (obj is int n)
    {
        Console.WriteLine($"Stepen {n * n}" );
    }
}



Thread thread1 = new Thread(StepenNumber);
Thread thread2 = new Thread(Factorial);
Thread thread3 = new Thread(Fibonachi);


Console.WriteLine("Введите число для вывполнения операции:");
int number = Convert.ToInt32(Console.ReadLine());


thread1.Start(number);
thread2.Start(number);
thread3.Start(number);