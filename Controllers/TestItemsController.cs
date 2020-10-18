using Microsoft.AspNetCore.Mvc;
using MongoTutorialDemo.Services;

namespace MongoTutorialDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestItemsController : ControllerBase
    {
        private readonly TestItemService _testItemService;

        public TestItemsController(TestItemService testItemService)
        {
            _testItemService = testItemService;
        }

        [HttpGet("testItems")]
        public IActionResult GetTestItems()
        {
           return Ok(_testItemService.GetTestItems());
        }
    }
}