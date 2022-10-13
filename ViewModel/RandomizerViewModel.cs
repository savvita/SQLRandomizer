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
        private int count = 1;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        private double nullPercentage = 0.0;
        public double NullPercentage
        {
            get => nullPercentage;
            set
            {
                nullPercentage = value;
                OnPropertyChanged(nameof(NullPercentage));
            }
        }

        private Model.SQLRandomizer randomizer = new Model.SQLRandomizer();
        private string? query;

        public string? Query
        {
            get => query;
            set
            {
                query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private string? inserts;

        public string? Inserts
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
            if(this.Query != null && checkPercentage())
            {
                Inserts = randomizer.GetValues(this.Query, Count, NullPercentage);
            }
        }

        private bool checkPercentage()
        {
            return NullPercentage >= 0 && NullPercentage <= 1;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
