using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

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

        public NormalDifficulty()
        {
            InitializeComponent();
            InitializeGame();
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
                        BackgroundColor = Colors.Gray,
                        CornerRadius = 5,
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
                }
                else
                {
                    // Not a match, flip the cards back after a delay
                    await Task.Delay(1000);
                    firstClickedButton.ImageSource = null;
                    secondClickedButton.ImageSource = null;
                }

                firstClickedButton = null;
                secondClickedButton = null;
            }
        }
    }
}



