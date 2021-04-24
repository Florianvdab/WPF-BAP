using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WPF_BAP.Models;
using WPF_BAP.ViewModels.Commands;
using static WPF_BAP.ViewModels.MainViewModel;

namespace WPF_BAP.ViewModels
{
    class AddArticleViewModel: INotifyPropertyChanged, ICloseWindows
    {
        #region Properties & Commands
        public long Artikel { get; set; }
        public string KassaNed { get; set; }
        public string KassaFr { get; set; }
        public string Kwaliteit { get; set; }
        public string PubliDate { get; set; }
        public string VkpEur { get; set; }
        public short? Hoofdgroep { get; set; }
        public short? Seizoen { get; set; }
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
                OnPropertyChanged(nameof(Error));
            }
        }

        public Action Close { get; set; }

        public ICommand AddArticleCommand { get; set; }
        #endregion

        #region Constructor
        public AddArticleViewModel()
        {
            this.AddArticleCommand = new RelayCommand(AddArticle);
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
            Article art = new Article();
            art.Artikel = Artikel;
            art.KassaNed = KassaNed;
            art.KassaFr = KassaFr;
            art.Kwaliteit = Kwaliteit;
            art.PubliDate = PubliDate;
            art.VkpEur = VkpEur;
            art.Hoofdgroep = Hoofdgroep;
            art.Seizoen = Seizoen;

            try
            {
                exampleContext context = new exampleContext();
                context.Articles.Add(art);
                MainViewModel.Instance.Articles.Add(art);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            finally
            {
                Close?.Invoke();
            }
        }
        #endregion
    }
}
