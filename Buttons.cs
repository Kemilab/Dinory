using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static async Task OnClickButtonBig(ImageButton ImageButton, int scaleDuration, int delayDuration)
        {
            await ImageButton.ScaleTo(1.3, (uint)scaleDuration);
            await Task.Delay(delayDuration);
            await ImageButton.ScaleTo(1, (uint)scaleDuration);
            await Task.Delay(delayDuration);
        }
    }
}
