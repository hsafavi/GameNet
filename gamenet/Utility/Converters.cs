using Arash;
using gamenet.Model;
using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace gamenet
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateToPersianDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            DateTime date = (DateTime)value;
            string d;
            if (DateTime.Now.Date == date.Date)
                d = "امروز";
            else if (DateTime.Now.Date.AddDays(1) == date.Date)
                d = "فردا";
            else d = (new PersianDate(date)).ToString();
            return d + "  " + date.Minute.ToString() + " : " + date.Hour.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            PersianDate pDate = (PersianDate)value;
            return pDate.ToDateTime();
        }
    }

    [ValueConversion(typeof(DateTime), typeof(PersianDate))]
    public class DateTimeToPersianDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            DateTime date = (DateTime)value;
            return new PersianDate(date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            PersianDate pDate = (PersianDate)value;
            return pDate.ToDateTime();
        }
    }

    [ValueConversion(typeof(Station.Types), typeof(ImageSource))]
    public class ImageSourceToStationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            var type = (Station.Types)value;
            BitmapImage logo = new BitmapImage();

            switch (type)
            {
                case Station.Types.PlayStation:
                    logo.BeginInit();
                    logo.UriSource = new Uri("pack://application:,,,/gamenet;component/Resources/cons.jpg");
                    logo.EndInit();
                    return logo;
                case Station.Types.Biliard:

                    logo.BeginInit();
                    logo.UriSource = new Uri("pack://application:,,,/gamenet;component/Resources/billiard.jpg");
                    logo.EndInit();
                    return logo;
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return null;
        }
    }
    public class IndexToColorTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            var num = (int)value;
            BitmapImage logo = new BitmapImage();

            if (num % 2 == 1)
                return new SolidColorBrush(Colors.LightBlue);
            else
                return new SolidColorBrush(Colors.Transparent);
           

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return null;
        }
    }
}
