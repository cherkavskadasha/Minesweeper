using Minesweeper.Models;
using Minesweeper.Models.ViewModels;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int ROWS = 15;
        int COLUMNS = 15;
        public ObservableCollection<ButtonViewModel> cells { get; set; } = new ObservableCollection<ButtonViewModel>();
        
        public GameManager GameManager { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            GameManager = new GameManager();
            InitializeGame();
        }

        public void InitializeGame()
        {
            GameManager.Initialize(ROWS, COLUMNS, "intermediate");
            cells.Clear();

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    cells.Add(new ButtonViewModel(i, j));
                }
            }
        }
    }
}