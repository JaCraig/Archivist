using Archivist.BaseClasses;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.DataTypes
{
    /// <summary>
    /// </summary>
    public class Calendar : FileBaseClass<Calendar>, IComparable<Calendar>, IEquatable<Calendar>, IObjectConvertable
    {
        /// <summary>
        /// </summary>
        public List<CalendarComponent> Events { get; } = new List<CalendarComponent>();

        /// <summary>
        /// </summary>
        public List<CalendarComponent> Alarms { get; } = new List<CalendarComponent>();

        /// <summary>
        /// </summary>
        public List<CalendarComponent> FreeBusy { get; } = new List<CalendarComponent>();

        /// <summary>
        /// </summary>
        public List<CalendarComponent> Journals { get; } = new List<CalendarComponent>();

        /// <summary>
        /// </summary>
        public List<CalendarComponent> TimeZones { get; } = new List<CalendarComponent>();

        /// <summary>
        /// </summary>
        public List<CalendarComponent> ToDos { get; } = new List<CalendarComponent>();
        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override int CompareTo(Calendar? other) => throw new NotImplementedException();
        /// <summary>
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="obj"></param>
        public void ConvertFrom<TObject>(TObject obj) => throw new NotImplementedException();
        /// <summary>
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <returns></returns>
        public TObject? ConvertTo<TObject>() => throw new NotImplementedException();
        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(Calendar? other) => throw new NotImplementedException();
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string? GetContent() => throw new NotImplementedException();
    }
}