using static Microsoft.Maui.Controls.Button;
namespace Dinory
{
    public partial class NormalDifficulty : ContentPage
    {
        private const int Rows = 2;
        private const int Columns = 8;
        private List<string> cardImages;
        private Dictionary<ImageButton, string> cardButtons;
        private ImageButton firstClickedButton;
        private ImageButton secondClickedButton;
        private int matchedPairs;
        private CancellationTokenSource countdownCancellationTokenSource;
        public NormalDifficulty()
        {
            InitializeComponent();
            InitializeGame();
            _ = StartCountdown(45);
        }

        private void InitializeGame()
        {
            cardImages = new List<string>
            {
                "dino1.png", "dino1.png",
                "dino2.png", "dino2.png",
                "dino3.png", "dino3.png",
                "dino4.png", "dino4.png",
                "dino5.png", "dino5.png",
                "dino6.png", "dino6.png",
                "dino7.png", "dino7.png",
                "dino8.png", "dino8.png",
            };

            cardImages = cardImages.OrderBy(x => Guid.NewGuid()).ToList();
            cardButtons = new Dictionary<ImageButton, string>();

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    var imageButton = new ImageButton
                    {
                        CornerRadius = 5,
                        ImageSource = "green.png",
                        BackgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.0f),
                        ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Top, 0), 
                    };

                    imageButton.Clicked += (sender, e) => CardClicked(imageButton);

                    var imageFile = cardImages[row * Columns + col];
                    cardButtons.Add(imageButton, imageFile);
                    GameGrid.Children.Add(imageButton);
                    Grid.SetRow(imageButton, row);
                    Grid.SetColumn(imageButton, col);
                }
            }
        }

        private async void CardClicked(ImageButton imageButton)
        {
            if (firstClickedButton == null)
            {
                firstClickedButton = imageButton;
                imageButton.ImageSource = cardButtons[imageButton];
            }
            else if (firstClickedButton != imageButton && secondClickedButton == null)
            {
                secondClickedButton = imageButton;
                imageButton.ImageSource = cardButtons[imageButton];

                if (cardButtons[firstClickedButton] == cardButtons[secondClickedButton])
                {
                    // A match is found
                    firstClickedButton.IsEnabled = false;
                    secondClickedButton.IsEnabled = false;
                    cardButtons.Remove(firstClickedButton);
                    cardButtons.Remove(secondClickedButton);
                    matchedPairs++;
                    if (matchedPairs == Rows * Columns / 2)
                    {
                        // Display an alert and navigate back to DifficultyPage when the user clicks "OK"
                        await DisplayAlert("Congratulations!", "You found all pairs!", "OK");
                        await Navigation.PopAsync();
                    }
                }
                else
                {

                    await Task.Delay(500);
                    firstClickedButton.ImageSource = "green.png";
                    secondClickedButton.ImageSource = "green.png";

                }

                firstClickedButton = null;
                secondClickedButton = null;
            }
        }
        private async Task StartCountdown(int seconds)
        {
            countdownCancellationTokenSource = new CancellationTokenSource();
            CountdownSlider.Value = 0;

            for (int i = 0; i <= seconds; i++)
            {
                if (countdownCancellationTokenSource.Token.IsCancellationRequested)
                {
                    break;
                }

                CountdownSlider.Value = i;
                await Task.Delay(1000);
            }

            if (!countdownCancellationTokenSource.Token.IsCancellationRequested)
            {
                await DisplayAlert("Oh!", "You'r slow!", "Take me home!");
                await Navigation.PopAsync();
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            countdownCancellationTokenSource.Cancel(); // Stop the countdown when the user leaves the page
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (countdownCancellationTokenSource.IsCancellationRequested)
            {
                _ = StartCountdown(45); // Restart the countdown when the user re-enters the page
            }
        }

    }
}



