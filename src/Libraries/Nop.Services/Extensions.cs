﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;

namespace Nop.Services
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert to select list
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="enumObj">Enum</param>
        /// <param name="markCurrentAsSelected">Mark current value as selected</param>
        /// <param name="valuesToExclude">Values to exclude</param>
        /// <param name="useLocalization">Localize</param>
        /// <returns>SelectList</returns>
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj,
           bool markCurrentAsSelected = true, int[] valuesToExclude = null, bool useLocalization = true) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("An Enumeration type is required.", nameof(enumObj));

            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
                         select new { ID = Convert.ToInt32(enumValue), Name = useLocalization ? localizationService.GetLocalizedEnum(enumValue) : CommonHelper.ConvertEnum(enumValue.ToString()) };
            object selectedValue = null;
            if (markCurrentAsSelected)
                selectedValue = Convert.ToInt32(enumObj);
            return new SelectList(values, "ID", "Name", selectedValue);
        }

        /// <summary>
        /// Convert to select list
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="objList">List of objects</param>
        /// <param name="selector">Selector for name</param>
        /// <returns>SelectList</returns>
        public static SelectList ToSelectList<T>(this T objList, Func<BaseEntity, string> selector) where T : IEnumerable<BaseEntity>
        {
            return new SelectList(objList.Select(p => new { ID = p.Id, Name = selector(p) }), "ID", "Name");
        }
        public static string ToString (this DateTime dt)
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            if (workContext.WorkingLanguage.LanguageCulture == "fa-IR")
            {
                return new PersianDateTime(dt).ToString(PersianDateTimeFormat.DateTime);
            }
            else return dt.ToString();
        }

        /// <summary>
        /// Converts the value of the current PersianDateTime object to its equivalent string representation using the specified format.
        /// </summary>
        /// <param name="format">A custom date and time format string.</param>
        /// <returns>A string representation of value of the current PersianDateTime object as specified by format.</returns>
        public static string ToString (this DateTime dt, string format)
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            if (workContext.WorkingLanguage.LanguageCulture == "fa-IR")
            {
                return new PersianDateTime(dt).ToString(format);
            }
            else return dt.ToString(format);
        }

    }
}