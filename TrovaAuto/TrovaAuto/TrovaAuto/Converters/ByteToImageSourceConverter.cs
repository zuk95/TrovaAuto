﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TrovaAuto.Dominio;
using Xamarin.Forms;

namespace TrovaAuto.Converters
{
    public class ByteToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] byteImmagine = value as byte[];

            if(byteImmagine == null)
                return ImageSource.FromResource(CostantiDominio.PATH_NOIMAGE_ICON);

            var stream = new MemoryStream(byteImmagine);
            return ImageSource.FromStream(() => stream);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
