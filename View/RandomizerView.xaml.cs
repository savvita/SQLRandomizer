using SQLRandomizer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SQLRandomizer.View
{
    /// <summary>
    /// Interaction logic for RandomizerView.xaml
    /// </summary>
    public partial class RandomizerView : Window
    {
        public RandomizerView()
        {
            InitializeComponent();
            this.DataContext = new RandomizerViewModel();
        }

        private void Count_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(e.Text.Any(ch => !Char.IsDigit(ch)))
            {
                e.Handled = true;
            }
        }

    }
}
