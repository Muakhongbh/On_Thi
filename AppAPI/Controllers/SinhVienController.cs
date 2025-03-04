using AppAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public SinhVienController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpPost("{a},{b},{c}")]
        public async Task<ActionResult<SinhVien>> PhuongTrinhBacBa(int a, int b, int c)
        {
           return Ok(a + b + c);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SinhVien>>> Get()
        {
            // Lấy danh sách sinh viên từ database và gán vào biến sinhViens và trả về cho client để hiển thị 
            var sinhViens = await _appDbContext.SinhViens.ToListAsync();
            return Ok(sinhViens);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SinhVien>> GetId(int id)
        {
            // Lấy sinh viên có Id = id từ database và gán vào biến sinhVien và trả về cho client để hiển thị 
            var sinhVien = await _appDbContext.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return Ok(sinhVien);
        }
        [HttpPost]  
        public async Task<ActionResult<SinhVien>> Post(SinhVien sinhVien)
        {
            // Tạo một sinh viên mới và lưu vào database và trả về cho client để hiển thị 
            _appDbContext.SinhViens.Add(sinhVien);
            await _appDbContext.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = sinhVien.Id }, sinhVien);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<SinhVien>> Put(int id, SinhVien sinhVien)
        {
            if (id != sinhVien.Id) // Kiểm tra xem Id của sinh viên có trùng với Id truyền vào không nếu không trả về BadRequest
            {
                return BadRequest();
            }
            // Cập nhật sinh viên và trả về cho client để hiển thị 
            _appDbContext.Entry(sinhVien).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<SinhVien>> Delete(int id)
        {
            // Xóa sinh viên có Id = id và trả về cho client để hiển thị 
            var sinhVien = await _appDbContext.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            _appDbContext.SinhViens.Remove(sinhVien);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
