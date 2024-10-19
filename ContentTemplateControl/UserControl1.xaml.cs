using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContentTemplateControl {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [ContentProperty("MyContent")]
    public partial class UserControl1 : UserControl, INotifyPropertyChanged {
        public static readonly DependencyProperty MyContentProperty = DependencyProperty.Register(
            "MyContent", typeof(object), typeof(UserControl1));
        public object MyContent {
            get { return (object)GetValue(MyContentProperty); }
            set { SetValue(MyContentProperty, value); }
        }
        private Int32 _MyCounter = 0;
        public Int32 MyCounter 
        {
            get 
            {
                return _MyCounter;
            } 
            set
            {
                if (value != _MyCounter) {
                    _MyCounter = value;
                    TextContent = $"My current count is {MyCounter.ToString()}.";
                    OnPropertyChanged(nameof(MyCounter));
                }
            }
        }
        public string _TextContent = "";


        public string TextContent {
            get
            {
                return getCountString();
            }
            set
            {
                if (_TextContent != value) {
                    _TextContent = value;
                    OnPropertyChanged(nameof(TextContent));
                }
            }
        }
        public UserControl1() {
            InitializeComponent();
            TextContent = $"My current count is {_MyCounter.ToString()}";
            DataContext = this;
        }
        public void IncrementCounter(object sender, EventArgs args) {
            MyCounter = _MyCounter + 1;
            Debug.WriteLine($"New Counter Value: {MyCounter}");
        }
        public string getCountString() {
            return $"My current count is {MyCounter.ToString()}.";
        }

        #region INotifyPropertyChange

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChangedEventArgs propertyChangedEventArgs = new(propertyName);
                PropertyChanged(this, propertyChangedEventArgs);
            }
        }
        #endregion
    }
}
