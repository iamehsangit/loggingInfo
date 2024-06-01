using Microsoft.Data.SqlClient;
using System.Data;
using WebApiProduct.Model;


namespace WebApiProduct.DataAccess
{
	public class ProductRepository
	{

		private readonly string _connectionString;
		public ProductRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<List<Product>> GetProductList()
		{
			List<Product> products = new List<Product>();

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var command =new SqlCommand("GetProductLst",connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						Product product = new Product();
						product.ProductName = reader["Name"].ToString();
						product.ProductCategory = reader["Category"].ToString();
						products.Add(product);
					}
					return products;
				}
			}
		}

		public async Task<bool> AddProduct(Product product)
		{
			using (var connection =new SqlConnection(_connectionString) )
			{
				await connection.OpenAsync();
				using (var command =new SqlCommand("SP_InsProduct", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.Add(new SqlParameter("@ProductName",product.ProductName));
					command.Parameters.Add(new SqlParameter("@ProductCategory", product.ProductCategory));

					int rowsAffects =await command.ExecuteNonQueryAsync();
					return rowsAffects > 0;
				}
			}
		}

		
	}
}
