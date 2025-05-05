using Minesweeper.Models.DbModels;
using Minesweeper.Models.ViewModels.Commands;
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
        public MenuViewModel(IGameRepository repository, MainViewModel mainVM)
        {
            InitializeProp(true);
            _repository = repository;
            _mainVM = mainVM;

            InitializeCommands();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IGameRepository _repository;

        private readonly MainViewModel _mainVM;

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

        private string _rows;

        public string Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                OnPropertyChanged();
            }
        }

        private string _columns;

        public string Columns
        {
            get => _columns;
            set
            {
                _columns = value;
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

        private bool _isChoosingDifficulty;

        public bool IsChoosingDifficulty
        {
            get => _isChoosingDifficulty;
            set
            {
                _isChoosingDifficulty = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand RegisterCommand { get; set; }

        public RelayCommand LoginCommand { get; set; }

        public RelayCommand ChangeLoginAndRegisterCommand { get; set; }

        public RelayCommand ChooseDifficultyCommand {  get; set; }

        public RelayCommand StartCommand { get; set; }

        public RelayCommand HistoryCommand { get; set; }

        public RelayCommand ExitCommand { get; set; }

        public RelayCommand ExitAccountCommand { get; set; }

        public void InitializeProp(bool toMainMenu)
        {
            Login = "";
            Password = "";
            Error = "";
            IsLogin = true;
            IsMenu = true;
            IsHistory = false;
            GameResults = new();
            if (toMainMenu)
            {
                UserId = 0;
                IsLoginScreen = true;
            }
            else
            {
                IsLoginScreen = false;
            }
        }

        public void InvokeExitRequest()
        {
            ExitRequested?.Invoke();
        }

        private void InitializeCommands()
        {
            RegisterCommand = GameCommandService.CreateRegisterCommand(this, _repository);

            LoginCommand = GameCommandService.CreateLoginCommand(this, _repository);

            ChangeLoginAndRegisterCommand = GameCommandService.CreateChangeLoginAndRegisterCommand(this);

            ChooseDifficultyCommand = GameCommandService.CreateChooseDifficultyCommand(this);

            StartCommand = GameCommandService.CreateStartCommand(this, _mainVM);

            HistoryCommand = GameCommandService.CreateHistoryCommand(this, _repository);

            ExitCommand = GameCommandService.CreateExitCommand(this);

            ExitAccountCommand = GameCommandService.CreateExitAccountCommand(this);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
