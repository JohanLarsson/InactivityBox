namespace InactivityBox
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Annotations;

    public class Vm : INotifyPropertyChanged
    {
        private Timer _timer;
        private TimeSpan _idleTime;
        public Vm()
        {
            _timer = new Timer(Update, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public TimeSpan IdleTime
        {
            get { return _idleTime; }
            private set
            {
                if (value.Equals(_idleTime))
                {
                    return;
                }
                _idleTime = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void Update(object o)
        {
            uint lastInputTime = Inactivity.GetLastInputTime();
            IdleTime = TimeSpan.FromMilliseconds(lastInputTime);
        }
    }
}
