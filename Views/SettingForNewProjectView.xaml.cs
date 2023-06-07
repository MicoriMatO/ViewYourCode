using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ViewYourCode.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingForNewProjectView.xaml
    /// </summary>
    public partial class SettingForNewProjectView : Window
    {
        public bool IsNewProject { get; set; }
        public SettingForNewProjectView()
        {
            InitializeComponent();

            this.Closing += CreateProject_Closing;

            TextBox_NameProject.Text = "NewProject";
            TextBox_PathProject.Text = "C:\\\\Users\\";
            IsNewProject = false;
        }

        private void Button_OpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            
            folderDialog.ShowDialog();
            TextBox_PathProject.Text = folderDialog.SelectedPath + "\\";   
        }

        private void Button_CreateProject_Click(object sender, RoutedEventArgs e)
        {
            IsNewProject = true;
            this.Close();
        }
        public void CreateProject_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsNewProject)
            {
                MainWindow mainWindow = new MainWindow(TextBox_NameProject.Text, TextBox_PathProject.Text);
                mainWindow.Show();
            }
            else
            {
                MainMenuView mainMenu = new MainMenuView();
                mainMenu.Show();
            }
            
        }
    }
}
