using System;

namespace StateBasedNavigation.Models
{
    public interface ITimer
    {
        event EventHandler Tick;

        void Start();

        void Stop();
    }
}
