using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Diagnostics;

namespace ContentControl {
    /// <summary>
    /// Interaction logic for MyCustomContent.xaml
    /// </summary>
    public partial class MyCustomContent : UserControl, INotifyPropertyChanged {
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        private string _current_image_source = "";
        public string MyImageSource {
            get
            {
                return _current_image_source;
            }
            set {
                if (File.Exists(value) && IsImage(value) && _current_image_source != value) {
                    _current_image_source = value;
                    OnPropertyChanged(nameof(MyImageSource));
                }
            }
        }
        public string _MyImagePath = "";
        public string MyImagePath 
        {
            get
            {
                return _MyImagePath;
            }
            set
            {
                Debug.WriteLine("Inside setter!");
                if (File.Exists(value) && IsImage(value) && value != _MyImagePath) {
                    Debug.WriteLine("File exists and is an image!");
                    _MyImagePath = value;
                    OnPropertyChanged(nameof(MyImagePath));
                }
            }
        }
        public MyCustomContent() {
            InitializeComponent();

            DataContext = this;
        }
        private bool IsImage(string f) {
            bool result = ImageExtensions.Contains(System.IO.Path.GetExtension(f).ToUpperInvariant());
            Debug.WriteLine($"{f} is {((result) ? "an" : "not an")} image");
            return result;
        }

        private void ImageSource_KeyUp(object sender, KeyEventArgs e) {
            string current_str = ImageSourceBox.Text;
            Debug.WriteLine($"Current string {current_str}");
            Thread.Sleep(2000);
            string new_str = ImageSourceBox.Text;
            Debug.WriteLine($"New string {current_str}");
            Debug.WriteLine($"Result Comparison: {current_str.Equals(new_str)}");
            if (!current_str.Equals(new_str)) {
                return;
            }
            MyImagePath = new_str;
            MyImageSource = new_str;
        }

        #region INotifyPropertyChanged
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
