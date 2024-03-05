using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SolresolConverter;
using SolresolConverter.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Popups;
using static SolresolConverter.Converter.Converter;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SolresolConverterApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            var _enumVal = Enum.GetValues(typeof(SolresolFormat)).Cast<SolresolFormat>();
            sorsoInFormat.ItemsSource = _enumVal.ToList();
            sorsoOutFormat.ItemsSource = _enumVal.ToList();
            sorsoInFormat.SelectedItem = SolresolFormat.Sorso;
            sorsoOutFormat.SelectedItem = SolresolFormat.Ses;
            // TODO Re-enable when more formats are available.
            sorsoInFormat.IsEnabled = false;
            compoundWordsBox.Visibility = (sorsoOutFormat.SelectedItem as SolresolFormat?)
                .Value == SolresolFormat.SesCmgreen ? Visibility.Visible : Visibility.Collapsed;
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            var inFormat = sorsoInFormat.SelectedItem as SolresolFormat?;
            var outFormat = sorsoOutFormat.SelectedItem as SolresolFormat?;
            List<CompoundWord> temp = new();
            string[] compoundWordContents = compoundWordsBox.Text.Split(';');
            foreach ( var compoundWord in compoundWordContents ) {
                temp.Add(new CompoundWord(compoundWord));
            }
            try
            {
                string output = ConvertSolresol(sorsoIn.Text, inFormat.Value, outFormat.Value, temp);
                sorsoOut.Text = output;
            } catch (NotImplementedException)
            {
                sorsoOut.Text = "Sorry, this format is not available yet!";
            } catch (Exception ex)
            {
                sorsoOut.Text = "Oops. Something went wrong.";
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            sorsoIn.Text = string.Empty;
            sorsoOut.Text = string.Empty;
            compoundWordsBox.Text = string.Empty;
        }

        private void sorsoOutFormat_SelectionChanged(object sender, RoutedEventArgs e)
        {
            compoundWordsBox.Visibility = (sorsoOutFormat.SelectedItem as SolresolFormat?)
                .Value == SolresolFormat.SesCmgreen ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
