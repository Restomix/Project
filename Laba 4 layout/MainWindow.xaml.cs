using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba_4_layout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public int N = 0;
        public void MainWork()
        {
            for (int i = 0; i < N; i++)
            {


                Grid DynamicGrid = new Grid();
             //   DynamicGrid.Width = 400;
                DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
                DynamicGrid.VerticalAlignment = VerticalAlignment.Top;
                DynamicGrid.ShowGridLines = true;
                DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
                for (int x = 0; x < 8; x++)
                {
                    DynamicGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width=new GridLength(50)});
                    DynamicGrid.RowDefinitions.Add(new RowDefinition() { Height= new GridLength(50)});


                }
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (Ferzi.combinations[i][x] == y)
                        {
                            Image current = new Image();
                            current.Source = new BitmapImage(new Uri(@"C:\Users\Yura\Desktop\Files\Параленьні обчислення\Laba 4 layout\Laba 4 layout\Sources\queen.png"));
                            
                            Grid.SetRow(current, y);
                            Grid.SetColumn(current, x);
                            DynamicGrid.Children.Add(current);
                        }
                    }
                }
                stack.Children.Add(DynamicGrid);
                stack.Children.Add(new Separator() { Height = 20 });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            this.N=Convert.ToInt32(Size.Text);
            Ferzi.main(N);
            MainWork();
            
            
        }
    }
    static public class Ferzi
    {

        static int[] chessboard = { 0, 0, 0, 0, 0, 0, 0, 0 };
        static int index = 0;
        static int version = 0;
        public static List<List<int>> combinations = new List<List<int>>();
        public static void GetNew()
        {
            version--;
            for (int i = 0; i < version; i++)
            {
                GetNew();
            }
        }
        public static void main(int N)
        {
            do
            {
                if (checking())
                {
                    if (index == 7)
                    {
                        if (N == version)
                        {
                            return;
                        }
                        // System.Console.WriteLine(version++ + " [0]=" + chessboard[0] + " [1]=" + chessboard[1] + " [2]=" + chessboard[2] + " [3]=" + chessboard[3] + " [4]=" + chessboard[4] + " [5]=" + chessboard[5] + " [6]=" + chessboard[6] + " [7]=" + chessboard[7]);
                        combinations.Add(new List<int>{ chessboard[0],  chessboard[1], chessboard[2], chessboard[3], chessboard[4],
                        chessboard[5], chessboard[6],  chessboard[7]});
                        chessboard[index]++;
                    }
                    else
                    {
                        index++;
                    }
                }
                else
                {
                    chessboard[index]++;
                }
                if (version == -1)
                {
                    //новий потік для обчислення
                    Thread open = new Thread(GetNew);
                    open.Start();
                }
            } while (chessboard[0] < 8);


        }

        static bool checking()
        {
            int i;

            if (index == 0)
            {
                return true;
            }

            if (chessboard[index] > 7)
            {
                chessboard[index] = 0;
                index--;
                return false;
            }

            for (i = 0; i < index; i++)
            {
                if ((chessboard[index] == chessboard[i]) | ((Math.Abs(chessboard[index] - chessboard[i])) == (index - i)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
