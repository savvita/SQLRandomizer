using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SQLRandomizer.ViewModel
{
    internal class RandomizerViewModel : INotifyPropertyChanged
    {
        private Model.SQLRandomizer randomizer = new Model.SQLRandomizer();
        private string query;

        public string Query
        {
            get => query;
            set
            {
                query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private string inserts;

        public string Inserts
        {
            get => inserts;
            set
            {
                inserts = value;
                OnPropertyChanged(nameof(Inserts));
            }
        }

        private RelayCommand? getRandom;
        public RelayCommand GetRandom
        {
            get => getRandom ?? new RelayCommand(() => { GetRandomized(); });
        }

        private void GetRandomized()
        {
            if(this.Query != null)
            {
                Inserts = randomizer.GetValues(this.Query, 10);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
