using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RodesAPI.Infra
{
    public static class ExtensionMethods
    {
        private const string K_SENHASTRING = "<CDOXS;2APKJ:3YUHL=TR05I>V91BW?7GEQ6@M8N4ZF";
        public static string[] Get(this HttpRequestHeaders headers, params string[] names )
        {
            string[] values =  new string[names.Length];
            for(int i=0;i< names.Length;i++)
            {
                foreach (KeyValuePair<string, System.Collections.Generic.IEnumerable<string>> header in headers)
                {
                    if (header.Key.Equals(names[i]))
                    {
                        values[i] = header.Value.FirstOrDefault();
                    }
                }
            }
            return values;
        }
        public static string Get(this HttpRequestHeaders headers, string name)
        {
            foreach (KeyValuePair<string, System.Collections.Generic.IEnumerable<string>> header in headers)
            {
                if (header.Key.Equals(name))
                {
                    return header.Value.FirstOrDefault();
                }
            }
            return null;
        }
        public static T ToSingleViewModel<T>(this IDataReader rdr)
        {

            if (rdr.Read())
            {
                T obj = (T)Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo info in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (rdr[info.Name].GetType().Name.Equals("String"))
                            info.SetValue(obj, rdr[info.Name].ToString().Trim());
                        else
                            info.SetValue(obj, rdr[info.Name]);
                    }
                    catch
                    { }
                }
                return obj;
            }
            else
                return default(T);
        }
        public static List<T> ToViewModel<T>(this IDataReader rdr)
        {

            List<T> objlist = new List<T>();

            while (rdr.Read())
            {
                T obj = (T)Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo info in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (rdr[info.Name].GetType().Name.Equals("String"))
                            info.SetValue(obj, rdr[info.Name].ToString().Trim());
                        else
                            info.SetValue(obj, rdr[info.Name]);
                    }
                    catch
                    { }
                }
                objlist.Add(obj);
            }


            return objlist.Count.Equals(0)?null:objlist;
        }
        public static string Encrypt(this string clearTextPassword)
        {
            String encryptedPasword = String.Empty;
            foreach (char passChar in clearTextPassword.ToUpper())
            {
                Int32 charVal = Convert.ToInt32(passChar);
                if (charVal > 47 && charVal < 90)
                {
                    Int32 constPosition = charVal - 48;
                    encryptedPasword += K_SENHASTRING.Substring(constPosition, 1);
                }
                else
                    encryptedPasword += passChar;
            }
            return encryptedPasword;
        }
        public static string Decrypt(this string encryptedPassword)
        {
            String clearTextPassword = String.Empty;
            foreach (char passChar in encryptedPassword)
            {
                Int32 charVal = Convert.ToInt32(passChar);
                if (charVal > 47 && charVal < 90)
                {
                    charVal = K_SENHASTRING.IndexOf(passChar) + 48;
                }
                clearTextPassword += char.ConvertFromUtf32(charVal);
            }
            return clearTextPassword;
        }
    }
}
