using RodesAPI.ViewModel;
using RodesAPI.Infra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RodesAPI.DAO
{
    public class UserDAO
    {
        private IDbConnection connection;
        public UserDAO(IDbConnection con)
        {
            connection = con;
        }
        public UserVM GetAuthenticatedUserVM(string user, string cryptpass)
        {
            try
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    IDataParameter pUser = command.CreateParameter();
                    pUser.ParameterName = "P_USER";
                    pUser.Value = user;
                    pUser.DbType = DbType.String;

                    IDataParameter pPass = command.CreateParameter();
                    pPass.ParameterName = "P_PASS";
                    pPass.Value = cryptpass;
                    pPass.DbType = DbType.String;


                    command.CommandText += " SELECT		USUARIOS UserName, NCOMPS FirstName";
                    command.CommandText += " FROM       SIGCDUSU";
                    command.CommandText += " WHERE      USUARIOS     = @P_USER";
                    command.CommandText += " AND        SENHAS       = @P_PASS";


                    command.Parameters.Add(pUser);
                    command.Parameters.Add(pPass);

                    UserVM vm = command.ExecuteReader().ToSingleViewModel<UserVM>();
                    if (vm != null)
                    {
                        int firstspace = vm.FirstName.IndexOf(" ");
                        if (firstspace > 0)
                            vm.FirstName = vm.FirstName.Substring(0, firstspace);
                    }

                    return vm;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
