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
using System.Windows.Shapes;

namespace ViewYourCode.Views
{
    /// <summary>
    /// Логика взаимодействия для MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : Window
    {
        private bool CloseProgram = true;
        public MainMenuView()
        {
            InitializeComponent();

            this.Closing += MainMenu_Closing;
        }

        private void CreateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            CloseProgram = false;

            this.Close();
        }

        public void MainMenu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CloseProgram)
            {
                return;
            }

            SettingForNewProjectView newProject = new SettingForNewProjectView();
            newProject.Show();
        }
    }
}
