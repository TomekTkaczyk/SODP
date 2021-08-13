using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Application
{
    //https://stackoverflow.com/questions/7641298/automapper-null-string-to-empty/16476293
    public class NullStringConverter : ITypeConverter<string, string>
    {
        public string Convert(string source, string destination, ResolutionContext context)
        {
            return source ?? string.Empty;
        }
    }
}
