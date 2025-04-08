using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using asp.net.web.Models;
using System.Text;

namespace asp.net.web.Controllers
{
    [ApiController]
    [Route("products")]
    public class MsgController : ControllerBase
    {
        private readonly FirestoreDb _firestoredb;
       
        public MsgController()
        {
            string firebasekey = @"{
                ""type"": ""service_account"",
                ""project_id"": ""asp-net-345c8"",
                ""private_key_id"": ""85daf38d0a087441efe4df786e9517e25ebbee82"",
                ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC4IA2iIjLO08Uf\nt9dIYZ1yGuCdl+mY59z0Gf79mdpP+evLzb7242kDHW/Rj0dvWpMZsE4XV6F8mVjY\ndKLCNtY8U4iFMrnsKhNs9jRDYWy9KIZ17Feh9IoXo8zBwlJADDwtLiqiDG9xxSn3\nSb0xBgVRKUiuPcbLyd2Qk8hbeedM/STBrN+WHfQrowyYO2Jb7nZpJUUbmMO6hAPG\nkvNbkslMoC9vzblylODIEZyMDB5lenaqpxkG8eZ7K/s+yIltfltiIPR67m9iDSU3\nBfFtsEVkdRmkQPBHDZ1q38i+RH2rd/+9ZSk7c3h/jMY10wdwKwCEXOWAx9cb7nSd\nBCVpUa59AgMBAAECggEAHwwCmH/5PsBdOSGz+qfBF3Q6Q0CSGl8gdgWrJkKK2hj7\nfmBZTsKeWrDcQcMN6dQlQTvmExAK8hpebZNPPX3nJoF0X/dje9PFdkZWnjT/k67R\na3F4fl3gaieL13EnktatT/X8qNn9cbrr/l9v+CP6ggq6z2ypyIOnqWEN9ATMcIHm\nuAi1udst8SIyEK29xx3ZwJFHTCT5mNU25NsWBQHx1nuSR1EcpvYteQd5+JSBeaGd\nNFSFSBSpLYuD+f1acnkPjLk2B9QWE5ieflaPxH/tUV9OW9ivhFfQJCI8vr02dCJM\nuX8Clxp1r1+dqeNUIj1jsPcQbPrG777P6E+b41CZUQKBgQDa7TCA4IQA61tB6Rd5\nr4tFi6tjGYwOj+hRIHqcOUzM3eu23/a4s2vYvEssw9Jwrqk4l2imG4KlqHwdDEYS\nIp7OlPpuclx2J9Md6JY/uMLQbUvOhB67g+8ouR4MK33isRQQgyOADi+N9sne/yS3\nfxZNuERikQ3+pgSaoNnflRdV7QKBgQDXTiwAaUienPsu91pKrgd97aC2QVsIF9G7\nk96r//K+QE5NsjEJpvwgPmTJa8tKwg53w+VMYAkRhnAafH5j6Bx7QKBObEX39e9c\n6TY8EDg8FKGVIwo3GezjF6qJ8Gs/E7F029O5rgsIEZnP2E8odbF2nCXqWoKRZ5Xf\nK9uBGEWo0QKBgGOA6Mm6weSVFG45nkTdh6R9XdF1/BVmTQYKiA/Xb1OyDf+Zfc7n\nJb5lnpljC5PRnvIsxxCwckoO8RJW0MZPW/Sy+7wVWHcPlMIEQ74EoO8QriLYJAvA\nZIQS11hasCXHrEHxCMKcL/sLTyd+udZ4+c8rUFGocj7qgC8zqrMXVXrlAoGBANRE\nOZVeNz5JPksbimU+FhzM/jkxTfI4qYnpSwsAF+4BsDFhkH8XplKTsQHzyEU39NOW\nyqX1uHsSs8spGeKdoBbTrDgk/wZr7UUIl3O3+fkhzfwew593a9ioKHY+FT3myHmR\nkLfrIu0djSsg80nMXt21LJxUB44bNeMEdjBcIbFBAoGAEGdI8aRI8/JYbr6sNtTw\nZTxu+nGQIjXZ92ujriW4XHVqzXol7E3kNioez2wBQyOc8p7KAJ3GNQ6KIXlB+r3a\nPr/B3LLSX+Q82akmp+6lbaS4geqEeVjEb8Im7YLj29a42HU+FliWxv6ZO9LcReW/\ns/3wBvnpcvL7XnTFYRdZR4Y=\n-----END PRIVATE KEY-----\n"",
                ""client_email"": ""firebase-adminsdk-fbsvc@asp-net-345c8.iam.gserviceaccount.com"",
                ""client_id"": ""109123410979235055675"",
                ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
                ""token_uri"": ""https://oauth2.googleapis.com/token"",
                ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
                ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-fbsvc%40asp-net-345c8.iam.gserviceaccount.com"",
                ""universe_domain"": ""googleapis.com""
                }";
            
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(firebasekey)); 

            var credential = GoogleCredential.FromStream(stream);
            if(FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = credential,
                });
            }

            _firestoredb = FirestoreDb.Create("asp-net-345c8");
        }
    
        [HttpPost("product")]
        public async Task<IActionResult> AddSimple([FromBody] Message data)
        {
            var docRef = _firestoredb.Collection("product").Document();
            await docRef.SetAsync(data);
            return Ok("Sending complete");
        }
    }
}
