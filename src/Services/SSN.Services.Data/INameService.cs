using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Services.Data
{
    public interface INameService
    {
        T GetById<T>(string city);
    }
}
