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
    public class CategoryController : ApiController
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        [HttpPost]
        public IHttpActionResult AddCategory(CategoryModels category)
        {
            sqlConnection = SQLHelperClasses.SqlHelper.OpenConnection();

            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter paramEmail = new SqlParameter();
            paramEmail.ParameterName = "@EmailId";
            paramEmail.Direction = System.Data.ParameterDirection.Input;
            paramEmail.SqlDbType = System.Data.SqlDbType.VarChar;
            paramEmail.Size = 50;
            paramEmail.Value = category.emailId;

            SqlParameter paramCategoryName = new SqlParameter();
            paramCategoryName.ParameterName = "@CategoryName";
            paramCategoryName.Direction = System.Data.ParameterDirection.Input;
            paramCategoryName.SqlDbType = System.Data.SqlDbType.VarChar;
            paramCategoryName.Size = 50;
            paramCategoryName.Value = category.categoryName;

            SqlParameter paramMessage = new SqlParameter();
            paramMessage.ParameterName = "@message";
            paramMessage.Direction = System.Data.ParameterDirection.Output;
            paramMessage.SqlDbType = System.Data.SqlDbType.VarChar;
            paramMessage.Size = 150;
            

            cmd.Parameters.AddRange(new SqlParameter[] { paramEmail, paramCategoryName, paramMessage});
            cmd.Connection = sqlConnection;
            const string procedureName = "AddCategory";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = procedureName;

            try
            {
                cmd.ExecuteNonQuery();
                if (paramMessage.Value.ToString().Equals("Category was added successfully"))
                    return Ok(paramMessage.Value.ToString());
                else
                {
                    var error = new HttpError("Problem inserting a new category");
                    var httpRespMsg = Request.CreateErrorResponse(HttpStatusCode.NotModified, error);
                    throw new HttpResponseException(httpRespMsg);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                SQLHelperClasses.SqlHelper.CloseConnection();
            }
            
        }
    }
}
