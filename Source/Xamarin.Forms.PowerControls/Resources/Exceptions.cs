using System;

namespace Xamarin.Forms.PowerControls.Resources
{
    public sealed class Exceptions
    {
        private static readonly object Lock = new object();
        private static Exceptions _instance;
        private Exceptions(){}

        public const string InvalidCast = "Cannot convert type {0} into {1}.";
        public const string ReadOnlyProperty = "Cannot override a redonly propperty.";

        public static Exceptions Get
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Exceptions());
                }
            }
        }
    }

    public static class ExceptionExtensions
    {
        public static Exception InvalidCastException(this Exceptions _, Type from, Type to)
            => new InvalidCastException(Exceptions.InvalidCast.Format(
                from.FullName,
                to.FullName));

        public static Exception ReadOnlyProperty(this Exceptions _)
            => new InvalidOperationException(Exceptions.ReadOnlyProperty);

        public static string Format(this string error, params object[] args) =>
            string.Format(error, args);
    }
}
