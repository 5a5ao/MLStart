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

    private void pauseClick(object sender, RoutedEventArgs e)
    {
        Button pauseButton = (Button)sender;
        Button playButton = FindButtonByName("playButton"); // Получаем кнопку воспроизведения из XAML

        if (pauseButton != null && playButton != null)
        {
            pauseButton.IsEnabled = false;
            pauseButton.Opacity = 0.5;

            playButton.IsEnabled = true;
            playButton.Opacity = 1.0;
        }

    }

    private void playClick(object sender, RoutedEventArgs e)
    {
        Button playButton = (Button)sender;
        Button pauseButton = FindButtonByName("pauseButton"); // Получаем кнопку воспроизведения из XAML

        if (playButton != null && playButton != null)
        {
            playButton.IsEnabled = false;
            playButton.Opacity = 0.5;

            pauseButton.IsEnabled = true;
            pauseButton.Opacity = 1.0;
        }

    }
    private Button FindButtonByName(string name)
    {
        var button = (Button)FindName(name);
        return button;
    }
}

