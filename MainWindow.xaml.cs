using Convertie.Assets;
using Convertie.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Convertie;

public enum ConvertingTypes
{
    Text,
    Hex,
    Base64,
    Base64URL,
    CBOR
}

public enum EncodingTypes
{
    UTF8,
    ASCII,
    UTF32,
    Unicode,
    BigEndianUnicode,
    Latin1
}

public partial class MainWindow : Window
{
    private bool _isHorizontal = true;
    private bool _convertOnTypeChange = false;
    private readonly Lock _textChangeEventTaskLock = new();

    public MainWindow()
    {
        InitializeComponent();
        PanelChangeIcon.Source = CustomIcons.ScreenVertical(SystemColors.AccentColor);
        TextEncodingDecodingCombobox.ItemsSource = Utils.GetEncodingDecodingTypes();
        TextEncodingDecodingCombobox.SelectedIndex = 0;
    }

    private void ShowError(string content, int duration = 3000)
    {
        ErrorTextBox.Text = content;
        MessageTextBox.Visibility = Visibility.Collapsed;
        ErrorTextBox.Visibility = Visibility.Visible;
        Task.Run(() =>
        {
            Task.Delay(duration).Wait();
            ShowMessage("Convert as you wish ;)");
        });
    }

    private void ShowMessage(string content)
    {
        ErrorTextBox.Text = null;
        MessageTextBox.Text = content;
        ErrorTextBox.Visibility = Visibility.Collapsed;
        MessageTextBox.Visibility = Visibility.Visible;
    }

    private void Convert()
    {
        try
        {
            OutputTextBox.Text = InputTextBox.Text.Convert((ConvertingTypes)InputComboBox.SelectedItem,
                (ConvertingTypes)OutputComboBox.SelectedItem,
                (EncodingTypes)TextEncodingDecodingCombobox.SelectedItem);
        }
        catch (Exception ex)
        {
            ShowError($"Failed to convert. Error: {ex.Message}", 5000);
        }
    }

    private void SetupContentGrid()
    {
        ContentGrid.ColumnDefinitions.Clear();
        ContentGrid.RowDefinitions.Clear();

        if (_isHorizontal)
        {
            _isHorizontal = false;
            PanelChangeIcon.Source = CustomIcons.ScreenHorizontal(SystemColors.AccentColor);

            ContentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            if (OutputElementsGrid.IsVisible)
            {
                ContentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            else
            {
                ContentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            }
            OutputElementsGrid.Margin = new Thickness(10, 0, 0, 0);

            Grid.SetColumn(InputElementsGrid, 0);
            Grid.SetColumn(OutputElementsGrid, 1);
        }
        else
        {
            _isHorizontal = true;
            PanelChangeIcon.Source = CustomIcons.ScreenVertical(SystemColors.AccentColor);

            ContentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            if (OutputElementsGrid.IsVisible)
            {
                ContentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            else
            {
                ContentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            }
            OutputElementsGrid.Margin = new Thickness(0, 10, 0, 0);

            Grid.SetRow(InputElementsGrid, 0);
            Grid.SetRow(OutputElementsGrid, 1);
        }
    }

    #region Overrides
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
    }
    #endregion

    #region Callbacks
    private void InputTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        Task.Run(() =>
        {
            lock (_textChangeEventTaskLock)
            {
                Dispatcher.Invoke(() =>
                {
                    if (string.IsNullOrEmpty(InputTextBox.Text))
                    {
                        OutputElementsGrid.Visibility = Visibility.Collapsed;
                        InputElementTitle.Visibility = Visibility.Collapsed;
                        InputComboBox.Visibility = Visibility.Collapsed;
                        SetupContentGrid();
                        return;
                    }

                    _convertOnTypeChange = false;

                    InputComboBox.ItemsSource = Utils.GetInputConvertingTypes(InputTextBox.Text);
                    InputComboBox.SelectedIndex = 0;

                    OutputComboBox.ItemsSource = Utils.GetOutputConvertingTypes(InputTextBox.Text, (ConvertingTypes)InputComboBox.SelectedItem);
                    OutputComboBox.SelectedIndex = 0;

                    TextEncodingDecodingCombobox.Visibility = Visibility.Collapsed;
                    if ((ConvertingTypes)InputComboBox.SelectedItem != ConvertingTypes.CBOR && (ConvertingTypes)OutputComboBox.SelectedItem != ConvertingTypes.CBOR)
                    {
                        if ((ConvertingTypes)InputComboBox.SelectedItem == ConvertingTypes.Text || (ConvertingTypes)OutputComboBox.SelectedItem == ConvertingTypes.Text)
                        {
                            TextEncodingDecodingCombobox.Visibility = Visibility.Visible;
                        }
                    }

                    OutputElementsGrid.Visibility = Visibility.Visible;
                    InputElementTitle.Visibility = Visibility.Visible;
                    InputComboBox.Visibility = Visibility.Visible;

                    Convert();
                    _convertOnTypeChange = true;
                });
            }
        });
    }

    private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        InputHint.Visibility = Visibility.Collapsed;
        // TODO: Check the clipboard and suggest the conversion 
    }

    private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(InputTextBox.Text))
        {
            InputHint.Visibility = Visibility.Visible;
        }
    }

    private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_convertOnTypeChange)
        {
            if (e.Source is ComboBox comboBox && comboBox.Name == "InputComboBox")
            {
                OutputComboBox.ItemsSource = Utils.GetOutputConvertingTypes(InputTextBox.Text, (ConvertingTypes)InputComboBox.SelectedItem);
                OutputComboBox.SelectedIndex = 0;
            }

            TextEncodingDecodingCombobox.Visibility = Visibility.Collapsed;
            if ((ConvertingTypes)InputComboBox.SelectedItem != ConvertingTypes.CBOR && (ConvertingTypes)OutputComboBox.SelectedItem != ConvertingTypes.CBOR)
            {
                if ((ConvertingTypes)InputComboBox.SelectedItem == ConvertingTypes.Text || (ConvertingTypes)OutputComboBox.SelectedItem == ConvertingTypes.Text)
                {
                    TextEncodingDecodingCombobox.Visibility = Visibility.Visible;
                }
            }

            Convert();
        }
    }

    private void PanelViewChangeButtonClick(object sender, RoutedEventArgs e)
    {
        SetupContentGrid();
    }
    #endregion
}
