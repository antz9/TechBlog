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
    public class ProfileController : ApiController
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        [HttpPost]
        public IHttpActionResult AddProfileDetails(ProfileModels profileModel)
        {
            sqlConnection = SQLHelperClasses.SqlHelper.OpenConnection();

            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter paramFullName = new SqlParameter();
            paramFullName.ParameterName = "@Fullname";
            paramFullName.Direction = System.Data.ParameterDirection.Input;
            paramFullName.SqlDbType = System.Data.SqlDbType.VarChar;
            paramFullName.Value = profileModel.FullName;

            SqlParameter paramDOB = new SqlParameter();
            paramDOB.ParameterName = "@DateOfBirth";
            paramDOB.Direction = System.Data.ParameterDirection.Input;
            paramDOB.SqlDbType = System.Data.SqlDbType.DateTime;
            paramDOB.Value = profileModel.DateOfBirth;

            SqlParameter paramOrganization = new SqlParameter();
            paramOrganization.ParameterName = "@Organization";
            paramOrganization.Direction = System.Data.ParameterDirection.Input;
            paramOrganization.SqlDbType = System.Data.SqlDbType.VarChar;
            paramOrganization.Value = profileModel.Organization;            

            SqlParameter paramCity = new SqlParameter();
            paramCity.ParameterName = "@City";
            paramCity.Direction = System.Data.ParameterDirection.Input;
            paramCity.SqlDbType = System.Data.SqlDbType.VarChar;
            paramCity.Value = profileModel.City;

            SqlParameter paramMobile = new SqlParameter();
            paramMobile.ParameterName = "@Mobile";
            paramMobile.Direction = System.Data.ParameterDirection.Input;
            paramMobile.SqlDbType = System.Data.SqlDbType.VarChar;
            paramMobile.Value = profileModel.Mobile;

            SqlParameter paramCollege = new SqlParameter();
            paramCollege.ParameterName = "@College";
            paramCollege.Direction = System.Data.ParameterDirection.Input;
            paramCollege.SqlDbType = System.Data.SqlDbType.VarChar;
            paramCollege.Value = profileModel.College;

            SqlParameter paramAboutMe = new SqlParameter();
            paramAboutMe.ParameterName = "@Aboutme";
            paramAboutMe.Direction = System.Data.ParameterDirection.Input;
            paramAboutMe.SqlDbType = System.Data.SqlDbType.VarChar;
            paramAboutMe.Value = profileModel.AboutMe;

            SqlParameter paramGender = new SqlParameter();
            paramGender.ParameterName = "@Gender";
            paramGender.Direction = System.Data.ParameterDirection.Input;
            paramGender.SqlDbType = System.Data.SqlDbType.Char;
            paramGender.Value = profileModel.Sex;

            SqlParameter paramLoginId = new SqlParameter();
            paramLoginId.ParameterName = "@LoginId_FK";
            paramLoginId.Direction = System.Data.ParameterDirection.Input;
            paramLoginId.SqlDbType = System.Data.SqlDbType.VarChar;
            paramLoginId.Value = GetLoginId(profileModel.EmailId);

            SqlParameter paramSuccess = new SqlParameter();
            paramSuccess.ParameterName = "@success";
            paramSuccess.Direction = System.Data.ParameterDirection.Output;
            paramSuccess.SqlDbType = System.Data.SqlDbType.VarChar;
            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                SQLHelperClasses.SqlHelper.CloseConnection();
            }
            if (bool.Parse(paramSuccess.Value.ToString()))
                return Ok(paramLoginId.Value.ToString());
            else
                return NotFound();
        }

        private string GetLoginId(string emailId)
        {
            string loginId = string.Empty;

            SqlParameter paramEmail = new SqlParameter();
            paramEmail.ParameterName = "@EmailId";
            paramEmail.Direction = System.Data.ParameterDirection.Input;
            paramEmail.SqlDbType = System.Data.SqlDbType.VarChar;
            paramEmail.Value = emailId;

            SqlParameter paramLoginId = new SqlParameter();
            paramLoginId.ParameterName = "@LoginId";            
            paramLoginId.SqlDbType = System.Data.SqlDbType.VarChar;
            paramLoginId.Direction = System.Data.ParameterDirection.Output;
            paramLoginId.Size = 50;
            cmd.Parameters.AddRange(new SqlParameter[] { paramEmail, paramLoginId });
            const string storedProcedureName = "GetLoginIdForUser";
            cmd.CommandText = storedProcedureName;
            cmd.Connection = sqlConnection;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.ToString());
            }
            if (paramLoginId.Value != null)
                loginId = paramLoginId.Value.ToString();

            return loginId;
        }
    }    
}

