﻿using MobileSoLienLac.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title = "Home"},
                new HomeMenuItem {Id = MenuItemType.Notify, Title = "Thông Báo"},
                new HomeMenuItem {Id = MenuItemType.Point, Title = "Điểm"},
                new HomeMenuItem {Id = MenuItemType.Fee, Title = "Học phí"},
                new HomeMenuItem {Id = MenuItemType.TTable, Title = "TKB"},
                new HomeMenuItem {Id = MenuItemType.ChangePass, Title = "Đổi mật khẩu"},
                new HomeMenuItem {Id = MenuItemType.About, Title = "Thông tin app"},
                new HomeMenuItem {Id = MenuItemType.Logout, Title = "Đăng xuất"}
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}