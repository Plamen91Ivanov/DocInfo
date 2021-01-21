using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Services.Data
{
    public interface IInfoServce
    {
        IEnumerable<T> GetInfo<T>();

        T GetAll<T>();
    }
}
