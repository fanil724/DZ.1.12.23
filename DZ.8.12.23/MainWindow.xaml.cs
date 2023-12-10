using Library1;
using System.IO;
using System.Printing.IndexedProperties;
using System.Windows;
using static DZ._8._12._23.MainWindow;

namespace DZ._8._12._23
{
    public partial class MainWindow : Window
    {


        Mutex m1 = new Mutex(true);
        Mutex m2 = new Mutex();
        Mutex m3 = new Mutex();
        Thread t1;
        Thread t2;
        Thread t3;
        Thread t4;


        int[] arr = new int[10];
        public MainWindow()
        {
            InitializeComponent();
            t1 = new Thread(GenerateNumber);
            t2 = new Thread(SearchPrimaryNumber);
            t3 = new Thread(SearchSevenNumber);
            t4 = new Thread(OutputList);
        }

        private void datetime_Click(object sender, RoutedEventArgs e)
        {
            Task t = Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add(DateTime.Now); }));
            });

            Task task = new Task(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add(DateTime.Now); }));
            });
            task.Start();

            Task task1 = Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add(DateTime.Now); }));
            });
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            input.Items.Clear();
        }

        private async void primarynum_Click(object sender, RoutedEventArgs e)
        {
            Ranges ranges = new Ranges(begin.Text, end.Text);
            Task task = Task.Run(() => PrimaryN(ranges));

            await task;
            input.Items.Add($"Количество элементов: {input.Items.Count}");
        }

        private void arrayss_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            for (int item = 0; item < arr.Length; item++)
            {
                arr[item] = random.Next(0, 100);
            }
            input.Items.Add(String.Join<int>(" ", arr));

            Task[] tasks = new Task[4];
            tasks[0] = (Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    input.Items.Add($"Минимальное значаение: {arr.Min()}");
                }));
            }));
            tasks[1] = (Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    input.Items.Add($"Максимальное значаение: {arr.Max()}");
                }));
            }));
            tasks[2] = (Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    input.Items.Add($"Суммарное значаение: {arr.Sum()}");
                }));
            }));
            tasks[3] = (Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    input.Items.Add($"Среднее значаение: {arr.Average()}");
                }));
            }));
        }

        private void PrimaryN(object obj)
        {
            int begin;
            int end;
            if (obj is Ranges ranges)
            {
                begin = ranges.Begin;
                end = ranges.END;

                for (int i = begin; i != end; i++)
                {
                    bool b = true;
                    for (int j = 2; j < i; j++)
                    {
                        if (i % j == 0 & i % 1 == 0)
                        {
                            b = false;
                        }
                    }
                    if (b)
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            input.Items.Add(i);
                        }));
                        Thread.Sleep(150);
                    }
                }
            }
        }
        public class Ranges
        {
            public int Begin { get; set; }
            public int END { get; set; }

            public Ranges(string begin, string end)
            {
                if (int.TryParse(begin, out int number) && number > 2)
                {
                    Begin = number;
                }
                else
                {
                    Begin = 2;
                }

                if (int.TryParse(end, out int num) && num > 2)
                {
                    END = num;
                }
                else
                {
                    END = -1;
                }
            }
        }

        private async void arrss_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            arr = new int[] { 32, 12, 43, 54, 765, 867, 32, 12, 43, 867, 85, 74, 756, 54 };
            input.Items.Add(String.Join<int>(" ", arr));

            Task t1 = Task.Factory.StartNew(() => arr = arr.Distinct().ToArray());
            Task t2 = t1.ContinueWith((t1) =>
            {
                Dispatcher.Invoke((Action)(() => input.Items.Add(String.Join<int>(" ", arr))));
                Dispatcher.Invoke((Action)(() => input.Items.Add($"Выполнение потока {t1.Id} завершено")));
                arr = arr.OrderBy(x => x).ToArray();
                Thread.Sleep(5000);
            });
            Task t3 = t2.ContinueWith((t2) =>
            {
                Dispatcher.Invoke((Action)(() => input.Items.Add(String.Join<int>(" ", arr))));
                Dispatcher.Invoke((Action)(() => input.Items.Add($"Выполнение потока {t2.Id} завершено")));
                index = BinSaerch(arr, 43);
                Thread.Sleep(5000);
            });
            await t3;
            input.Items.Add($"Индекс положения числа 43:  {index}   {Array.IndexOf(arr, 43)}");
        }

        private int BinSaerch(int[] ar, int number)
        {
            int first = 0; int last = ar.Length - 1;
            int middle = 0;
            while (first <= last)
            {
                middle = (first + last) / 2;
                if (ar[middle] == number)
                {
                    return middle;
                }
                else if (number < ar[middle])
                {
                    last = middle - 1;
                }
                else
                {
                    first = middle + 1;
                }
            }
            return -1;
        }

        private void DLL_Click(object sender, RoutedEventArgs e)
        {
            List<Task> t = new List<Task>();
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add(Library1.Class1.Mylibrary()); }));
            }));
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"2023 год высокосный? - {Library1.LeapYear.isLeap(2023).ToString()}"); }));
            }));
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Сумма трех чисел 123, 54, 954:  {Library1.MathOperat.Summ(123, 54, 954)}"); }));
            }));

            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Максимально число из этих чисел 123, 54, 954:  {Library1.MathOperat.Maxx(123, 54, 954)}"); }));
            }));
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Минимальное число из этих чисел 123, 54, 954:  {Library1.MathOperat.Minn(123, 54, 954)}"); }));
            }));
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Факториал числа 5:  {Library1.MathOperat.Factorial(5)}"); }));
            }));
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Четность числа 5:  {Library1.MathOperat.even(5).ToString()}"); }));
            }));
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Не четность числа 5:  {Library1.MathOperat.odd(5)}"); }));
            }));

            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Сложение дробей (2/7+4/9):  {new Fraction(2, 7) + new Fraction(4, 9)}"); }));
            }));
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Вычитание дробей (2/7-4/9):  {new Fraction(2, 7) - new Fraction(4, 9)}"); }));
            }));
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Умножение дробей (2/7*4/9):  {new Fraction(2, 7) * new Fraction(4, 9)}"); }));
            }));
            t.Add(Task.Run(() =>
            {
                Dispatcher.Invoke((Action)(() =>
                { input.Items.Add($"Деление дробей (2/7%4/9):  {new Fraction(2, 7) / new Fraction(4, 9)}"); }));
            }));
        }

        private void gennumber_Click(object sender, RoutedEventArgs e)
        {
            t2.IsBackground = true;
            t3.IsBackground = true;
            t1.IsBackground = true;
            t4.IsBackground = true;
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
        }


        private void GenerateNumber()
        {
            m1.WaitOne();
            string path = "test.txt";
            Random r = new Random();
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for (int i = 0; i < 50; i++)
                {
                    writer.Write($"{r.Next(50, 150)}{(i == 49 ? "" : " ")}");
                }
            }
            m1.ReleaseMutex();
        }

        private void SearchPrimaryNumber()
        {
            m2.WaitOne();
            string text;
            using (StreamReader reader = new StreamReader("test.txt"))
            {
                text = reader.ReadToEnd();
            }
            var te = text.Split(" ").Select(x => x.Trim()).ToArray();
            using (StreamWriter writer = new StreamWriter("test2.txt", false))
            {
                for (int item = 0; item < te.Length; item++)
                {
                    if (Int32.TryParse(te[item], out int number))
                    {
                        if (Library1.MathOperat.prime(number))
                        {
                            writer.Write($"{number}{(item == te.Length - 1 ? "" : " ")}");
                        }
                    }
                }
            }
            m2.ReleaseMutex();

        }
        private void SearchSevenNumber()
        {
            m3.WaitOne();
            string text;
            using (StreamReader reader = new StreamReader("test2.txt"))
            {
                text = reader.ReadToEnd();
            }

            var te = text.Split(" ").Select(x => x.Trim()).ToArray();

            using (StreamWriter writer = new StreamWriter("test3.txt", false))
            {
                for (int i = 0; i < te.Length - 1; i++)
                {
                    char last_char = te[i].Last();
                    if (last_char == '7')
                    {
                        writer.Write($"{te[i]}{(i == te.Length - 1 ? "" : " ")}");
                    }
                }
            }
            m3.ReleaseMutex();
        }

        private void OutputList()
        {

            Thread.Sleep(3000);
            string text;
            using (StreamReader reader = new StreamReader("test.txt"))
            {
                text = reader.ReadToEnd();
            }
            Dispatcher.Invoke((Action)(() => input.Items.Add($"Размер файла test.txt: {sizeConver("test.txt")}")));
            Dispatcher.Invoke((Action)(() => input.Items.Add(text)));
            int count = text.Split(" ").Select(x => x.Trim()).Count();
            Dispatcher.Invoke((Action)(() => input.Items.Add($"Количество сгенерированных числе: {count}")));
            Thread.Sleep(3000);
            using (StreamReader reader = new StreamReader("test2.txt"))
            {
                text = reader.ReadToEnd();
            }
            Dispatcher.Invoke((Action)(() => input.Items.Add($"Размер файла test2.txt: {sizeConver("test2.txt")}")));
            Dispatcher.Invoke((Action)(() => input.Items.Add(text)));
            count = text.Split(" ").Select(x => x.Trim()).Count();
            Dispatcher.Invoke((Action)(() => input.Items.Add($"Количество простых числе: {count}")));
            Thread.Sleep(3000);
            using (StreamReader reader = new StreamReader("test3.txt"))
            {
                text = reader.ReadToEnd();
            }
            Dispatcher.Invoke((Action)(() => input.Items.Add($"Размер файла test3.txt: {sizeConver("test3.txt")}")));
            Dispatcher.Invoke((Action)(() => input.Items.Add(text)));
            count = text.Split(" ").Select(x => x.Trim()).Count();
            Dispatcher.Invoke((Action)(() => input.Items.Add($"Количество чиле заканчивающихся на 7: {count}")));

        }

        private string sizeConver(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                FileInfo info = new FileInfo(filePath);
                long size = info.Length;
                string[] sizeletters = new string[] { "bytes", "KB", "MB", "GB", "TB" };
                for (int i = 0; i < 5; i++)
                {
                    if (size < 1024)
                    {
                        string fileSize = size.ToString() + sizeletters[i];
                        return fileSize;
                    }
                    size /= 1024;
                }
            }
            return "";
        }

        private void Kazino_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            int countPlayer = ran.Next(20, 100);
            List<Player> list = new List<Player>();
            for (int i = 0; i < countPlayer; i++)
            {
                list.Add(new Player(i));
            }

            Thread.Sleep(200000);

            foreach (Player player in list)
            {
                Thread.Sleep(500);
                Dispatcher.Invoke((Action)(() => input.Items.Add($"Игрок {player.thread.Name} : начальная сумма: {player.beginsum} , конечная сумма {player.endinsum}")));
               
            }
        }

        public class Player
        {
            static Semaphore semaphore = new Semaphore(5, 5);
            public Thread thread { get; set; }
            public int beginsum { get; set; } = 150;
            public int endinsum { get; set; } = 150;
            int count = 3;

            public Player(int i)
            {
                thread = new Thread(Play);
                thread.Name = $"Игрок {i}";
                thread.IsBackground = true;
                thread.Start();
            }


            private void Play()
            {
                Random rnd = new Random();
                while (count > 0)
                {
                    int CasinoNumber = rnd.Next(1, 36);
                    semaphore.WaitOne();
                    int PlayerNaumber = rnd.Next(1, 36);
                    int Stavka = rnd.Next(1, endinsum);
                   
                    if (CasinoNumber == PlayerNaumber)
                    {
                        endinsum += Stavka;
                    }
                    else
                    {
                        endinsum -= Stavka;
                    }

                    if (endinsum <= 0)
                    {
                        return;
                    }
                  
                    semaphore.Release();
                    count--;
                }


            }
        }


    }
}