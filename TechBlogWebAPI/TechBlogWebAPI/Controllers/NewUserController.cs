using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechBlogWebAPI.Models;

namespace TechBlogWebAPI.Controllers
{
    public class NewUserController : ApiController
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
       
        [HttpPost]
        [ActionName("AddUser")]
        public int AddUser(NewUser newUserModel)
        {        
            int success = -1;
            sqlConnection = SQLHelperClasses.SqlHelper.OpenConnection();
           
                
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter paramEmail = new SqlParameter("@emailId", System.Data.SqlDbType.VarChar);
            paramEmail.Value = newUserModel.EmailId;
            SqlParameter paramPassword = new SqlParameter("@password", System.Data.SqlDbType.VarChar);
            paramPassword.Value = newUserModel.ChoosePassword;           
            SqlParameter paramProfilePicture = new SqlParameter("@profilePicture", System.Data.SqlDbType.VarBinary);
            paramProfilePicture.Value = newUserModel.ProfilePicture;

            const string storedProcedureName = "NewUser";
            try
            {
                success = SQLHelperClasses.SqlHelper.ExecuteNonQuery(sqlConnection, cmd.CommandType, storedProcedureName,  new SqlParameter[] 
                { paramEmail,paramPassword, paramProfilePicture});               
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }

            return success;
        }
    }
}
