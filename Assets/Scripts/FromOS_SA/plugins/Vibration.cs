
// Source: https://gist.github.com/aVolpe/707c8cf46b1bb8dfb363

using UnityEngine;
using System.Threading;

public class Vibration
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="interval">Number of intervals</param>
    /// <param name="milliseconds">Milliseconds to wait between two vibrations</param>
    public void VibrateInterval(int interval, long milliseconds) {
        for(int i = 1; i <= interval; i++) {
            Handheld.Vibrate();
            Thread.Sleep((int)(1000 + milliseconds));
        }
    }
}
