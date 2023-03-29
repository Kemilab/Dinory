
namespace Dinory
{
    public  class Music : BindableObject
    {
        public static readonly BindableProperty IsMusicPlayingProperty =
        BindableProperty.Create(nameof(IsMusicPlaying), typeof(bool), typeof(Music), false);

        public bool IsMusicPlaying
        {
            get => (bool)GetValue(IsMusicPlayingProperty);
            set => SetValue(IsMusicPlayingProperty, value);
        }
        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                // Start playing music
                IsMusicPlaying = true;
            }
            else
            {
                // Pause music
                IsMusicPlaying = false;
            }
        }
    }
}
