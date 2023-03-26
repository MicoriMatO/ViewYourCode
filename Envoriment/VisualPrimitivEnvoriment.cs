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
using ViewYourCode.Models;
using ViewYourCode.Controllers;
using ViewYourCode.Models.TestPreFabs;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

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


        public VisualPrimitivEnvoriment()
        {
            modelSeparator = new ModelSeparator();
        }

        public void CreatePuzzl(ref Canvas ideGrid, BasePreFabsModel model)// create new puzzle block
        {
            
            var sepModel = modelSeparator.SeparateModels(model);//separator its work 

            Grid puzle = new Grid();

            if (sepModel is TestUnit)
            {
                puzle = new Grid
                            {
                                Name = "puzle_" + model.PreFabsName + ideGrid.Children.Count.ToString(),
                                MinWidth = 50,
                                MinHeight = 50,
                                
                                Background = new SolidColorBrush(Colors.BlueViolet),
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                                Margin = new Thickness(px, py, 0, 0)
                            };

                puzle.MouseMove += Item_MouseMove;
                puzle.MouseLeftButtonDown += Item_MouseLeftButtonDown;
                puzle.MouseLeftButtonUp += Item_MouseLeftButtonUp;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = puzle.Name;
                puzle.Children.Insert(0, textBlock);
                
                puzle.Children.Insert(1, CreatePuzzlePanel(1, puzle, "param1"));//1 e.t.c - other params
                puzle.Children.Insert(2, CreatePuzzlePanel(1, puzle, "param2"));

                puzle.Children.Insert(3, CreatePuzzlePanel(0, puzle, "next"));//but, last - next(if true atach) // 0 - next, 1 e.t.c - params  
            } 

            

            ideGrid.Children.Insert(ideGrid.Children.Count, puzle);
        }

        public Grid CreatePuzzlePanel(int turn, Panel Grid, string name)//create triger atachment zone for puzzl
        {
            Grid trigger = new Grid
            {
                Name = name//"trigger_" + ideGrid.Children.Count.ToString() + "_" + turn
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
            {// это надо исправить (Повторение кода)
                trigger.Height = 20;
                trigger.Width = 20;
                trigger.Background = new SolidColorBrush(Colors.Green);
                trigger.HorizontalAlignment = HorizontalAlignment.Left;
                trigger.VerticalAlignment = VerticalAlignment.Top;
                trigger.Margin = new Thickness(30, 30 * Grid.Children.Count, 0, 0);
            }

            return trigger;
        }

        public void TargetUp_PuzleAtach(object sender)//НАЧАЛО ДИКОЙ ХУЙНИ!!
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
                {
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
                        {
                            idTargetItem = parentPanel.Children.IndexOf((Panel)item);

                            Panel child = (Panel)parentPanel.Children[idMovementedItem];
                            if (item == child)//block self atach
                            {
                                continue;
                            }
                            
                            parentPanel.Children.Remove((Panel)parentPanel.Children[idMovementedItem]);

                            child.Margin = new Thickness((((Panel)itemChild).Margin.Left + ((Panel)itemChild).ActualWidth), (((Panel)itemChild).Margin.Top + ((Panel)itemChild).ActualHeight), 0, 0);//Fix IT

                            //желательно следующий код вывести в отдельный метод!!!!!!!!! (ref.p ref.c) return;
                            //дальше ад boxing(а) и unboxing(а) т.е. лютая хрень

                            //добавить поиск по имени параметра и добавление в baseModel
                            //parent превратить в модель 
                            //найти параметр по названию затригереного поля
                            //chidl превратить в модель
                            //заполнить соответсвующий параметр
                            //а дальше хз
                            //мб 

                            ((Panel)item).Children.Add(child);

                            return;
                        }
                    }

                    return;
                }
            }

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