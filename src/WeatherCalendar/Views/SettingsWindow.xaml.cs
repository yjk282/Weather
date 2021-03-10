﻿using ReactiveUI;
using System;
using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using WeatherCalendar.ViewModels;

namespace WeatherCalendar.Views
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();

            ViewModel = new SettingsViewModel();

            this.WhenActivated(WhenActivated);
        }

        private void WhenActivated(CompositeDisposable disposable)
        {
            this.OneWayBind(
                    ViewModel,
                    model => model.Provinces,
                    view => view.ProvincesComboBox.ItemsSource)
                .DisposeWith(disposable);

            this.OneWayBind(
                    ViewModel,
                    model => model.Districts,
                    view => view.DistrictsComboBox.ItemsSource)
                .DisposeWith(disposable);

            this.OneWayBind(
                    ViewModel,
                    model => model.Cities,
                    view => view.CitiesComboBox.ItemsSource)
                .DisposeWith(disposable);

            this.Bind(
                    ViewModel,
                    model => model.SelectedProvince,
                    view => view.ProvincesComboBox.SelectedValue)
                .DisposeWith(disposable);

            this.Bind(
                    ViewModel,
                    model => model.SelectedDistrict,
                    view => view.DistrictsComboBox.SelectedValue)
                .DisposeWith(disposable);

            this.Bind(
                    ViewModel,
                    model => model.SelectedCity,
                    view => view.CitiesComboBox.SelectedValue)
                .DisposeWith(disposable);

            this.OneWayBind(
                    ViewModel,
                    model => model.SelectedCityInfo,
                    view => view.ErrorTextBlock.Visibility,
                    city => city == null ? Visibility.Visible : Visibility.Hidden)
                .DisposeWith(disposable);

            this.Bind(
                    ViewModel,
                    model => model.IsWorkTimerVisible,
                    view => view.IsWorkTimerVisibleCheckBox.IsChecked)
                .DisposeWith(disposable);

            this.Bind(
                    ViewModel,
                    model => model.WorkTimerStartTime,
                    view => view.WorkTimerStartTimePicker.Text,
                    time => time.ToString("h\\:mm"),
                    text => TimeSpan.TryParseExact(
                        text,
                        "h\\:mm",
                        CultureInfo.CurrentUICulture,
                        out var result)
                        ? result
                        : TimeSpan.Zero)
                .DisposeWith(disposable);

            this.Bind(
                    ViewModel,
                    model => model.WorkTimerEndTime,
                    view => view.WorkTimerEndTimePicker.Text,
                    time => time.ToString("h\\:mm"),
                    text => TimeSpan.TryParseExact(
                        text,
                        "h\\:mm",
                        CultureInfo.CurrentUICulture,
                        out var result)
                        ? result
                        : TimeSpan.Zero)
                .DisposeWith(disposable);

            this.OneWayBind(
                    ViewModel,
                    model => model.IsWorkTimerVisible,
                    view => view.WorkTimerStartTimePicker.IsEnabled)
                .DisposeWith(disposable);

            this.OneWayBind(
                    ViewModel,
                    model => model.IsWorkTimerVisible,
                    view => view.WorkTimerEndTimePicker.IsEnabled)
                .DisposeWith(disposable);

            this.TitleBorder
                .Events()
                .MouseLeftButtonDown
                .Do(_ => DragMove())
                .Subscribe()
                .DisposeWith(disposable);

            this.CloseButton
                .Events()
                .Click
                .Do(_ => Close())
                .Subscribe()
                .DisposeWith(disposable);
        }
    }
}
