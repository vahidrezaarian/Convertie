using Convertie.Utilities;
using System.Windows;
using System.Windows.Input;

namespace Convertie;

public enum ConvertingTypes
{
    Text,
    Hex,
    Base64,
    Base64URL,
    JSON,
    CBOR
}

public enum EncodingTypes
{
    UTF8,
    ASCII,
    UTF7,
    UTF32,
    Unicode,
    BigEndianUnicode,
    Latin1
}

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeComboBoxes(ConvertingTypes.Hex);
    }

    private void InitializeComboBoxes(ConvertingTypes selectedInputType)
    {
        var inputTypes = Utils.GetInputTypes();
        InputComboBox.ItemsSource = inputTypes;
        OutputComboBox.ItemsSource = Utils.GetOutputTypes(selectedInputType);
        TextEncodingDecodingCombobox.ItemsSource = Utils.GetEncodingDecodingTypes();

        InputComboBox.SelectedIndex = inputTypes.IndexOf(selectedInputType);
        OutputComboBox.SelectedIndex = 0;
        TextEncodingDecodingCombobox.SelectedIndex = 0;
    }


    #region Overrides
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
    }
    #endregion

    private void OutputTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }

    private void OutputTextBox_GotFocus(object sender, RoutedEventArgs e)
    {

    }

    private void InputTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }

    private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
    {

    }

    private void ConvertButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var output = Converter.Convert(InputTextBox.Text, (ConvertingTypes)InputComboBox.SelectedItem, 
                (ConvertingTypes)OutputComboBox.SelectedItem, 
                (EncodingTypes)TextEncodingDecodingCombobox.SelectedItem);

            if (output is null)
            {
                throw new Exception("Invalid output!");
            }
            OutputTextBox.Text = output;
        }
        catch (Exception ex)
        {
            // TODO: Show error
        }
    }

    private void OutputComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if ((ConvertingTypes)OutputComboBox.SelectedItem == ConvertingTypes.Text)
        {
            TextEncodingDecodingCombobox.Visibility = Visibility.Visible;
        }
        else
        {
            TextEncodingDecodingCombobox.Visibility = Visibility.Collapsed;
        }
    }

    private void InputComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
}
