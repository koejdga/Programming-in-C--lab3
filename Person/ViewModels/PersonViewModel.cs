using System.Windows.Forms;
using Person.Models;
using Person.Stores;
using Person.Tools;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Person.ViewModels
{
    public class PersonViewModel : ViewModelBase
    {
        private readonly PersonModel _person;
        private readonly NavigationStore _navigationStore;

        public string Name => _person.Name;
        public string Surname => _person.Surname;
        public string Email => _person.Email.ToString();
        public string BirthDate => _person.BirthDate.ToString("d");

        public string IsAdult => _person.IsAdult ? "yes" : "no";
        public string SunSign => _person.SunSign.ToString();
        public string ChineseSign => _person.ChineseSign.ToString();
        public string IsBirthday => _person.IsBirthday ? "yes" : "no";


        public PersonViewModel(PersonModel person, NavigationStore navigationStore)
        {
            _person = person;
            _navigationStore = navigationStore;

            if (_person.IsBirthday)
            {
                _ = MessageBox.Show("Happy birthday!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region Back Command

        private RelayCommand<object> _backCommand;

        public RelayCommand<object> BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new RelayCommand<object>(_ => Back(), CanExecute);
                };
                return _backCommand;
            }
        }

        private void Back()
        {
            _navigationStore.CurrentViewModel = new InputUserDataViewModel(_person, _navigationStore);
        }

        private bool CanExecute(object obj)
        {
            return true;
        }

        #endregion



    }
}
