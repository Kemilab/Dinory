namespace Dinory
{
    public partial class EasyDifficulty : ContentPage
    {
        private const int Rows = 2;
        private const int Columns = 5;

        private List<string> Images;
        private Button _firstButtonClicked;
        private Button _secondButtonClicked;

        public EasyDifficulty()
        {
            InitializeComponent();
            InitializeImages();
            LoadGameBoard();
        }

        private void InitializeImages()
        {
            Images = new List<string>
            {
                "blue.png",
                "blue.png",
                "green.png",
                "green.png",
                "brown.png",
                "brown.png",
                "red.png",
                "red.png",
                "green_dino.png",
                "green_dino.png"
            };

            var random = new Random();
            Images = Images.OrderBy(_ => random.Next()).ToList();
        }

        private void LoadGameBoard()
        {
            for (int i = 0; i < Rows; i++)
            {
                GameBoardGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < Columns; i++)
            {
                GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    var button = new Button
                    {
                        BackgroundColor = Colors.Gray,
                        CommandParameter = $"{row},{column}"
                    };
                    button.Clicked += Button_Clicked;
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                    GameBoardGrid.Children.Add(button);
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var position = button.CommandParameter.ToString().Split(',');
            var row = int.Parse(position[0]);
            var column = int.Parse(position[1]);

            var imageIndex = row * Columns + column;
            var imageSource = ImageSource.FromFile(Images[imageIndex]);

            button.ImageSource = imageSource;
            button.BackgroundColor = Colors.Transparent;

            if (_firstButtonClicked == null)
            {
                _firstButtonClicked = button;
            }
            else
            {
                _secondButtonClicked = button;
                await Task.Delay(1000);

                if (Images[imageIndex] == Images[_firstButtonClicked.RowIndex() * Columns + _firstButtonClicked.ColumnIndex()])
                {
                    _firstButtonClicked.IsEnabled = false;
                    _secondButtonClicked.IsEnabled = false;
                }
                else
                {
                    _firstButtonClicked.ImageSource = null;
                    _firstButtonClicked.BackgroundColor = Colors.Gray;
                    _secondButtonClicked.ImageSource = null;
                    _secondButtonClicked.BackgroundColor = Colors.Gray;
                }

                _firstButtonClicked = null;
                _secondButtonClicked = null;
            }
        }
    }

    public static class ButtonExtensions
    {
        public static int RowIndex(this Button button)
        {
            return (int)button.GetValue(Grid.RowProperty);
        }

        public static int ColumnIndex(this Button button)
        {
            return (int)button.GetValue(Grid.ColumnProperty);
        }
    }
}


