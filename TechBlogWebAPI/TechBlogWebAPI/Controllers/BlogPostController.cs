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
    public class BlogPostController : ApiController
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        [HttpPost]
        public IHttpActionResult AddPost(PostsModels post)
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
            paramEmail.Value = post.EmailId;           

            SqlParameter paramPostTitle = new SqlParameter();
            paramPostTitle.ParameterName = "@PostTitle";
            paramPostTitle.Direction = System.Data.ParameterDirection.Input;
            paramPostTitle.SqlDbType = System.Data.SqlDbType.VarChar;            
            paramPostTitle.Value = post.PostTitle;

            SqlParameter paramContent = new SqlParameter();
            paramContent.ParameterName = "@Content";
            paramContent.Direction = System.Data.ParameterDirection.Input;
            paramContent.SqlDbType = System.Data.SqlDbType.VarChar;
            paramContent.Value = post.PostContent;

            SqlParameter paramPostedBy = new SqlParameter();
            paramPostedBy.ParameterName = "@PostedBy";
            paramPostedBy.Direction = System.Data.ParameterDirection.Input;
            paramPostedBy.SqlDbType = System.Data.SqlDbType.VarChar;
            paramPostTitle.Size = 50;
            paramPostedBy.Value = post.PostedBy;

            SqlParameter paramCategoryId = new SqlParameter();
            paramCategoryId.ParameterName = "@CategoryId_FK";
            paramCategoryId.Direction = System.Data.ParameterDirection.Input;
            paramCategoryId.SqlDbType = System.Data.SqlDbType.VarChar;
            paramPostedBy.Size = 50;
            paramCategoryId.Value = post.CategoryId;

            SqlParameter paramStatus = new SqlParameter();
            paramStatus.ParameterName = "@Status";
            paramStatus.Direction = System.Data.ParameterDirection.Input;
            paramStatus.SqlDbType = System.Data.SqlDbType.Char;
            paramStatus.Size = 1;
            paramStatus.Value = post.Status;

            SqlParameter paramMessage= new SqlParameter();
            paramMessage.ParameterName = "@message";
            paramMessage.Direction = System.Data.ParameterDirection.Output;
            paramMessage.Size = 100;
            paramMessage.SqlDbType = System.Data.SqlDbType.VarChar;

            cmd.Parameters.AddRange(new SqlParameter[] { paramEmail, paramCategoryId, paramContent, paramMessage, paramPostedBy, paramPostTitle, paramStatus });
            cmd.Connection = sqlConnection;
            const string procedureName = "AddPost";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = procedureName;
            try
            {
                cmd.ExecuteNonQuery();

                if (paramMessage.Value.Equals("New Post added successfully"))
                    return Ok(paramMessage.Value.ToString());
                else
                {
                    HttpError error = new HttpError(paramMessage.Value.ToString());
                    var responseMessage = Request.CreateErrorResponse(HttpStatusCode.NotModified, error);
                    throw new HttpResponseException(responseMessage);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }   
        }
    }
}
