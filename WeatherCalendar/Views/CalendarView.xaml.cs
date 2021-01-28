﻿using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;
using Splat;
using WeatherCalendar.Themes;

namespace WeatherCalendar.Views
{
    /// <summary>
    /// CalendarView.xaml 的交互逻辑
    /// </summary>
    public partial class CalendarView
    {
        public CalendarView()
        {
            InitializeComponent();

            this.WhenActivated(WhenActivated);
        }

        private void WhenActivated(CompositeDisposable disposable)
        {
            this.OneWayBind(
                    ViewModel,
                    model => model.CurrentMonthRows,
                    view => view.UniformGrid.Rows)
                .DisposeWith(disposable);

            this.OneWayBind(
                    ViewModel,
                    model => model.CurrentMonth,
                    view => view.DateTextBlock.Text,
                    time => time.ToString("yyyy-MM"))
                .DisposeWith(disposable);

            this.OneWayBind(
                ViewModel,
                model => model.CurrentMonth,
                view => view.Column1TextBlock.Foreground,
                _ =>
                {
                    var theme = Locator.Current.GetService<ITheme>();
                    return theme.DayNameNormalForeground;
                });

            this.OneWayBind(
                ViewModel,
                model => model.CurrentMonth,
                view => view.Column2TextBlock.Foreground,
                _ =>
                {
                    var theme = Locator.Current.GetService<ITheme>();
                    return theme.DayNameNormalForeground;
                });

            this.OneWayBind(
                ViewModel,
                model => model.CurrentMonth,
                view => view.Column3TextBlock.Foreground,
                _ =>
                {
                    var theme = Locator.Current.GetService<ITheme>();
                    return theme.DayNameNormalForeground;
                });

            this.OneWayBind(
                ViewModel,
                model => model.CurrentMonth,
                view => view.Column4TextBlock.Foreground,
                _ =>
                {
                    var theme = Locator.Current.GetService<ITheme>();
                    return theme.DayNameNormalForeground;
                });

            this.OneWayBind(
                ViewModel,
                model => model.CurrentMonth,
                view => view.Column5TextBlock.Foreground,
                _ =>
                {
                    var theme = Locator.Current.GetService<ITheme>();
                    return theme.DayNameNormalForeground;
                });

            this.OneWayBind(
                ViewModel,
                model => model.CurrentMonth,
                view => view.Column6TextBlock.Foreground,
                _ =>
                {
                    var theme = Locator.Current.GetService<ITheme>();
                    return theme.DayNameWeekendForeground;
                });

            this.OneWayBind(
                ViewModel,
                model => model.CurrentMonth,
                view => view.Column7TextBlock.Foreground,
                _ =>
                {
                    var theme = Locator.Current.GetService<ITheme>();
                    return theme.DayNameWeekendForeground;
                });

            this.BindCommand(
                    ViewModel!, 
                    model => model.LastMonthCommand, 
                    view => view.LastMonth)
                .DisposeWith(disposable);

            this.BindCommand(
                    ViewModel!, 
                    model => model.NextMonthCommand, 
                    view => view.NextMonth)
                .DisposeWith(disposable);

            this.BindCommand(
                    ViewModel!,
                    model => model.CurrentMonthCommand,
                    view => view.CurrentMonth)
                .DisposeWith(disposable);

            if (UniformGrid.Children.Count > 0)
                return;

            foreach (var mode in ViewModel.Days)
            {
                var viewFor = ViewLocator.Current.ResolveView(mode);
                if (viewFor is not UIElement element)
                    continue;

                viewFor.ViewModel = mode;

                UniformGrid.Children.Add(element);
            }
        }
    }
}
