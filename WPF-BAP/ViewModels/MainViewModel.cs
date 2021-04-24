using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using WPF_BAP.Models;
using WPF_BAP.ViewModels.Commands;
using WPF_BAP.Views;
using static WPF_BAP.ViewModels.MainViewModel;

namespace WPF_BAP.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Properties & Commands
        public ObservableCollection<Article> Articles { get; set; }

        public Timer Timer { get; set; }

        private string _error { get; set; }
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                Timer = new Timer();
                Timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                Timer.Interval = 5000;
                Timer.AutoReset = false;
                Timer.Enabled = true;
                OnPropertyChanged(nameof(Error));
            }
        }
        public Article _selectedArticle { get; set; }
        public Article SelectedArticle
        {
            get
            {
                return _selectedArticle;
            }
            set
            {
                _selectedArticle = value;
                OnPropertyChanged(nameof(SelectedArticle));
            }
        }

        private static MainViewModel _instance = new MainViewModel();
        public static MainViewModel Instance { get { return _instance; } }

        public ICommand AddArticleCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            exampleContext context = new exampleContext();
            Articles = new ObservableCollection<Article>(context.Articles);

            this.AddArticleCommand = new RelayCommand(AddArticle);
            this.SaveCommand = new RelayCommand(Save);
            this.DeleteCommand = new RelayCommand(Delete);
        }
        #endregion

        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Command Functions
        public void AddArticle()
        {
            AddArticleWindow window = new AddArticleWindow();
            window.Show();
        }
        public void Save()
        {
            try
            {
                exampleContext context = new exampleContext();
                context.Articles.Update(SelectedArticle);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }
        public void Delete()
        {
            try
            {
                exampleContext context = new exampleContext();
                context.Articles.Remove(SelectedArticle);
                MainViewModel.Instance.Articles.Remove(SelectedArticle);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }       
        }
        #endregion

        #region Timer Function
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Error = "";
            Timer.Dispose();
        }
        #endregion
    }

    public interface ICloseWindows
    {
        public Action Close { get; set; }
    }

    public class WindowCloser
    {
        public static bool GetEnableWindowClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableWindowClosingProperty);
        }

        public static void SetEnableWindowClosing(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableWindowClosingProperty, value);
        }

        // Using a DependencyProperty as the backing store for EnableWindowClosing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableWindowClosingProperty =
            DependencyProperty.RegisterAttached("EnableWindowClosing", typeof(bool), typeof(WindowCloser), new PropertyMetadata(false, OnEnableWindowClosingChanged));

        private static void OnEnableWindowClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is AddArticleViewModel vm)
                    {
                        vm.Close += () =>
                        {
                            window.Close();
                        };
                    }
                };

            }
        }
    }
}
