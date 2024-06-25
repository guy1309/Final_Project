using EmedicineEB.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EmedicineEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;

        }



        [HttpPost]
        [Route("registration")]
        public Response register (Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            string? v = _configuration.GetConnectionString("EMedCS");
            SqlConnection connection = new SqlConnection(v.ToString());
            response = dal.register(users, connection);
            return response;
        }


        [HttpPost]
        [Route("login")]
        public Response login(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.login(users, connection);
            return response;
        }


        [HttpPost]
        [Route("viewUser")]
        public Response viewUser(Users users)
        {
            DAL dal = new DAL();
            string? v = _configuration.GetConnectionString("EMedCS");
            SqlConnection connection = new SqlConnection(v.ToString());
            Response response = dal.viewUser(users, connection);
            return response;
        }


        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.updateProfile(users, connection);
            return response;
        }



    }

}
