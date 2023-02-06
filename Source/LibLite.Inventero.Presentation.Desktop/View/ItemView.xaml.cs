using LibLite.Inventero.Presentation.Desktop.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace LibLite.Inventero.Presentation.Desktop.View
{
    /// <summary>
    /// Interaction logic for ItemView.xaml
    /// </summary>
    public partial class ItemView : UserControl
    {
        private const int LABEL_WIDTH = 100;
        private const int INPUT_WIDTH = 300;

        public ItemView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ItemViewModel;
            var inputs = viewModel.Inputs;

            foreach (var input in inputs)
            {
                if (input is StringInput)
                {
                    var label = new Label()
                    {
                        Width = LABEL_WIDTH,
                        Content = input.Label,
                    };
                    var textBox = new TextBox()
                    {
                        Width = INPUT_WIDTH,
                    };
                    var binding = new Binding(input.Binding)
                    {
                        Source = viewModel,
                    };
                    textBox.SetBinding(TextBox.TextProperty, binding);
                    var panel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };
                    panel.Children.Add(label);
                    panel.Children.Add(textBox);
                    Content.Children.Add(panel);
                }
                if (input is DoubleInput)
                {
                    var label = new Label()
                    {
                        Width = LABEL_WIDTH,
                        Content = input.Label,
                    };
                    var textBox = new TextBox()
                    {
                        Width = INPUT_WIDTH,
                    };
                    var binding = new Binding(input.Binding)
                    {
                        Source = viewModel,
                    };
                    textBox.SetBinding(TextBox.TextProperty, binding);
                    var panel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };
                    panel.Children.Add(label);
                    panel.Children.Add(textBox);
                    Content.Children.Add(panel);
                }
                if (input is DropDownInput i)
                {
                    var label = new Label()
                    {
                        Width = LABEL_WIDTH,
                        Content = input.Label,
                    };
                    var comboBox = new ComboBox()
                    {
                        Width = INPUT_WIDTH,
                        IsEditable = true,
                        StaysOpenOnEdit = true,
                        IsTextSearchEnabled = false,
                        DisplayMemberPath = i.DisplayMember,
                        Tag = i,
                    };
                    var itemsSourceBinding = new Binding(i.ItemsBinding)
                    {
                        Source = viewModel,
                    };
                    var textBinding = new Binding(i.SearchBinding)
                    {
                        Source = viewModel,
                    };
                    var selectedItemBinding = new Binding(i.SelectedItemBinding)
                    {
                        Source = viewModel,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    };
                    comboBox.SetBinding(ItemsControl.ItemsSourceProperty, itemsSourceBinding);
                    comboBox.SetBinding(ComboBox.TextProperty, textBinding);
                    comboBox.SetBinding(Selector.SelectedItemProperty, selectedItemBinding);
                    comboBox.IsKeyboardFocusWithinChanged += ComboBox_IsKeyboardFocusWithinChanged;
                    comboBox.SelectionChanged += ComboBox_SelectionChanged;
                    comboBox.Loaded += ComboBox_Loaded;
                    var binding = new Binding(input.Binding)
                    {
                        Source = viewModel,
                    };
                    comboBox.SetBinding(TextBox.TextProperty, binding);
                    var panel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };
                    panel.Children.Add(label);
                    panel.Children.Add(comboBox);
                    Content.Children.Add(panel);
                }
            }
            var buttons = Content.Children[0];
            Content.Children.RemoveAt(0);
            Content.Children.Add(buttons);
        }

        private void ComboBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isKeyboardFocusWithin = (bool)e.NewValue;
            if (!isKeyboardFocusWithin) { return; }
            var comboBox = sender as ComboBox;
            comboBox.IsDropDownOpen = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var input = (DropDownInput)comboBox.Tag;
            input.SelectionChangedCommand.Execute(null);
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var textBox = (TextBox)comboBox.Template.FindName("PART_EditableTextBox", comboBox);
            textBox.Tag = comboBox;
            textBox.TextChanged += ProductView_TextChanged;
        }

        private void ProductView_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBoxBase)sender;
            var comboBox = (ComboBox)textBox.Tag;
            var input = (DropDownInput)comboBox.Tag;
            input.SearchCommand.Execute(null);
        }
    }
}
