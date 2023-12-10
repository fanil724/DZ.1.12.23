using System.Windows;
namespace DZ._4._12._23
{
    public partial class MainWindow : Window
    {

        static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        static CancellationToken token = cancelTokenSource.Token;
        static CancellationTokenSource cancelTokenSource2 = new CancellationTokenSource();
        static CancellationToken token2 = cancelTokenSource2.Token;

        AutoResetEvent autoPN = new AutoResetEvent(true);
        bool PPN = false;

        AutoResetEvent autoF = new AutoResetEvent(true);
        bool PF = false;

        Thread tPN;
        Thread tF;
        Ranges rPN;

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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartF_Click(object sender, RoutedEventArgs e)
        {
            tF = new Thread(Fibonachi2);
            tF.Name = "Fibanachi";
            if (int.TryParse(EndF.Text, out int number) & number > 1)
            {
                tF.Start(number);
            }
            else
            {
                MessageBox.Show("Введите яисло больше 2", "info", MessageBoxButton.OK);
            }
        }

        private void StartPN_Click(object sender, RoutedEventArgs e)
        {
            tPN = new Thread(PrimaryN);
            tPN.Name = "PrimaryN";
            rPN = new Ranges(BeginPN.Text, EndPN.Text);
            tPN.Start(rPN);
        }

        private void StopPN_Click(object sender, RoutedEventArgs e)
        {
            if (token.IsCancellationRequested == false)
            {
                cancelTokenSource.Cancel();
            }
        }

        private void StopF_Click(object sender, RoutedEventArgs e)
        {
            if (token2.IsCancellationRequested == false)
            {
                cancelTokenSource2.Cancel();
            }
        }

        private void ResetPN_Click(object sender, RoutedEventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            PrimaryNumber.Items.Clear();
        }

        private void ResetF_Click(object sender, RoutedEventArgs e)
        {
            cancelTokenSource2 = new CancellationTokenSource();
            token2 = cancelTokenSource2.Token;
            Fibonachi.Items.Clear();
        }

        private void PausePN_Click(object sender, RoutedEventArgs e)
        {
            PPN = true;
        }

        private void PlayPN_Click(object sender, RoutedEventArgs e)
        {
            PPN = false;
            autoPN.Set();
        }

        private void PaueF_Click(object sender, RoutedEventArgs e)
        {
            PF = true;
        }

        private void PlayF_Click(object sender, RoutedEventArgs e)
        {
            PF = false;
            autoF.Set();
        }
        public void PrimaryN(object obj)
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
                        if (PPN) { autoPN.WaitOne(); }

                        if (token.IsCancellationRequested)
                        {
                            return;
                        }
                        if (i % j == 0 & i % 1 == 0)
                        {
                            b = false;
                        }
                    }
                    if (b)
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            PrimaryNumber.Items.Add(i);
                        }));
                        Thread.Sleep(150);
                    }


                }
            }

        }

        public void Fibonachi2(object obj)
        {
            if (obj is int end)
            {

                int j = 1;
                for (int i = 1; i <= end; i += j)
                {
                    if (PF) { autoF.WaitOne(); }
                    Dispatcher.Invoke((Action)(() =>
                    {
                        Fibonachi.Items.Add(i);
                    }));
                    Thread.Sleep(250);

                    if (token2.IsCancellationRequested)
                    {
                        return;
                    }

                    j = i - j;

                }
            }
        }
    }
}
