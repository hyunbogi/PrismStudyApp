using StateBasedNavigation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace StateBasedNavigation.Models
{
    public class ChatService : IChatService
    {
        private static readonly string Avatar1Uri = @"/StateBasedNavigation.Desktop;component/Avatars/MC900432625.PNG";
        private static readonly string Avatar2Uri = @"/StateBasedNavigation.Desktop;component/Avatars/MC900433938.PNG";
        private static readonly string Avatar3Uri = @"/StateBasedNavigation.Desktop;component/Avatars/MC900433946.PNG";
        private static readonly string Avatar4Uri = @"/StateBasedNavigation.Desktop;component/Avatars/MC900434899.PNG";

        private static readonly string[] _receivedMessages =
            new[]
            {
                "Hi, how are you?",
                "You will not believe this!",
                "So far so good",
                "Hope you're doing ok...",
                "Yes",
                "No",
                "Sometimes",
                "Is that all?",
                "Can't right now..."
            };

        private readonly Dispatcher _dispatcher;
        private readonly ITimer _timer;
        private readonly ReadOnlyCollection<Contact> _contacts;
        private readonly Random _random;

        private bool _connected;

        public ChatService()
            : this(new DispatcherTimerWrapper(new TimeSpan(0, 0, 1)))
        {
        }

        public ChatService(ITimer timer)
        {
            _dispatcher = Application.Current.Dispatcher;
            _random = new Random();
            _timer = timer;
            _timer.Tick += OnTimerTick;
            _timer.Start();

            _contacts = new ReadOnlyCollection<Contact>(
                new[]
                {
                    new Contact { Name = "Friend #1", AvatarUri = Avatar1Uri, PersonalMessage = "Enjoying my vacations!" },
                        new Contact { Name = "Friend #2", AvatarUri = Avatar3Uri },
                        new Contact { Name = "Friend #3", AvatarUri = Avatar2Uri },
                        new Contact { Name = "Friend #4", AvatarUri = Avatar1Uri, PersonalMessage = "Work work work work work" },
                        new Contact { Name = "Friend #5", AvatarUri = Avatar4Uri },
                        new Contact { Name = "Friend #6", AvatarUri = Avatar2Uri },
                        new Contact { Name = "Friend #7", AvatarUri = Avatar4Uri },
                        new Contact { Name = "Friend #8", AvatarUri = Avatar2Uri },
                        new Contact { Name = "Friend #9", AvatarUri = Avatar3Uri },
                        new Contact { Name = "Friend #10", AvatarUri = Avatar1Uri }
                });
        }

        public event EventHandler ConnectionStatusChanged;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public bool Connected
        {
            get => _connected;
            set
            {
                if (_connected != value)
                {
                    _connected = value;
                    ConnectionStatusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void GetContacts(Action<IOperationResult<IEnumerable<Contact>>> callback)
        {
            _dispatcher.BeginInvoke(
                new Action(() =>
                {
                    callback(new GetContactsOperationResult(_contacts));
                }));
        }

        public void SendMessage(Contact contact, string message, Action<IOperationResult> callback)
        {
            Timer timer = null;
            timer = new Timer(
                state =>
                {
                    _dispatcher.BeginInvoke(
                        new Action(() =>
                        {
                            timer.Dispose();
                            Debug.WriteLine($"Sent message to '{contact.Name}': {message}");
                            callback(new OperationResult());
                        }));
                },
                null,
                3000,
                Timeout.Infinite);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (Connected)
            {
                var coinToss = _random.Next(3);
                if (coinToss == 0)
                {
                    ReceiveMessage(
                        GetRandomMessage(_random.Next(_receivedMessages.Length)),
                        GetRandomContact(_random.Next(_contacts.Count)));
                }
                else
                {
                    coinToss = _random.Next(150);
                    if (coinToss == 0)
                    {
                        Connected = false;
                    }
                }
            }
        }

        private void ReceiveMessage(string message, Contact contact)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(contact, message));
        }

        private string GetRandomMessage(int messageIndex)
        {
            return _receivedMessages[messageIndex];
        }

        private Contact GetRandomContact(int contactIndex)
        {
            return _contacts[contactIndex];
        }

        private class GetContactsOperationResult : OperationResult<IEnumerable<Contact>>
        {
            public GetContactsOperationResult(IEnumerable<Contact> contacts)
            {
                Result = contacts;
            }
        }

        private class DispatcherTimerWrapper : ITimer
        {
            private readonly DispatcherTimer _timer;

            public DispatcherTimerWrapper(TimeSpan interval)
            {
                _timer = new DispatcherTimer { Interval = interval };
            }

            public event EventHandler Tick
            {
                add { _timer.Tick += value; }
                remove { _timer.Tick -= value; }
            }

            public void Start()
            {
                _timer.Start();
            }

            public void Stop()
            {
                _timer.Stop();
            }
        }
    }
}
