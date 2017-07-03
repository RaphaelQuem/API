using RodesAPI.DAO;
using RodesAPI.Infra;
using RodesAPI.Services;
using RodesAPI.ViewModel;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RodesAPI.Controllers
{
    public class AuthController : ApiController
    {
        [Route("rodesapi/auth")]
        public UserVM Get()
        {
            string login = Request.Headers.Get("login");
            string pass = Request.Headers.Get("pass");

            UserVM user = new UserVM();

            using (SqlConnection con = RodesAPIHelper.GetOpenConnection())
            {
                UserDAO dao = new UserDAO(con);
                user = dao.GetAuthenticatedUserVM(login, pass.Encrypt());

                if(user==null)
                    throw new  HttpResponseException(HttpStatusCode.NotFound);


                user.Token = TokenService.Tokenize(user.UserName);

                return user;
            }

        }
        [Route("rodesapi/auth")]
        public HttpResponseMessage Delete()
        {
            string login = Request.Headers.Get("login");
            TokenService.InvalidateToken(login);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}
