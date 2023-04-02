using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
namespace Dinory
{
    public partial class EasyDifficulty : ContentPage, INotifyPropertyChanged
    {
        private const int Rows = 2;
        private const int Columns = 5;
        private CancellationTokenSource _timerCancellationTokenSource;


        private List<string> Images;
        private Button _firstButtonClicked;
        private Button _secondButtonClicked;

        [Obsolete]
        public EasyDifficulty()
        {
            InitializeComponent();
            InitializeImages();
            StartTimer();
            BindingContext = this;
            LoadGameBoard();
        }
        private async void OnClickSettings(object sender, EventArgs e)
        {
            await this.ShowPopupAsync(new SettingsPage());
        }

        private void InitializeImages()
        {
            Images = new List<string>
            {
                "dino1.png",
                "dino1.png",
                "dino2.png",
                "dino2.png",
                "dino3.png",
                "dino3.png",
                "dino4.png",
                "dino4.png",
                "dino5.png",
                "dino5.png"
            };

            var random = new Random();
            Images = Images.OrderBy(_ => random.Next()).ToList();
        }

        [Obsolete]
        private void LoadGameBoard()
        {
            for (int i = 0; i < Rows; i++)
            {
                GameBoardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            }

            for (int i = 0; i < Columns; i++)
            {
                GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            int cardIndex = 0;

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    var image = new Image
                    {
                        Source = ImageSource.FromFile(Images[cardIndex]),
                        IsVisible = false,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };

                    var button = new Button
                    {
                        BackgroundColor = Colors.Gray,
                        CommandParameter = $"{row},{column}",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };
                    button.Clicked += Button_Clicked;

                    var cardGrid = new Grid();
                    cardGrid.Children.Add(image);
                    cardGrid.Children.Add(button);

                    Grid.SetRow(cardGrid, row);
                    Grid.SetColumn(cardGrid, column);
                    GameBoardGrid.Children.Add(cardGrid);

                    cardIndex++;
                }
            }
        }
        private double _countdownValue = 40;
        public double CountdownValue
        {
            get => _countdownValue;
            set
            {
                if (_countdownValue != value)
                {
                    _countdownValue = value;
                    OnPropertyChanged();
                }
            }
        }
        #pragma warning disable
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        #pragma warning restore
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void StartTimer()
        {
            _timerCancellationTokenSource = new CancellationTokenSource();

            DateTime endTime = DateTime.Now.AddSeconds(40);

            while (true)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), _timerCancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    // Task was canceled, do nothing.
                    return;
                }

                TimeSpan remaining = endTime - DateTime.Now;

                CountdownValue = remaining.TotalSeconds;

                if (remaining <= TimeSpan.Zero)
                {
                    await DisplayAlert("Time's up!", "The game has ended.", "OK");
                    await Navigation.PopAsync();
                    break;
                }
            }
        }



        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button.IsMatched())
            {
                return;
            }
            var position = button.CommandParameter.ToString().Split(',');
            var row = int.Parse(position[0]);
            var column = int.Parse(position[1]);
            var imageIndex = row * Columns + column;
            var imageSource = ImageSource.FromFile(Images[imageIndex]);

            // Add rotation animation
            var cardGrid = button.Parent as Grid;
            await cardGrid.RotateYTo(90, 100);
            ((cardGrid.Children[0] as Image).IsVisible) = true;
            button.BackgroundColor = Colors.Transparent;
            await cardGrid.RotateYTo(0, 100);

            if (_firstButtonClicked == null)
            {
                _firstButtonClicked = button;
            }
            else
            {
                _secondButtonClicked = button;
                await Task.Delay(100);

                var firstImage = ((button.Parent as Grid).Children[0] as Image).Source as FileImageSource;
                var secondImage = ((_firstButtonClicked.Parent as Grid).Children[0] as Image).Source as FileImageSource;

                if (firstImage != null && secondImage != null && firstImage.File == secondImage.File)
                {
                    _firstButtonClicked.SetMatched(true);
                    _secondButtonClicked.SetMatched(true);

                    if (CheckIfAllPairsFound())
                    {
                        await DisplayAlert("Congratulations!", "You have matched all the pairs!", "OK");
                        await Navigation.PopAsync();
                    }
                }
                else
                {
                    await Task.Delay(10);
                    await cardGrid.RotateYTo(90, 100);
                    ((cardGrid.Children[0] as Image).IsVisible) = false;
                    button.BackgroundColor = Colors.Gray;
                    await cardGrid.RotateYTo(0, 100);

                    var firstCardGrid = _firstButtonClicked.Parent as Grid;
                    await firstCardGrid.RotateYTo(90, 100);
                    ((firstCardGrid.Children[0] as Image).IsVisible) = false;
                    _firstButtonClicked.BackgroundColor = Colors.Gray;
                    await firstCardGrid.RotateYTo(0, 100);
                }

                _firstButtonClicked = null;
                _secondButtonClicked = null;
            }
        }






        private bool CheckIfAllPairsFound()
        {
            foreach (var child in GameBoardGrid.Children)
            {
                if (child is Grid cardGrid)
                {
                    var button = cardGrid.Children.OfType<Button>().FirstOrDefault();
                    if (button != null && !button.IsMatched())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            StopTimer();
        }

        private void StopTimer()
        {
            _timerCancellationTokenSource?.Cancel();
            _timerCancellationTokenSource?.Dispose();
            _timerCancellationTokenSource = null;
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

        public static bool IsMatched(this Button button)
        {
            return (bool)button.GetValue(MatchedProperty);
        }

        public static void SetMatched(this Button button, bool value)
        {
            button.SetValue(MatchedProperty, value);
        }

        public static readonly BindableProperty MatchedProperty =
            BindableProperty.CreateAttached("Matched", typeof(bool), typeof(ButtonExtensions), false);
    }

    public class ImageButton : Button
    {
        public ImageSource CardImage { get; set; }
        public bool IsImageVisible { get; set; }
    }

}


