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

namespace tet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages =
       {
            new BitmapImage(new Uri("Assets/TileEmpty.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/ITales.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/JTales.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/LTales.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/OTales.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/STales.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TTales.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/ZTales.png",UriKind.Relative)),
        };
        private readonly ImageSource[] blockImages =
        {
            new BitmapImage(new Uri("Assets/BlockEmpty.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/IBlock.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/JBlock.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/LBlock.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/OBlock2var",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/SBlock.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TBlock.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/ZBlock.png",UriKind.Relative)),
        };
        private readonly Image[,] imageControls;
        private GameState gameState = new GameState();
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.Gamegrid);
        }
        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellsize = 25;
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellsize,
                        Height = cellsize,
                    };
                    Canvas.SetTop(imageControl, (r - 2) * cellsize + 10);
                    Canvas.SetLeft(imageControl, c * cellsize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }
        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }
        private void DrawBlock(Block block)
        {
            foreach (PositionOfBlock p in block.TilePosition())
            {
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }
        private void DrawNextBlock(BlockQuene blockQuene) // отрисовка следующего блока
        {
            Block next = blockQuene.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }
        private void DrawHooldBlock(Block hooldblock)
        {
            if (hooldblock == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[hooldblock.Id];
            }
        }
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.Gamegrid);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.Blockquene);
            DrawHooldBlock(gameState.HooldBlock);
            ScoreText.Text = $"Score: {gameState.Score}";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft(); break;
                case Key.Right:
                    gameState.MoveBlockRight(); break;
                case Key.Down:
                    gameState.MoveBlockDown(); break;
                case Key.Up:
                    gameState.RotateBlockCW(); break;
                case Key.Space:
                    gameState.RotateBlockCCW(); break;
                case Key.C:
                    gameState.HoldBlock(); break;
                default: return;
            }
            Draw(gameState);
        }
        private async Task GameLoop()
        {
            Draw(gameState);
            while (!gameState.GameOver)
            {
                await Task.Delay(500);
                gameState.MoveBlockDown();
                Draw(gameState);
            }
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) // playagainclick
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }
    }
}