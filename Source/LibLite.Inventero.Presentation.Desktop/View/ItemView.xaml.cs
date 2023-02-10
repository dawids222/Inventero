using LibLite.Inventero.Presentation.Desktop.Models.Views.Inputs;
using LibLite.Inventero.Presentation.Desktop.View.Controls;
using LibLite.Inventero.Presentation.Desktop.ViewModel;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibLite.Inventero.Presentation.Desktop.View
{
    /// <summary>
    /// Interaction logic for ItemView.xaml
    /// </summary>
    public partial class ItemView : UserControl
    {
        private const int INPUT_WIDTH = 300;

        public ItemView()
        {
            InitializeComponent();
        }

        // IDEA: How about moving this logic to event bus handler?
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CreateInputs();
            MoveButtonsToTheEnd();
        }

        private void CreateInputs()
        {
            var viewModel = (ItemViewModel)DataContext;
            var inputs = viewModel.Inputs;
            foreach (var input in inputs)
            {
                var control = CreateInput(input, viewModel);
                Content.Children.Add(control);
            }
        }

        private UIElement CreateInput(Input input, ItemViewModel viewModel)
        {
            return input switch
            {
                StringInput stringInput => CreateStringInput(stringInput, viewModel),
                NumberInput doubleInput => CreateNumberInput(doubleInput, viewModel),
                DateInput dateInput => CreateDateInput(dateInput, viewModel),
                SelectInput selectInput => CreateSelectInput(selectInput, viewModel),
                _ => throw new NotImplementedException(),
            };
        }

        private static StackPanel CreateStringInput(StringInput input, ItemViewModel viewModel)
        {
            var label = CreateInputLabel(input);
            var textBox = CreateInputTextBox(input, viewModel);
            return CreateInputStackPanel(label, textBox);
        }

        private StackPanel CreateNumberInput(NumberInput input, ItemViewModel viewModel)
        {
            var label = CreateInputLabel(input);
            var textBox = CreateInputTextBox(input, viewModel);
            textBox.TextChanged += NumberTextBox_TextChanged;
            return CreateInputStackPanel(label, textBox);
        }

        private static StackPanel CreateDateInput(DateInput input, ItemViewModel viewModel)
        {
            var label = CreateInputLabel(input);
            var datePicker = CreateInputDatePicker(input, viewModel);
            return CreateInputStackPanel(label, datePicker);
        }

        private static StackPanel CreateSelectInput(SelectInput input, ItemViewModel viewModel)
        {
            var label = CreateInputLabel(input);
            var select = new Select()
            {
                Width = INPUT_WIDTH,
                DisplayMemberPath = input.DisplayMember,
            };
            select.SetBinding(Select.ItemsSourceProperty, CreateDefaultBinding(input.ItemsBinding, viewModel));
            select.SetBinding(Select.SearchTextProperty, CreatePropertyChangedBinding(input.SearchBinding, viewModel));
            select.SetBinding(Select.SelectedItemProperty, CreatePropertyChangedBinding(input.Binding, viewModel));
            select.SetBinding(Select.SearchCommandProperty, CreateDefaultBinding(input.SearchCommand, viewModel));
            select.SetBinding(Select.ItemSelectedCommandProperty, CreateDefaultBinding(input.ItemSelectedCommad, viewModel));
            return CreateInputStackPanel(label, select);
        }

        private static Label CreateInputLabel(Input input)
        {
            return new Label()
            {
                Width = INPUT_WIDTH,
                Content = input.Label,
            };
        }

        private static TextBox CreateInputTextBox(Input input, ItemViewModel viewModel)
        {
            var textBox = new TextBox()
            {
                Width = INPUT_WIDTH,
            };
            var binding = CreateLostFocusBinding(input.Binding, viewModel);
            textBox.SetBinding(TextBox.TextProperty, binding);
            return textBox;
        }

        private static DatePicker CreateInputDatePicker(Input input, ItemViewModel viewModel)
        {
            var datePicker = new DatePicker()
            {
                Width = INPUT_WIDTH,
            };
            var binding = CreatePropertyChangedBinding(input.Binding, viewModel);
            datePicker.SetBinding(DatePicker.TextProperty, binding);
            return datePicker;
        }

        private static Binding CreateDefaultBinding(string binding, ItemViewModel viewModel)
        {
            return new Binding(binding) { Source = viewModel };
        }

        private static Binding CreatePropertyChangedBinding(string binding, ItemViewModel viewModel)
        {
            return new Binding(binding)
            {
                Source = viewModel,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
        }

        private static Binding CreateLostFocusBinding(string binding, ItemViewModel viewModel)
        {
            return new Binding(binding)
            {
                Source = viewModel,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.LostFocus,
            };
        }

        private static StackPanel CreateInputStackPanel(params UIElement[] controls)
        {
            var panel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10),
            };
            foreach (var control in controls)
            {
                panel.Children.Add(control);
            }
            return panel;
        }

        private void MoveButtonsToTheEnd()
        {
            var buttons = Content.Children[0];
            Content.Children.RemoveAt(0);
            Content.Children.Add(buttons);
        }

        private void NumberTextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var builder = new StringBuilder();
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' };
            foreach (char c in textBox.Text)
            {
                if (Array.IndexOf(validChars, c) == -1) { continue; }
                builder.Append(c);
            }
            textBox.Text = builder.ToString();
        }
    }
}
