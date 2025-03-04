using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        [HttpPost("sort-array")]
        public IActionResult SortArray([FromBody] int[] arr)
            // [FromBody] int[] arr: Lấy dữ liệu từ body của request, dữ liệu này là một mảng số nguyên có tên là arr 
        {
            if (arr == null || arr.Length == 0)
                // Nếu mảng arr không tồn tại hoặc không có phần tử nào thì trả về BadRequest với thông báo "Mảng không hợp lệ"
            {
                return BadRequest("Mảng không hợp lê");
            }

            var sortNumber = arr.Distinct().OrderByDescending(x => x).ToList();
            // Sắp xếp mảng arr theo thứ tự giảm dần và loại bỏ các phần tử trùng nhau, sau đó chuyển kết quả về dạng List và gán vào biến sortNumber 
            return Ok(sortNumber);
        }
        [HttpPost("diem-trung-binh")]
        public IActionResult DiemTrungBinh([FromBody] float math , float eng , float his , string nganh)
        {
            if (math < 0 || math > 10 || eng < 0 || eng > 10 || his < 0 || his > 10)
			{
				return BadRequest("Điểm không hợp lệ");
			}
			var diemTB = (math + eng + his) / 3;    
            if (nganh == "his" && nganh == "eng" && nganh == "math")
            {
                diemTB = diemTB * 2;
            }
            if(diemTB >= 8)
			{
				return Ok("Học lực giỏi");
			}
			else if (diemTB >= 6.5)
			{
				return Ok("Học lực khá");
			}
			else if (diemTB >= 5)
			{
				return Ok("Học lực trung bình");
			}
			else
			{
				return Ok("Học lực yếu");
			}
        }
    }
}
