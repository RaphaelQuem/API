using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace RodesAPI.Infra
{
    public static class RodesAPIHelper
    {
        public static RodesApiConfiguration GetConfig()
        {
            try
            {
                using (StreamReader config = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\RODESAPI.CFG"))
                {
                    string x = config.ReadToEnd();
                    return JsonConvert.DeserializeObject<RodesApiConfiguration>(x);
                }
            }
            catch
            {
                throw new Exception("Unable to find RODESAPI.CFG");
            }
        }

        public static SqlConnection GetOpenConnection()
        {

            using (RodesApiConfiguration cfg = RodesAPIHelper.GetConfig())
            {
                SqlConnection con = new SqlConnection(cfg.ConnectionStrings["Rodes"]);
                con.Open();
                return con;
            }
            
        }
    }
}