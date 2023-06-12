using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using VPMSerialezator.Models;
using ViewYourCode.Controllers;
using VPMSerialezator.Models.TestPreFabs;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using VPMSerialezator; 

namespace ViewYourCode.Envoriment
{
    public class VisualPrimitivEnvoriment
    {
        private double px;
        private double py;
        private double ox;
        private double oy;
        private bool canMove = false;
        private int idMovementedItem = -1;
        private int idTargetItem = -1;
        private ModelSeparator modelSeparator;
        
        private List<BasePreFabsModel> TempModels;


        public VisualPrimitivEnvoriment()
        {
            modelSeparator = new ModelSeparator();

            TempModels = new List<BasePreFabsModel>();
        }

        public void CreatePuzzl(ref Canvas ideGrid, BasePreFabsModel model)// create new puzzle block
        {
            
            var sepModel = modelSeparator.SeparateModels(model);//separator its work

            Grid puzle = new Grid();

            try
            {
                puzle = new Grid
                {
                    Name = "puzle_" + model.PreFabsName + ideGrid.Children.Count.ToString(),
                    MinWidth = 50,
                    MinHeight = 50,

                    Background = sepModel.Color,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(px, py, 0, 0)
                };

                puzle.MouseMove += Item_MouseMove;
                puzle.MouseLeftButtonDown += Item_MouseLeftButtonDown;
                puzle.MouseLeftButtonUp += Item_MouseLeftButtonUp;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = puzle.Name;
                sepModel.PreFabsName = puzle.Name;

                puzle.Children.Insert(0, textBlock);

                try
                {
                    if (sepModel.Operation != null)
                    {
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzleCombobox(0 ,puzle, "operator"));
                    }
                }
                catch (Exception) { }
                try
                {
                    if (sepModel.Logical != null)
                    {
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzleCombobox(1, puzle, "logical"));
                    }
                }
                catch (Exception) { }
                try
                {
                    if (sepModel.Value != null)
                    {
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzleInsertValue(puzle, "value"));
                    }
                }
                catch (Exception) { }
                try
                {
                    if (sepModel.param1 != null)
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzzlePanel(1, puzle, "param1"));
                    if (sepModel.param2 != null)
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzzlePanel(1, puzle, "param2"));
                    if (sepModel.param3 != null)
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzzlePanel(1, puzle, "param3"));
                    if (sepModel.param4 != null)
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzzlePanel(1, puzle, "param4"));
                    if (sepModel.param5 != null)
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzzlePanel(1, puzle, "param5"));
                }
                catch (Exception) { }
                try
                {
                    if (sepModel.insertedUnit != null)
                    {
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzzlePanel(2, puzle, "inserted"));
                    }
                }
                catch (Exception) { }
                try
                {
                    if (sepModel.nextUnit != null)
                        puzle.Children.Insert(puzle.Children.Count, CreatePuzzlePanel(0, puzle, "next"));

                }
                catch (Exception) { }

                TempModels.Add(sepModel);
                ideGrid.Children.Insert(ideGrid.Children.Count, puzle);

                puzle.Resources.Add("model", sepModel);
            }
            catch (Exception) { }

        }
        public ComboBox CreatePuzleCombobox(int mod, Panel Grid, string name)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.HorizontalAlignment = HorizontalAlignment.Left;
            comboBox.VerticalAlignment = VerticalAlignment.Top;
            comboBox.Margin = new Thickness(0, 16 , 0, 0);
            if (mod == 0)
            {
                comboBox.Items.Add(" + ");
                comboBox.Items.Add(" - ");
                comboBox.Items.Add(" / ");
                comboBox.Items.Add(" * ");
            }
            if (mod == 1)
            {
                comboBox.Items.Add(" or ");
                comboBox.Items.Add(" is ");
                comboBox.Items.Add(" not ");
                comboBox.Items.Add(" and ");
            }

            comboBox.SelectedIndex = 0;

            comboBox.SelectionChanged += OperatorChanged;

            return comboBox;
        }
        public TextBox CreatePuzleInsertValue(Panel Grid, string name)
        {
            TextBox textBox = new TextBox();
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.VerticalAlignment = VerticalAlignment.Top;
            textBox.Width = 20;
            textBox.Margin = new Thickness(0, 16, 0, 0);

            textBox.SelectionChanged += ChangeValue;

            return textBox;
        }
        public void ChangeValue(object sender, RoutedEventArgs e)
        {
            Panel parentPanel = ((TextBox)sender).Parent as Panel;

            BasePreFabsModel p = (BasePreFabsModel)parentPanel.FindResource("model");
            var tempP = modelSeparator.SeparateModels(p);
            tempP = p;

            try
            {
                tempP.Value = ((TextBox)sender).Text;
            }
            catch (Exception) { }

            p = tempP;
        }
        public void OperatorChanged(object sender, SelectionChangedEventArgs e)
        {
            Panel parentPanel = ((ComboBox)sender).Parent as Panel;

            BasePreFabsModel p = (BasePreFabsModel)parentPanel.FindResource("model");
            var tempP = modelSeparator.SeparateModels(p);
            tempP = p;

            try
            {
                tempP.Operation = ((ComboBox)sender).SelectedItem;
            }
            catch (Exception) { }

            p = tempP;
        }

        public Grid CreatePuzzlePanel(int turn, Panel Grid, string name)//create triger atachment zone for puzzl
        {
            Grid trigger = new Grid
            {
                Name = name
            };

            if (turn == 0)//next
            {
                trigger.Height = 10;
                trigger.Width = 20;
                trigger.Background = new SolidColorBrush(Colors.Blue);
                trigger.HorizontalAlignment = HorizontalAlignment.Left;
                trigger.VerticalAlignment = VerticalAlignment.Top;
                trigger.Margin = new Thickness(0, 32 * Grid.Children.Count, 0, 0);
            }
            else if (turn == 1)
            {//TODO: это надо исправить (Повторение кода)
                trigger.Height = 20;
                trigger.Width = 20;
                trigger.Background = new SolidColorBrush(Colors.Green);
                trigger.HorizontalAlignment = HorizontalAlignment.Left;
                trigger.VerticalAlignment = VerticalAlignment.Top;
                trigger.Margin = new Thickness(30, 30 * Grid.Children.Count, 0, 0);
            }
            else if (turn == 2)
            {//TODO: это надо исправить (Повторение кода)
                trigger.Height = 20;
                trigger.Width = 20;
                trigger.Background = new SolidColorBrush(Colors.Purple);
                trigger.HorizontalAlignment = HorizontalAlignment.Left;
                trigger.VerticalAlignment = VerticalAlignment.Top;
                trigger.Margin = new Thickness(30, 30 * Grid.Children.Count, 0, 0);
            }

            return trigger;
        }

        public void TargetUp_PuzleAtach(object sender)
        {
            Panel parentPanel = ((Panel)sender).Parent as Panel;

            double thisX = px - ox;
            double thisY = py - oy;

            foreach (var item in parentPanel.Children)// оптемизировать  каким либо методом выборки иначе на большом объёме это приведёт к увелмичению времени работы
            {// как пример искать элементы в видемой поле зрения
                if (!(item is Panel))
                {
                    continue;
                }
                if (((Panel)item).Margin.Left < thisX &&
                    (((Panel)item).Margin.Left + ((Panel)item).ActualWidth) > thisX &&
                    ((Panel)item).Margin.Top < thisY &&
                    (((Panel)item).Margin.Top + ((Panel)item).ActualHeight) > thisY)//проверка есть ли в области блока _ клик
                {//первое это поиск в общем колличестве block-ов
                    foreach (var itemChild in ((Panel)item).Children)
                    {
                        if (!(itemChild is Panel))//исключить useless texBoxes
                        {
                            continue;
                        }

                        if (((Panel)itemChild).Margin.Left < thisX - ((Panel)item).Margin.Left &&
                            (((Panel)itemChild).Margin.Left + ((Panel)itemChild).ActualWidth) > thisX - ((Panel)item).Margin.Left &&
                            ((Panel)itemChild).Margin.Top < thisY - ((Panel)item).Margin.Top &&
                            (((Panel)itemChild).Margin.Top + ((Panel)itemChild).ActualHeight) > thisY - ((Panel)item).Margin.Top)//проверка есть ли в области блока _ клик
                        {//второе поиск уже триггера в этом block
                            idTargetItem = parentPanel.Children.IndexOf((Panel)item);

                            Panel child = (Panel)parentPanel.Children[idMovementedItem];
                            if (item == child)
                            {
                                continue;
                            }
                            
                            parentPanel.Children.Remove((Panel)parentPanel.Children[idMovementedItem]);

                            child.Margin = new Thickness((((Panel)itemChild).Margin.Left + ((Panel)itemChild).ActualWidth), (((Panel)itemChild).Margin.Top + ((Panel)itemChild).ActualHeight), 0, 0);//Fix IT


                            if (((Panel)itemChild).Name == "next")
                            {
                                InsertParameterIntoBlock(child, item as Panel, 0); 
                            }
                            if (((Panel)itemChild).Name == "inserted")
                            {
                                InsertParameterIntoBlock(child, item as Panel, -1);
                            }
                            if (((Panel)itemChild).Name != "next" &&
                                ((Panel)itemChild).Name != "inserted")
                            {
                                for (int i = 1; i < 11; i++)
                                {
                                    if (((Panel)itemChild).Name == ("param" + i))
                                    {
                                        InsertParameterIntoBlock(child, item as Panel, i);
                                    }
                                }
                            }
                            


                            ((Panel)item).Children.Add(child);

                            return;
                        }
                    }

                    return;
                }
            }

        }

        private void InsertParameterIntoBlock(Panel child, Panel parentPanel, int mod)
        {
            BasePreFabsModel p = (BasePreFabsModel)parentPanel.FindResource("model");
            BasePreFabsModel c = (BasePreFabsModel)child.FindResource("model");
            var tempP = modelSeparator.SeparateModels(p);
            tempP = p;

            try
            {
                if (mod == 0)
                {
                    tempP.nextUnit = c;
                }
                if (mod == -1)
                {
                    tempP.insertedUnit = c;
                }
            }
            catch (Exception) { }
            try
            {
                if (mod == 1)
                    tempP.param1 = c;
                if (mod == 2)
                    tempP.param2 = c;
                if (mod == 3)
                    tempP.param3 = c;
                if (mod == 4)
                    tempP.param4 = c;
                if (mod == 5)
                    tempP.param5 = c;

            }
            catch (Exception) { }
  
            p = tempP;
        }

        public void TriggerAtach_MouseUp(object sender, MouseButtonEventArgs e)// atach grabes puzzle to other puzzle
        {
            if (!canMove)
            {
                return;
            }
            if (idMovementedItem < 0)
            {
                return;
            }
            Panel itemState = (Panel)((Panel)sender).Parent;
            Thickness thickness = itemState.Margin;

            if (itemState == (Panel)((Canvas)itemState.Parent).Children[idMovementedItem])
            {
                return;
            }

            Panel itemMove = (Panel)((Canvas)itemState.Parent).Children[idMovementedItem];
            itemMove.Margin = new Thickness(thickness.Left + itemState.Width, thickness.Top + itemState.Width, 0, 0);
        }

        public void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            var curpos = e.MouseDevice.GetPosition(sender as Panel);

            px = curpos.X;
            py = curpos.Y;

            //this.Title = px + " : " + py;
        }

        public void Item_MouseMove(object sender, MouseEventArgs e)// move grabes puzzle
        {
            if (!canMove)
            {
                return;
            }

            Panel item = (Panel)sender;

            if ((item.Parent as Panel).Name != "EditGrid")
            {

                var temp = item;
                var child = item.Parent as Panel;
                while (child.Name != "EditGrid")
                {
                    child = child.Parent as Panel;
                }

                (item.Parent as Panel).Children.Remove(temp);

                item.Margin = new Thickness(px - ox, py - oy, 0, 0);
                child.Children.Insert(child.Children.Count, item);

                return;
            }

            item.Margin = new Thickness(px - ox, py - oy, 0, 0);
        }

        public void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)//event for puzle, grab it
        {
            Panel item = (Panel)sender;
            Panel parent = item.Parent as Panel;

            parent.Children.Remove(item);
            parent.Children.Insert(parent.Children.Count, item);

            Thickness thickness = item.Margin;
            ox = px - thickness.Left;
            oy = py - thickness.Top;

            idMovementedItem = ((Panel)item.Parent).Children.IndexOf(item);

            canMove = true;
        }

        public void Item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)//event for puzle, drop it
        {
            TargetUp_PuzleAtach(sender);

            mouseLeftButtonUp(sender, e);
        }
        public void mouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            idMovementedItem = -1;

            canMove = false;
        }
    }
}