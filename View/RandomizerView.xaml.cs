using Microsoft.Win32;
using SQLRandomizer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
        RandomizerViewModel randomizer = new RandomizerViewModel();
        public RandomizerView()
        {
            InitializeComponent();
            this.DataContext = randomizer;

            randomizer.Loading += ShowLoading;
            randomizer.Loaded += HideLoading;
        }

        private void HideLoading()
        {
            this.Loading.Visibility = Visibility.Hidden;
        }

        private void ShowLoading()
        {
            this.Loading.Visibility = Visibility.Visible;
        }

        private void Count_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(e.Text.Any(ch => !Char.IsDigit(ch)))
            {
                e.Handled = true;
            }
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.sql)|*.sql";
            if (openFileDialog.ShowDialog() == true)
            {
                query.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.sql)|*.sql";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, inserts.Text);
            }
        }

    }
}
