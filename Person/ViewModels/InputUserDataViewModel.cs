using Person.Exceptions;
using Person.Models;
using Person.Stores;
using Person.Tools;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Person.ViewModels
{
    public class InputUserDataViewModel : ViewModelBase
    {

        #region Private Fields
        private bool _isEnabled = true;
        private RelayCommand<object> _proceedCommand;
        private PersonModel _person;
        private readonly NavigationStore _navigationStore;
        #endregion

        #region Person Fields
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        private string _surname;

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                NotifyPropertyChanged();
            }
        }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _birthdate = new DateTime(2023, 3, 20);

        public DateTime Birthdate
        {
            get => _birthdate;
            set
            {
                _birthdate = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Properties
        public RelayCommand<object> ProceedCommand
        {
            get
            {
                if (_proceedCommand == null)
                {
                    _proceedCommand = new RelayCommand<object>(_ => Proceed(), CanExecute);
                };
                return _proceedCommand;
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged();
            }
        }

        #endregion


        private async void Proceed()
        {
            try
            {
                IsEnabled = false;
                _person = await Task.Run(() => new PersonModel(Name,
                                                               Surname,
                                                               Email,
                                                               Birthdate));
                _navigationStore.CurrentViewModel = new PersonViewModel(_person, _navigationStore);
            }
            catch (BirthDateIsInFutureException e)
            {
                HandleException(e);
            }
            catch (BirthDateIssTooFarAwayException e)
            {
                HandleException(e);
            }
            catch (InvalidEmailAddressException e)
            {
                HandleException(e);
            }
            finally
            {
                IsEnabled = true;
            }
        }

        private void HandleException(Exception e)
        {
            _ = MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) &&
                !string.IsNullOrEmpty(Email) && Birthdate != null;
        }

        #region Constructors

        public InputUserDataViewModel(PersonModel person, NavigationStore navigationStore) : this(navigationStore)
        {
            _person = person;
            Name = _person.Name;
            Surname = _person.Surname;
            Email = _person.Email.ToString();
            Birthdate = _person.BirthDate;
        }

        public InputUserDataViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        #endregion



    }
}
