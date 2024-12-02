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
    private int iloscbomb = 10;
    private int iloscFlag;
    public MainWindow()
    {
        InitializeComponent();
        iloscFlag = iloscbomb;
        for (int i = 0; i < rozmiar; i++)
        {
            plansza.RowDefinitions.Add(new RowDefinition());
            plansza.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int i = 0; i < rozmiar; i++)
        for (int j = 0; j < rozmiar; j++)
        {
            Przycisk przycisk = new Przycisk
            {
                Wartosc = 0,
                FontSize = 30,
                Background = Brushes.LightGray,
                Foreground = Brushes.Black
            };
            przycisk.Click += Przyciskkliknij;
            przycisk.MouseRightButtonUp += PrzyciskPrawyClick;
            Grid.SetRow(przycisk, i);
            Grid.SetColumn(przycisk, j);
            plansza.Children.Add(przycisk);
        }
        
        RozstawBomby(iloscbomb);
       // PokazPlansze();
    }

    private void PrzyciskPrawyClick(object sender, MouseButtonEventArgs e)
    {
        Przycisk przycisk = (Przycisk) sender;
        int x = Grid.GetRow(przycisk);
        int y = Grid.GetColumn(przycisk);
        Flaga(x, y);
    }

    private void Przyciskkliknij(object sender, RoutedEventArgs e)
    {
        Przycisk przycisk = (Przycisk) sender;
        int x = Grid.GetRow(przycisk);
        int y = Grid.GetColumn(przycisk);
        OdkryjPrzycisk(x, y);
    }

    private Przycisk WyszukajPrzyciski(int x, int y)
    { 
        var przycisk = plansza.Children.Cast<Przycisk>()
            .First(p => Grid.GetRow(p) == x && Grid.GetColumn(p) == y);
        return przycisk;
    }

    private void RozstawBomby(int ilosc)
    {
     Random random = new Random();
     while (ilosc > 0)
     {
            int x = random.Next(rozmiar);
            int y = random.Next(rozmiar);
            Przycisk przycisk = WyszukajPrzyciski(x, y);
            if(przycisk.Wartosc == 0)
            {
                przycisk.Wartosc = 10;
                ilosc--;
                ZliczBomby(x,y);
            }
     }
    }

    private void PokazPlansze()
    {
        plansza.Children.Cast<Przycisk>().ToList().ForEach(p =>
        {
            if (p.Wartosc == 10)
            {
                p.Content = "💣";
            }
            else
            {
                p.Content = p.Wartosc;
            }
        });
    }
   private void OdkryjPrzycisk(int x, int y)
    {
        
        Przycisk przycisk = WyszukajPrzyciski(x, y);
        if(przycisk.Content != null)
            return;
        
        if (przycisk.Wartosc == 10)
        {
            MessageBox.Show("Przegrałeś");
            PokazPlansze();
        }
        else
        {
            przycisk.Content = przycisk.Wartosc;
            if(przycisk.Wartosc != 0)
                return;
            
            for (int i = x-1; i <= x+1; i++)
            for (int j = y-1; j <= y+1; j++)
            {
                if (i >= 0 && i < rozmiar && j >= 0 && j < rozmiar )
                {
                    OdkryjPrzycisk(i, j);
                    
                }
            }
        }
    }


    private void Flaga(int x, int y)
    {
        Przycisk przycisk = WyszukajPrzyciski(x, y);
        if (przycisk.Content == null)
        {
            if(iloscFlag > 0)
            {
                przycisk.Content = "🚩";
                iloscFlag--;
            }
            else
            {
                if (przycisk.Content.ToString() == "🚩")
                {
                    przycisk.Content = null;
                    iloscFlag++;
                }
            }
        }
    }
   

    private void ZliczBomby(int x, int y)
    {
        for (int i = x-1; i <= x+1; i++)
        for (int j = y-1; j <= y+1; j++)
        {
            if (i >= 0 && i < rozmiar && j >= 0 && j < rozmiar)
            {
                Przycisk przycisk = WyszukajPrzyciski(i, j);
                if (przycisk.Wartosc != 10)
                {
                    przycisk.Wartosc++;
                }
            }
        }
            
        
    }
}