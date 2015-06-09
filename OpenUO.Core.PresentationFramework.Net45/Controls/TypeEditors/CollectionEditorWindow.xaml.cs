﻿using System;
using System.Windows;
using OpenUO.Core.PresentationOpenUO.Core.Themes.TypeEditors;

namespace OpenUO.Core.PresentationOpenUO.Core.TypeEditors
{
    /// <summary>
    ///     Interaction logic for CollectionEditor.xaml
    /// </summary>
    public partial class CollectionEditorWindow : Window
    {
        public CollectionEditorWindow(CollectionEditorControl ctrl)
        {
            InitializeComponent();
            baseControl = ctrl;

            foreach(var tmp in baseControl.NumerableValue)
            {
                myLst.Items.Add(tmp);
            }

            //Visibilty of cmdAdd

            //var aa = baseControl.MyProperty.PropertyType.GetGenericArguments();
            if(!HasDefaultConstructor(baseControl.MyProperty.PropertyType.GetGenericArguments()[0]) || baseControl.MyProperty.IsReadOnly)
            {
                cmdAdd.Visibility = Visibility.Collapsed;
            }

            if(baseControl.MyProperty.IsReadOnly)
            {
                cmdRemove.Visibility = Visibility.Collapsed;
            }

            //myLst.ItemsSource = baseControl.NumerableValue;
        }

        public CollectionEditorControl baseControl
        {
            get;
            set;
        }

        public bool HasDefaultConstructor(Type type)
        {
            if(type.IsValueType)
            {
                return true;
            }

            var constructor = type.GetConstructor(Type.EmptyTypes);

            if(constructor == null)
            {
                return false;
            }

            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e)
        {
            if(myLst.SelectedItem != null)
            {
                myLst.Items.Remove(myLst.SelectedItem);
            }
            myGrid.Instance = null;
        }

        private void myLst_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            myGrid.Instance = myLst.SelectedItem;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            var newElem = Activator.CreateInstance(baseControl.MyProperty.PropertyType.GetGenericArguments()[0]);
            myLst.Items.Add(newElem);
        }
    }
}