﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using Soccer_App.Model;
using System.Collections.ObjectModel;

namespace Soccer_App.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        IList<Datum> _leaguesPreferences;

        public event PropertyChangedEventHandler PropertyChanged;

        private ImageSource foto;
        public ImageSource Foto
        {
            get { return foto; }
            set
            {
                foto = value;
                OnPropertyChanged();
            }
        }
        public IList<Datum> LeaguesPreferences
        {
            get
            {
                if (_leaguesPreferences == null)
                    _leaguesPreferences = new ObservableCollection<Datum>();
                return _leaguesPreferences;
            }
            set
            {
                SetValue(ref _leaguesPreferences, value);
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }
        protected void SetValue<T>(ref T backingFieled, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingFieled, value))

            {

                return;

            }

            backingFieled = value;

            OnPropertyChanged(propertyName);
        }
    }
}
