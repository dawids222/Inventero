using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibLite.Inventero.Presentation.Desktop.View.Controls
{
    /// <summary>
    /// Interaction logic for Select.xaml
    /// </summary>
    public partial class Select : UserControl
    {
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set
            {
                SelectedItemText = GetDisplayValue(value);
                SetValue(SelectedItemProperty, value);
            }
        }

        public string SelectedItemText
        {
            get { return (string)GetValue(SelectedItemTextProperty); }
            set { SetValue(SelectedItemTextProperty, value); }
        }

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }

        public ICommand ItemSelectedCommand
        {
            get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
            set { SetValue(ItemSelectedCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object),
                typeof(Select), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedItemTextProperty =
            DependencyProperty.Register("SelectedItemText", typeof(string),
                typeof(Select), new PropertyMetadata(null));

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string),
                typeof(Select), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable),
                typeof(Select), new PropertyMetadata(null));

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string),
                typeof(Select), new PropertyMetadata(null));

        public static readonly DependencyProperty SearchCommandProperty =
            DependencyProperty.Register("SearchCommand", typeof(ICommand),
                typeof(Select), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemSelectedCommandProperty =
            DependencyProperty.Register("ItemSelectedCommand", typeof(ICommand), typeof(Select), new PropertyMetadata(null));

        public Select()
        {
            InitializeComponent();
            Loaded += Select_Loaded;
        }

        private void Select_Loaded(object sender, RoutedEventArgs e)
        {
            SelectedItemText = GetDisplayValue(SelectedItem);

            searchTextBox.GotFocus += SearchTextBox_GotFocus;
            searchTextBox.TextChanged += SearchTextBox_TextChanged;

            openButton.Click += OpenButton_Click;
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            itemsListView.Visibility = Visibility.Visible;
        }

        private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText = searchTextBox.Text;
            await ExecuteAsync(SearchCommand);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            itemsListView.Visibility = SwitchVisibility(itemsListView.Visibility);
        }

        private async void ItemsListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (ListViewItem)sender;
            if (!item.IsSelected) { return; }
            SelectedItem = item.Content;
            await ExecuteAsync(ItemSelectedCommand);
            itemsListView.Visibility = Visibility.Collapsed;
        }

        private string GetDisplayValue(object value)
        {
            if (value is null) { return string.Empty; }
            var type = value.GetType();
            var property = type.GetProperty(DisplayMemberPath);
            var result = property.GetValue(value, null);
            return (string)result;
        }

        private static Visibility SwitchVisibility(Visibility visibility)
        {
            return visibility switch
            {
                Visibility.Visible => Visibility.Collapsed,
                Visibility.Collapsed => Visibility.Visible,
                Visibility.Hidden => Visibility.Visible,
                _ => throw new NotImplementedException(),
            };
        }

        private static Task ExecuteAsync(ICommand command, object parameter = null)
        {
            if (command is null) { return Task.CompletedTask; }
            return command switch
            {
                IAsyncRelayCommand asyncCommand => asyncCommand.ExecuteAsync(parameter),
                _ => Task.Run(() => command.Execute(parameter)),
            };
        }
    }
}
