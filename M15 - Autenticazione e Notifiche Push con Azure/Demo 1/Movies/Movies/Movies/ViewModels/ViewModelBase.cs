﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Movies.Models;
using Movies.Services;

namespace Movies.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public static readonly IMoviesSource MoviesSource;

        private static bool _isLoading;
        private static IList<Movie> _allMovies;

        static ViewModelBase()
        {
            MoviesSource = new MobileAppMoviesSource(
                            MobileAppAuthenticator.Instance.ServiceClient);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }
        public IList<Movie> AllMovies
        {
            get => _allMovies;
            set => Set(ref _allMovies, value);
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(name);
            return true;
        }

        #endregion
    }
}