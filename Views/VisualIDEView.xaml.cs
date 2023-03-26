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
        public Envoriment.VisualPrimitivEnvoriment VisualPrimitivEnvoriment;
        public List<BasePreFabsModel> preFabsList;
        
        public MainWindow()
        {
            VisualPrimitivEnvoriment = new Envoriment.VisualPrimitivEnvoriment();
            preFabsList = new List<BasePreFabsModel>();




            InitializeComponent();

            EditGrid.MouseMove += VisualPrimitivEnvoriment.Grid_MouseMove;
            EditGrid.MouseLeftButtonUp += VisualPrimitivEnvoriment.mouseLeftButtonUp;


            PreFabsList.ItemsSource = preFabsList;

            preFabsList.Add(new TestUnit());
        }

        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {


            //var temp = e.Data.GetData(DataFormats.FileDrop);//сделать через D&D
            //string idPrefab = preFabsList[PreFabsList.SelectedIndex].PreFabsId;//КОСТЫЛЁЧЕК

            VisualPrimitivEnvoriment.CreatePuzzl(ref EditGrid, preFabsList[PreFabsList.SelectedIndex]);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //FileWriter fileWriter = new FileWriter();
            //fileWriter.WriteToSkript(EditGrid.Children);
        }

        private void PreFabsList_Grab(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            int indexSelectItem = listBox.SelectedIndex;

            if (indexSelectItem >= 0)
            { 
                DragDrop.DoDragDrop(listBox, (PreFabsList.Items[indexSelectItem] as BasePreFabsModel).PreFabsId, DragDropEffects.Move);
            }      
        }
    }
}
