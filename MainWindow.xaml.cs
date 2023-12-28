using Microsoft.UI.Xaml;
using SolresolConverter;
using SolresolConverter.Converter;
using System;
using System.Linq;

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
            sorsoOutFormat.SelectedItem = SolresolFormat.SesCmgreen;
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            var inFormat = sorsoInFormat.SelectedItem as SolresolFormat?;
            var outFormat = sorsoOutFormat.SelectedItem as SolresolFormat?;
            string output = Converter.ConvertSolresol(sorsoIn.Text, inFormat.Value, outFormat.Value);
            sorsoOut.Text = output;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            sorsoIn.Text = string.Empty;
            sorsoOut.Text = string.Empty;
        }
    }
}
