﻿using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using Weather;
using WeatherCalendar.Models;

// ReSharper disable UnassignedGetOnlyAutoProperty

namespace WeatherCalendar.ViewModels
{
    public class DayViewModel : ReactiveObject
    {
        /// <summary>
        /// 日期信息
        /// </summary>
        [Reactive]
        public DateInfo Date { get; set; }
        
        /// <summary>
        /// 是否为当日
        /// </summary>
        [ObservableAsProperty]
        public bool IsCurrentDay { get; }

        /// <summary>
        /// 是否为当前月
        /// </summary>
        [Reactive]
        public bool IsCurrentPageMonth { get; set; }

        /// <summary>
        /// 天气信息
        /// </summary>
        [Reactive]
        public ForecastInfo Forecast { get; set; }

        /// <summary>
        /// 公历日期
        /// </summary>
        [ObservableAsProperty]
        public string DayName { get; }

        /// <summary>
        /// 农历日期
        /// </summary>
        [ObservableAsProperty]
        public string LunarDayName { get; }

        /// <summary>
        /// 节气
        /// </summary>
        [ObservableAsProperty]
        public string SolarTermName { get; }

        /// <summary>
        /// 节假日
        /// </summary>
        [ObservableAsProperty]
        public string HolidayName { get; }

        /// <summary>
        /// 是否为中国节假日
        /// </summary>
        [ObservableAsProperty]
        public bool IsChineseHoliday { get; }

        /// <summary>
        /// 是否为周末
        /// </summary>
        [ObservableAsProperty]
        public bool IsWeekend { get; }

        public DayViewModel()
        {
            Date = new DateInfo();

            this.WhenAnyValue(x => x.Date.Date)
                .Select(d => d.Date == DateTime.Today)
                .ToPropertyEx(this, model => model.IsCurrentDay);

            this.WhenAnyValue(x => x.Date.Date)
                .Select(d => d.Day.ToString())
                .ToPropertyEx(this, model => model.DayName);

            this.WhenAnyValue(x => x.Date.Date)
                .Select(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday)
                .ToPropertyEx(this, model => model.IsWeekend);

            this.WhenAnyValue(
                    x => x.Date.LunarDayName,
                    x => x.Date.LunarMonthName,
                    x => x.Date.LunarMonthSizeFlag,
                    x => x.Date.LunarLeapMonthFlag,
                    (lunarDayName,
                        lunarMonthName,
                        lunarMonthSizeFlag,
                        lunarLeapMonthFlag) =>
                    {
                        if (lunarDayName == "初一")
                            return $"{lunarLeapMonthFlag}{lunarMonthName}{lunarMonthSizeFlag}";

                        return lunarDayName;
                    })
                .ToPropertyEx(this, model => model.LunarDayName);

            this.WhenAnyValue(
                    x => x.Date.SolarTerm,
                    x => x.Date.ShuJiuOrDogDays,
                    (solarTerm, shuJiuOrDogDays) =>
                    {
                        if (!string.IsNullOrWhiteSpace(solarTerm))
                            return solarTerm;

                        if (!string.IsNullOrWhiteSpace(shuJiuOrDogDays))
                            return shuJiuOrDogDays;

                        return "";
                    })
                .ToPropertyEx(this, model => model.SolarTermName);

            this.WhenAnyValue(
                    x => x.Date.ChineseHoliday,
                    x => x.Date.Holiday,
                    (chineseHoliday, holiday) =>
                    {
                        if (!string.IsNullOrWhiteSpace(chineseHoliday))
                            return chineseHoliday;

                        return holiday;
                    })
                .ToPropertyEx(this, model => model.HolidayName);

            this.WhenAnyValue(x => x.Date.ChineseHoliday)
                .Select(holiday => !string.IsNullOrWhiteSpace(holiday))
                .ToPropertyEx(this, model => model.IsChineseHoliday);
        }

        public override string ToString()
        {
            return Date?.Date.ToString("yyyy-MM-dd");
        }
    }
}
