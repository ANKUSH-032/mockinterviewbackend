using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Mockinterview.Generic
{
    public class Authenticates
    {
        private static readonly IConfigurationRoot _iconfiguration;
        private static readonly string? _con = string.Empty;
        private static readonly string? _secretKey = string.Empty;

        static Authenticates()
        {
            var builder = new ConfigurationBuilder()
                 .  SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json");
            _iconfiguration = builder.Build();

            _con = _iconfiguration["ConnectionStrings:DataConnect"];
            _secretKey = _iconfiguration["AppSettings:Secret"];
        }
        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_con);
            }

        }
        private static LoginUser Authentication<T>(AuthenticationRequest request)
        {
            LoginUser loginUser = new();

            dynamic response = GetUserDetails<T>(Email: request.Email);
            if (response == null)
            {
                return null;
            }
            else if (response.Name.ToUpper().Equals("USERNOTREGISTER") || response.Name.ToUpper().Equals("DELETED"))
            {
                return response;
            }
            else
            {
                if (!VerifyPasswordHash(request.Password, response.PasswordHash, response.PasswordSalt))
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                          new Claim(ClaimTypes.Name , response.UserId ), new Claim(ClaimTypes.Role, response.Role), new Claim(ClaimTypes.Email, response.Email)
                    }
                    ),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                loginUser.UserId = response.UserId;
                loginUser.Email = response.Email;
                loginUser.Name = response.Name;
                loginUser.Token = tokenHandler.WriteToken(token);
                loginUser.Role = response.Role;

                return loginUser;

            }
        }
        public static dynamic Login<T>(AuthenticationRequest loginCredentials)
        {
            return Authentication<T>(loginCredentials);
        }
        public static dynamic GetUserDetails<T>(string? UserId = null, string? Email = null)
        {
            dynamic? response;

            using (IDbConnection db = Connection)
            {
                response = db.QueryFirstOrDefault<T>("[dbo].[uspUserDetailsGet]", new
                {
                    UserId,
                    Email
                }, commandType: CommandType.StoredProcedure);
            }

            return response;
        }
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            try
            {
                string storedSaltStr = Encoding.ASCII.GetString(storedSalt);
                var newPassword = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(password, storedSaltStr);
                string oldPassword = Encoding.Default.GetString(storedHash);
                return newPassword == oldPassword;
            }
            catch
            {
                throw;
            }
        }
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var mySalt = DevOne.Security.Cryptography.BCrypt.BCryptHelper.GenerateSalt();
            passwordSalt = Encoding.ASCII.GetBytes(mySalt);

            var myHash = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(password, mySalt);
            passwordHash = Encoding.ASCII.GetBytes(myHash);
        }

        public static System.Data.DataTable ConvertToDataTable<T>(List<T> items)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable(typeof(T).Name);
            //Get all the properties by using reflection
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            if (items != null && items.Count > 0)
            {
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        values[i] = Props[i].GetValue(item) ?? DBNull.Value;

                    }
                    dataTable.Rows.Add(values);
                }
            }
            return dataTable;
        }
    }
}
