using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Text;
using asp.net.web.Models;

namespace asp.net.web.Controllers
{
    [ApiController]
    [Route("api/products")] // API route path adjusted to reflect "api/products"
    public class ProductController : ControllerBase
    {
        private readonly FirestoreDb _firestoredb;

        // Constructor to initialize Firestore instance
        public ProductController()
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
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = credential,
                });
            }

            _firestoredb = FirestoreDb.Create("asp-net-345c8");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }

            // Ensure that 'id' is provided in the request body
            if (string.IsNullOrEmpty(product.id))
            {
                return BadRequest("The 'id' field is required.");
            }

            // Map the fields of the product object into an anonymous object for Firestore
            var firestoreProduct = new
            {
                id = product.id, // Use the provided 'id'
                name = product.name,
                price = product.price,
                description = product.description,
                category = product.category,
                stock = product.stock,
                imageUrl = product.imageUrl
            };

            // Create a document reference using the provided 'id'
            var docRef = _firestoredb.Collection("products").Document(product.id);

            try
            {
                // Save the document to Firestore with the manually provided 'id'
                await docRef.SetAsync(firestoreProduct);

                // Return the product with the provided ID and success status
                return CreatedAtAction(nameof(GetProductById), new { id = product.id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating product: {ex.Message}");
            }
        }


        // GET: /api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var productsCollection = _firestoredb.Collection("products");
            var productsSnapshot = await productsCollection.GetSnapshotAsync();
            var products = new List<Product>();

            foreach (var document in productsSnapshot.Documents)
            {
                // Ensure all fields are properly fetched from Firestore
                var product = new Product
                {
                    id = document.Id,
                    name = document.GetValue<string>("name"),
                    price = document.GetValue<double>("price"),
                    description = document.GetValue<string>("description"),
                    category = document.GetValue<string>("category"),
                    stock = document.GetValue<int>("stock"),
                    imageUrl = document.GetValue<string>("imageUrl")
                };
                products.Add(product);
            }

            return Ok(products);
        }

        // GET: /api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var productRef = _firestoredb.Collection("products").Document(id);
            var documentSnapshot = await productRef.GetSnapshotAsync();

            if (!documentSnapshot.Exists)
            {
                return NotFound("Product not found.");
            }

            var product = new Product
            {
                id = documentSnapshot.Id,
                name = documentSnapshot.GetValue<string>("name"),
                price = documentSnapshot.GetValue<double>("price"),
                description = documentSnapshot.GetValue<string>("description"),
                category = documentSnapshot.GetValue<string>("category"),
                stock = documentSnapshot.GetValue<int>("stock"),
                imageUrl = documentSnapshot.GetValue<string>("imageUrl")
            };

            return Ok(product);
        }

        // PUT: /api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product product)
        {
            var productRef = _firestoredb.Collection("products").Document(id);
            var documentSnapshot = await productRef.GetSnapshotAsync();

            if (!documentSnapshot.Exists)
            {
                return NotFound("Product not found.");
            }

            product.id = id;
            await productRef.SetAsync(product, SetOptions.MergeAll);

            return Ok("Product updated successfully.");
        }

        // DELETE: /api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var productRef = _firestoredb.Collection("products").Document(id);
            var documentSnapshot = await productRef.GetSnapshotAsync();

            if (!documentSnapshot.Exists)
            {
                return NotFound("Product not found.");
            }

            await productRef.DeleteAsync();
            return Ok("Product deleted successfully.");
        }
    }
}
