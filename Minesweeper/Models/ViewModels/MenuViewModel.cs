using Minesweeper.Models.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public MenuViewModel(IGameRepository repository)
        {
            InitializeProp();
            _repository = repository;

            RegisterCommand = new RelayCommand((o) =>
            {
                if (Login != "" && Password != "")
                {
                    int result = _repository.RegisterUser(Login, Password);

                    if (result > 0)
                    {
                        UserId = result;
                        IsLoginScreen = false;
                    }
                    else
                    {
                        Error = "Користувач з таким логіном вже існує!";
                    }
                }
                else
                {
                    Error = "Заповніть всі поля!";
                }
            });

            LoginCommand = new RelayCommand((o) =>
            {
                if (Login != "" && Password != "")
                {
                    int result = _repository.LoginUser(Login, Password);

                    if (result > 0)
                    {
                        UserId = result;
                        IsLoginScreen = false;
                    }
                    else
                    {
                        Error = "Неправильний логін та/або пароль!";
                    }
                }
                else
                {
                    Error = "Заповніть всі поля!";
                }
            });

            ChangeLoginAndRegisterCommand = new RelayCommand((o) => {
                Login = "";
                Password = "";
                Error = "";
                IsLogin = !IsLogin;
            });

            HistoryCommand = new RelayCommand((o) =>
            {
                if ((string)o == "true")
                {
                    GameResults = _repository.GetResults(UserId);
                }
                IsHistory = !IsHistory;
            });

            ExitCommand = new RelayCommand((o) =>
            {
                ExitRequested?.Invoke();
            });

            ExitAccountCommand = new RelayCommand((o) =>
            {
                InitializeProp();
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IGameRepository _repository;

        public event Action ExitRequested;

        private string _error;

        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        private string _login;
        
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public int UserId { get; set; }

        private List<Result> _gameResults;

        public List<Result> GameResults
        {
            get => _gameResults;
            set
            {
                _gameResults = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoginScreen;

        public bool IsLoginScreen
        {
            get => _isLoginScreen;
            set
            {
                _isLoginScreen = value;
                OnPropertyChanged();
            }
        }

        private bool _isLogin;

        public bool IsLogin
        {
            get => _isLogin;
            set
            {
                _isLogin = value;
                OnPropertyChanged();
            }
        }

        private bool _isMenu;

        public bool IsMenu
        {
            get => _isMenu;
            set
            {
                _isMenu = value;
                OnPropertyChanged();
            }
        }

        private bool _isHistory;

        public bool IsHistory
        {
            get => _isHistory;
            set
            {
                _isHistory = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand RegisterCommand { get; set; }

        public RelayCommand LoginCommand { get; set; }

        public RelayCommand ChangeLoginAndRegisterCommand { get; set; }

        public RelayCommand HistoryCommand { get; set; }

        public RelayCommand ExitCommand { get; set; }

        public RelayCommand ExitAccountCommand { get; set; }

        private void InitializeProp()
        {
            UserId = 0;
            Login = "";
            Password = "";
            Error = "";
            IsLoginScreen = true;
            IsLogin = true;
            IsMenu = true;
            IsHistory = false;
            GameResults = new();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
