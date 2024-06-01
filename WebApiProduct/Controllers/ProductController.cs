using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProduct.DataAccess;
using WebApiProduct.Model;

namespace WebApiProduct.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ProductController : ControllerBase
	{
		private readonly ProductRepository _productRepository;
		private readonly ILogger<ProductController> _logger;
		public ProductController(ProductRepository productRepository, ILogger<ProductController> logger)
		{
			_productRepository = productRepository;
			_logger = logger;
		}

		[HttpGet("GetProductsAPI")]
		public async Task<IActionResult> GetProductList()
		{
			List<Product> product =await _productRepository.GetProductList();
			return Ok(product);
		}

		[HttpPost("AddProductAPI")]

		public async Task<IActionResult> AddProduct([FromBody] Product request)
		{
			bool result =await _productRepository.AddProduct(request);
			if(result)
			{
				return Ok();
			}
			else
			{
				return BadRequest();

			}
		}

		[HttpGet]
		public OkObjectResult Test()
		{
			try
			{
				int a = Convert.ToInt32("pp");
				return Ok(200);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "error test");
				return Ok(400);
			}
			

		}
	}
}
