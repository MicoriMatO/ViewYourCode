using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using ViewYourCode.Models;
using ViewYourCode.Models.Prefabs;
using ViewYourCode.Models.TestPreFabs;
using ViewYourCode.Controllers;

namespace ViewYourCode
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<BasePreFabsModel> listEditGrid = new ObservableCollection<BasePreFabsModel>();
        public ObservableCollection<BasePreFabsModel> listPreFabs = new ObservableCollection<BasePreFabsModel>();
        private int indexDrag;
        private int indexDrop;
        //private int nameIncrimetn = 0;
        private bool flagFromAddedList;
        public MainWindow()
        {
            InitializeComponent();

            PreFabsInit();
        }

        private void PreFabsInit()
        {
            
            
            listPreFabs.Add(new CycleTest());
            listPreFabs.Add(new TestUnit());

            PreFabsList.ItemsSource = listPreFabs;
            EditGrid.ItemsSource = listEditGrid;

        }

        private void TestMeta()
        {
            BasePreFabsModel basePreFabsModel = new BasePreFabsModel();
        }

        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void PreFabs_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Pressed)
            {   
                if (((ListBox)sender).Name == "PreFabsList")
                {
                    flagFromAddedList = true;  
                }
                else
                {
                    flagFromAddedList = false; 
                }
                try
                {
                    indexDrag = ((ListBox)sender).SelectedIndex;
                    object itemDrags = ((ListBox)sender).Items[((ListBox)sender).SelectedIndex];
                    DragDrop.DoDragDrop(((ListBox)sender), itemDrags, DragDropEffects.Move);
                }
                catch (Exception)
                {
                    //MessageBox.Show(er.Message);
                }
                
            }
        }
        private void EditList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                BasePreFabsModel si = listEditGrid[((ListBox)sender).SelectedIndex];
                
                DragDrop.DoDragDrop((ListBox)sender, si, DragDropEffects.Move);
            }
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            var temp = new object();
            
            if (flagFromAddedList)
            {
                temp = listPreFabs[indexDrag];
            }
            else
            {
                temp = listEditGrid[indexDrag];
            }
            
            indexDrop = ((ListBox)sender).SelectedIndex;
            var dropItem = e.Data.GetData(temp.GetType());

            if (!flagFromAddedList)
            {
                if (indexDrop == indexDrag)
                {
                    return;
                }

                listEditGrid.Remove(listEditGrid[indexDrag]);
            }                

            if (indexDrop == -1)
            {
                listEditGrid.Add((BasePreFabsModel)dropItem);
            }
            else
            {
                listEditGrid.Insert(indexDrop, (BasePreFabsModel)dropItem);  
            }
            
            flagFromAddedList = false;

            EditGrid.ItemsSource = listEditGrid;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FileWriter fileWriter = new FileWriter();
            fileWriter.WriteToSkript(listEditGrid);
        }
    }
}
