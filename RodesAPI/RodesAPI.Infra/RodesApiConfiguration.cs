using System;
using System.Collections.Generic;

namespace RodesAPI.Infra
{
    public class RodesApiConfiguration :IDisposable
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}