using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace saper4g1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int rozmiar = 10;
    public MainWindow()
    {
        InitializeComponent();
        for (int i = 0; i < rozmiar; i++)
        {
            plansza.RowDefinitions.Add(new RowDefinition());
            plansza.ColumnDefinitions.Add(new ColumnDefinition());
        }
    }
}