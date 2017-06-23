using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RodesAPI.Services
{
    public static class TokenService
    {
        public static Dictionary<string, string> ValidTokens { get; set; }
        public static Dictionary<string, DateTime> TokenExpiration { get; set; }
        public static void KillExpiredTokens()
        {
            List<string> keytkn = new List<string>();
            List<string> keyexp = new List<string>();
            foreach(KeyValuePair<string,string> valuePair in ValidTokens)
            {
                if (TokenService.TokenExpiration[valuePair.Value] < DateTime.Now)
                {
                    keytkn.Add(valuePair.Key);
                    keyexp.Add(valuePair.Value);
                }                   
            }
            for (int i =0;i< keytkn.Count;i++)
            {
                TokenService.ValidTokens.Remove(keytkn[i]);
                TokenService.TokenExpiration.Remove(keyexp[i]);
            }
        }
        public static string Tokenize(string value)
        {
            string token = "";
            string tokenizedvalue = "";
            foreach(char chr in value)
            {
                tokenizedvalue += (int)chr;
            }

            string time = DateTime.Now.Second.ToString() + DateTime.Now.DayOfYear.ToString() + DateTime.Today.Year.ToString().Substring(3) ;

            for(int i=0; i< (tokenizedvalue.Length>time.Length? time.Length: tokenizedvalue.Length);i++)
            {
                token += time[i] + (tokenizedvalue[i] * time[i]);
            }
            token = token.Substring(0, 20);

            if (TokenService.ValidTokens == null)
                TokenService.ValidTokens = new Dictionary<string, string>();
            if (TokenService.TokenExpiration == null)
                TokenService.TokenExpiration = new Dictionary<string, DateTime>();

            if (TokenService.ValidTokens.ContainsKey(value))
                TokenService.ValidTokens.Remove(value);
            if (TokenService.TokenExpiration.ContainsKey(token))
                TokenService.TokenExpiration.Remove(token);

            TokenService.ValidTokens.Add(value.ToUpper(), token);
            TokenService.TokenExpiration.Add(token, DateTime.Now.AddHours(4));

            return token;
        }
        public static bool ValidateToken(string token)
        {
            KillExpiredTokens();
            if (TokenService.ValidTokens == null)
                return false;

            if (TokenService.TokenExpiration.ContainsKey(token))
                TokenService.TokenExpiration.Remove(token);
            TokenService.TokenExpiration.Add(token, DateTime.Now.AddMinutes(2));

            return ValidTokens.ContainsValue(token);
        }
        public static void  InvalidateToken(string user)
        {
            user = user.ToUpper();
            string token = "";
            if (TokenService.ValidTokens != null)
            {
                if (TokenService.ValidTokens.ContainsKey(user))
                {
                    token = TokenService.ValidTokens[user];
                    TokenService.ValidTokens.Remove(user);
                }
            }
            if (TokenService.TokenExpiration == null)
            {
                if (TokenService.TokenExpiration.ContainsKey(token))
                    TokenService.TokenExpiration.Remove(token);
            }
        }
    }
}