using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IConnectionService
    {
        void Connect();
        void Disconect();
    }
}
