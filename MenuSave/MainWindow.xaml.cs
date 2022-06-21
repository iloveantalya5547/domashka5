using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;



namespace MenuSave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Boolean saveAll = false;
        private string path;
        private Boolean textChanged = false;
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileButton();
        }

        private void OpenFileButton()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                Text.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void btnSaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileAsButton();
        }


        private void SaveFileAsButton()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, Text.Text);
                path = saveFileDialog.FileName;
                SaveButton.IsEnabled = true;
                saveAll = true;
            }
                
        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileButton();
        }

        private void SaveFileButton()
        {
            File.WriteAllText(path, Text.Text);
        }


        private void btnCloseFile_Click(object sender, RoutedEventArgs e)
        {
            if (textChanged == true)
                CloseAndSaveButton();
            else
                this.Close();
        }

        private void CloseAndSaveButton()
        {
            if (saveAll == true)
            {
                File.WriteAllText(path, Text.Text);
                this.Close();
            }
            else
            {
                MenuClose closemenu = new MenuClose();

                if (closemenu.ShowDialog() == true)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text File (*.txt)|*.txt";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        File.WriteAllText(saveFileDialog.FileName, Text.Text);
                        path = saveFileDialog.FileName;
                    }
                }
            }
        }
        

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text.Text.Length > 0)
            {
                textChanged = true;
                SaveAsButton.IsEnabled = true;

            }
        }
    }
}
