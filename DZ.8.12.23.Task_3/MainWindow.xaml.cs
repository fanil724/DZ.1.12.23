using System.Windows;

namespace DZ._8._12._23.Task_3
{

    public partial class MainWindow : Window
    {
        private const int Max_Copies = 3;
        private static Semaphore? semaphor = new Semaphore(Max_Copies, Max_Copies, "stmaphor");

        public MainWindow()
        {
            if (semaphor != null && semaphor.WaitOne(0))
            {
                InitializeComponent();
            }
            else
            {
                MessageBox.Show($"Нельзя запускать больше трех копии!!!", "Info", MessageBoxButton.OK);
                Close();
            }

        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();

            main.Show();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            semaphor.Release();
        }
    }
}