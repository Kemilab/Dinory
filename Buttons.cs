namespace Dinory
{
    public class Buttons
    {
        public static async Task OnClickButtonSmall(Button Button, int scaleDuration, int delayDuration)
        {
            await Button.ScaleTo(0.9, (uint)scaleDuration);
            await Task.Delay(delayDuration);
            await Button.ScaleTo(1, (uint)scaleDuration);
            await Task.Delay(delayDuration);
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
