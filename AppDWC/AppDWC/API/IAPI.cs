using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDWC.API
{
    public interface IAuthAPI
    {
        [Post("/login")]
        Task<string> SignIn([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

        [Post("/register")]
        Task<string> Register([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

        [Put("/update-quote")]
        Task<string> Update([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

        [Put("/register-pin")]
        Task<string> RegisterPin([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);


        [Post("/find")]
        Task<string> Find([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

        [Post("/getToken")]
        Task<string> GetToken([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

        [Post("/loginPin")]
        Task<string> LoginPin([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, int> data);
    }
}