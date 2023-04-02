using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.Maui.Controls.Button;

namespace Dinory
{
    public partial class HardDifficulty : ContentPage
    {
        private const int Rows = 3;
        private const int Columns = 8;
        private List<string> cardImages;
        private Dictionary<ImageButton, string> cardButtons;
        private ImageButton firstClickedButton;
        private ImageButton secondClickedButton;
        private int matchedPairs;
        private CancellationTokenSource countdownCancellationTokenSource;
        private int numberOfTries;
        public HardDifficulty()
        {
            InitializeComponent();
            InitializeGame();
            _ = StartCountdown(60);
        }
        private async void OnClickSettings(object sender, EventArgs e)
        {
            await this.ShowPopupAsync(new SettingsPage());
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
                "dino9.png", "dino9.png",
                "dino9.png", "dino9.png",
                "dino9.png", "dino9.png",
                "dino9.png", "dino9.png"
            };

            cardImages = cardImages.OrderBy(x => Guid.NewGuid()).ToList();
            cardButtons = new Dictionary<ImageButton, string>();

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    var imageSource = new Image
                    {
                        Source = "red.png",
                        Aspect = Aspect.AspectFill
                    };
                    var imageButton = new ImageButton
                    {
                        CornerRadius = 5,
                        ImageSource = imageSource.Source, // Assign the ImageSource
                        BackgroundColor = new Color(0.5f, 0.5f, 0.5f, 0.0f),
                        ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Top, 0),
                        Margin = new Thickness(2) // Add some margin around the cards
                    };

                    imageButton.Clicked += (sender, e) => CardClicked(imageButton);

                    var imageFile = cardImages[row * Columns + col];
                    cardButtons.Add(imageButton, imageFile);
                    CardsGrid.Children.Add(imageButton);
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
                    // Matched pair found
                    matchedPairs++;
                    numberOfTries++;

                    // If all pairs are matched, end the game
                    if (matchedPairs == Rows * Columns / 2)
                    {
                        countdownCancellationTokenSource.Cancel();
                        await DisplayAlert("Congratulations!", "You won the game!", "OK");
                        await Navigation.PopAsync();
                    }

                    firstClickedButton = null;
                    secondClickedButton = null;
                }
                else
                {
                    // Not a match, flip the cards back after a short delay
                    await Task.Delay(500);
                    firstClickedButton.ImageSource = "red.png";
                    secondClickedButton.ImageSource = "red.png";

                    firstClickedButton = null;
                    secondClickedButton = null;
                }
            }
        }

        private async Task StartCountdown(int seconds)
        {
            countdownCancellationTokenSource = new CancellationTokenSource();
            var token = countdownCancellationTokenSource.Token;

            for (int i = seconds; i >= 0; i--)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                TimerSlider.Value = i;
                await Task.Delay(1000);
            }

            if (!token.IsCancellationRequested)
            {
                await DisplayAlert("Time's up!", $"You ran out of time! You made {numberOfTries} tries.", "Try Again");
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
                _ = StartCountdown(60); // Restart the countdown when the user re-enters the page
            }
        }
    }
}