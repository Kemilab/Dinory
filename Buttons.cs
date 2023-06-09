﻿namespace Dinory
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
    }
}
