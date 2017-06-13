using RodesAPI.DAO;
using RodesAPI.Infra;
using RodesAPI.Services;
using RodesAPI.ViewModel;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace RodesAPI.Controllers
{
    public class ItemsController : ApiController
    {
        
        [Route("rodesapi/items/{uniqueid}")]
        [Auth]
        public ItemsVM Get(string uniqueid)
        { 
            ItemsVM item = new ItemsVM();
            
            using (SqlConnection con =  RodesAPIHelper.GetOpenConnection())
            {
                ItemsDAO dao = new ItemsDAO(con);
                item = dao.GetItemVMByItemID(uniqueid);
            }                                 
            
            if(item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return item;

        }
    }
}
