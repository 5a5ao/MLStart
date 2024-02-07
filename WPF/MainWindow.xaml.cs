using System.Windows;
using System.Windows.Controls;

namespace Program;

/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

    }
    private void menuClick(object sender, RoutedEventArgs e)
    {
        Button button = (Button)sender;
        ContextMenu contextMenu = button.ContextMenu;
        contextMenu.PlacementTarget = button;
        contextMenu.IsOpen = true;
    }

}

